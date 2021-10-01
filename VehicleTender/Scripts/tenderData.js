$(document).ready(() => {
    loadData();
});

function loadData() {
    if ($("#tenderInfo").length > 0) {
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
        key: ["Id", "IdBid"],
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
        },
        update: function (key, values) {
            console.log(key.Id);
            console.log(key.IdBid);
            console.log(values.BidPrice);
            $.ajax({
                url: "/Home/EditBid",
                type: "POST",
                data: JSON.stringify({ TenderStockId: key.Id }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
            var url = window.location.pathname;
            var id = url.substring(url.lastIndexOf('/') + 1);
            $.ajax({
                url: "/Home/AddBid",
                type: "POST",
                data: JSON.stringify({ TenderStockId: key.Id, Price: values.BidPrice, TenderId: id }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
            dataSource.refresh();
        },
    })

    $("#tenderCars").dxDataGrid({
        dataSource: usersDataSource,
        showBorders: true,
        columnAutoWidth: true,
        columns: [
            {
                dataField: "Id",
                allowEditing: false,
                visible: false
            }, {
                dataField: "RegNo",
                allowEditing: false
            }, {
                dataField: "Year",
                allowEditing: false,
                dataType: 'Text'
            }, {
                dataField: "Make",
                allowEditing: false
            }, {
                dataField: "CarLine",
                allowEditing: false
            }, {
                dataField: "Model",
                allowEditing: false
            }, {
                dataField: "Mileage",
                allowEditing: false,
                dataType: 'Text'
            }, {
                dataField: "Comments",
                allowEditing: false
            }, {
                dataField: "IdBId",
                allowEditing: true,
                visible: false
            },
            {
                dataField: "IdBid",
                allowEditing: false,
                visible: false
            }, {
                dataField: "BidPrice",
                allowEditing: true
            }
        ],
        editing: {
            mode: "batch",
            allowUpdating: true,
            allowAdding: false,
            allowDeleting: false,
            selectTextOnEditStart: true,
            startEditAction: "click"
        },
        summary: {
            totalItems: [{
                column: "BidPrice",
                summaryType: "sum",
                valueFormat: "currency"
            }]
        },
    }).dxDataGrid("instance");
}