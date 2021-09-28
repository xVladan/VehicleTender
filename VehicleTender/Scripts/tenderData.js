$(document).ready(() => {
    loadData();
});

function loadData() {
    if ($("#tenderInfo").length>0) {
        LoadTender();
    } else {
        LoadAdminTenderData();
    }
}
function LoadAdminTenderData() {
}

async function LoadTender() {
    var tender = await LoadTenderData();
    await LoadTenderCars(tender.Id);
    await LoadTenderBids(tender.Id);
    
}
async function LoadTenderData() {
    var url = window.location.pathname;
    var dataTender;
    var id = url.substring(url.lastIndexOf('/') + 1);
    return $.ajax({
        url: "/Home/GetTender",
        type: "POST",
        data: JSON.stringify({ Id: id }),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: (data) => {
            dataTender = data;
            tenderId = dataTender.Id;
            $("#tenderNo").text(dataTender.TenderNo);
            $("#openDate").text(dataTender.OpenDate);
            $("#sellerName").text(dataTender.Dealer);
            $("#closeDate").text(dataTender.CloseDate);
            $("#tenderContact").text(dataTender.DealerName);
        },
        error: (data) => {
        }
    });
}
async function LoadTenderCars(id) {
    let userPromis = $.Deferred();
    let usersDataSource = new DevExpress.data.DataSource({
        id: "dataGrid",
        key: "Id",
        load: () => {
            $.ajax({
                type: "POST",
                url: "/Home/GetTenderCars",
                data: JSON.stringify({ Id: id }),
                contentType: 'application/json; charset=utf-8',
                success: (data) => {
                    userPromis.resolve(data);
                },
                error: (data) => {
                    userPromis.reject(data);
                }
            });
            return userPromis.promise();
        }
    })
    $("#tenderCars").dxDataGrid({
        dataSource: usersDataSource,
        showBorders: true,
        columnAutoWidth: true,
        columns: ["Id", "RegNo", "Year", "Make", "CarLine", "Model", "Mileage", "Comments"],
    }).dxDataGrid("columnOption", "Id", "visible", false);
    
}
async function LoadTenderBids() {
    $.ajax({
        type: "POST",
        url: "/Home/GetTenderBids",
        data: JSON.stringify({ Id: id }),
        contentType: 'application/json; charset=utf-8',
        success: (data) => {
            debugger;
        },
        error: (data) => {
        }
    });
}
