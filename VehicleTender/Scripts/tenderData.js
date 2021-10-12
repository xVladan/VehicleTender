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
    var url = window.location.pathname;
    var id = url.substring(url.lastIndexOf('/') + 1);
    $.ajax({
        url: "/Home/GetTender",
        type: "POST",
        data: JSON.stringify({ Id: id }),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: (tender) => {
            $("#tenderNo").text(tender.TenderNo);
            $("#openDate").text(tender.OpenDate);
            $("#sellerName").text(tender.Dealer);
            $("#closeDate").text(tender.CloseDate);
            $("#tenderContact").text(tender.DealerName);
        },
        error: (tenderPromis) => {
            console.log(tenderPromis);
        }
    });
    loadAdminTenderCarsGrid(id);
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

function loadBids(tenderId, stockId) {
    let bidsPromis = $.Deferred();
    let dataGridInstance = $("#tenderStocks").dxDataGrid("instance");
    let tenderBidsDataSource = new DevExpress.data.DataSource({
        key: "Id",
        load: () => {
            $.ajax({
                type: "POST",
                url: "/Home/GetBids",
                data: JSON.stringify({ Id: tenderId, stockId: stockId }),
                contentType: 'application/json; charset=utf-8',
                success: (data) => {
                    bidsPromis.resolve(data);
                },
                error: (data) => {
                    bidsPromis.reject(data);
                }
            });
            return bidsPromis.promise();
        }
    });
    return tenderBidsDataSource;
}

function loadCars(id) {
    let tenderCarsPromis = $.Deferred();
    let tenderCarsDataSource = new DevExpress.data.DataSource({
        id: "tenderCarsGrid",
        key: "Id",
        load: () => {
            $.ajax({
                type: "POST",
                url: "/Home/GetTenderCars",
                data: JSON.stringify({ Id: id }),
                contentType: 'application/json; charset=utf-8',
                success: (data) => {
                    tenderCarsPromis.resolve(data);
                },
                error: (data) => {
                    tenderCarsPromis.reject(data);
                }
            });
            return tenderCarsPromis.promise();
        }
    });
    return tenderCarsDataSource;
}

async function loadAdminTenderCarsGrid(id) {
    const grid = $("#tenderStocks").dxDataGrid({
        dataSource: loadCars(id),
        keyExpr: "Id",
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
            }
        ],
        masterDetail: {
            enabled: true,
            template: function (container, options) {
                const stockId = options.data.Id;
                $("<div>")
                    .dxDataGrid({
                        dataSource: loadBids(id, stockId),
                        keyExpr: "Id",
                        columnAutoWidth: true,
                        showBorders: true,
                        selection: {
                            mode: "single",
                            allowSelectAll: false
                        },
                        columns: [
                            {
                                dataField: "BidderName",
                                allowEditing: false
                            },
                            {
                                dataField: "Price",
                                allowEditing: false
                            },
                            {
                                dataField: "IsWinningPrice",
                                caption: "Winner",
                                allowEditing: false
                            },
                        ],
                        onSelectionChanged: function (e) { // Handler of the "selectionChanged" event
                            var currentSelectedRowKeys = e.currentSelectedRowKeys;
                            var currentDeselectedRowKeys = e.currentDeselectedRowKeys;
                            var allSelectedRowKeys = e.selectedRowKeys;
                            const winner = e.selectedRowsData[0];
                            $.ajax({
                                url: "/Home/SelectWinnerBid",
                                type: "POST",
                                data: JSON.stringify({ Id: winner.Id, tenderId: id, stockId: winner.StockId }),
                                contentType: 'application/json; charset=utf-8',
                            });
                            grid.refresh();
                        }
                    }).appendTo(container);
            }
        }
    }).dxDataGrid("instance");
}

async function LoadTenderCars(id) {
    let userPromis = $.Deferred();
    let usersDataSource = new DevExpress.data.DataSource({
        id: "dataGrid",
        key: ["TenderStockId", "IdBid"],
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
            var url = window.location.pathname;
            var id = url.substring(url.lastIndexOf('/') + 1);
            $.ajax({
                url: "/Home/AddBid",
                type: "POST",
                data: JSON.stringify({ TenderStockId: key.TenderStockId, Price: values.BidPrice, TenderId: id }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
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