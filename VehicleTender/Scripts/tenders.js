$(document).ready(() => {

    insertDataIntoTable();
});


function insertDataIntoTable() {
    var tender = new DevExpress.data.DataSource({
        key: 'Id',
        load: function () {
            var d = new $.Deferred();

            $.ajax({
                type: "GET",
                url: "/Tender/TenderEntries",
                contentType: "application/json; charset=utf-8",
                data: "{}",
                success: (data) => {
                    d.resolve(data)
                },
                error: (data) => {
                    d.reject(data);
                }
            });
            return d.promise();
        },
        insert: function (values) {
            $.ajax({
                url: "/Tender/CreateTender",
                type: "POST",
                data: JSON.stringify({
                    tender: values
                }),
                contentType: 'application/json; charset=utf-8',
            });
        },
        update: function (key, values) {
            let tenderArray = tender.items();
            let editedTender = tenderArray.find(item => item.Id === key)
            editedTender = {
                ...editedTender,
                CreatedDate: values.CreatedDate ? values.CreatedDate : editedTender.CreatedDate,
                UserId: values.UserId ? values.UserId : editedTender.UserId,
                TenderNo: values.TenderNo ? values.TenderNo : editedTender.TenderNo,
                OpenDate: values.OpenDate ? values.OpenDate : editedTender.OpenDate,
                CloseDate: values.CloseDate ? values.CloseDate : editedTender.CloseDate,
                StatusId: values.StatusId ? values.StatusId : editedTender.StatusId,
                TenderStockId: values.TenderStockId ? values.TenderStockId : editedTender.TenderStockId,
                TenderUserId: values.TenderUserId ? values.TenderUserId : editedTender.TenderUserId,
            }
            $.ajax({
                url: "/Tender/EditTender",
                type: "POST",
                data: JSON.stringify(editedTender),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
        },

    });

    function dropDownTenderStatusData() {
        let d = new $.Deferred();
        const lookupLocationSource = {
            store: new DevExpress.data.CustomStore({
                key: "Id",
                loadMode: "raw",
                load: function () {
                    return $.ajax({
                        url: "/Tender/TenderStatusDropdown",
                        type: "GET",
                        data: "{}",
                        contentType: 'application/json; charset=utf-8',
                        success: (data) => {
                            d.resolve(data);
                        },
                        error: (data) => {
                            d.reject(data);
                        }
                    });
                }
            }),
            sort: "text"
        }
        return lookupLocationSource;
    };

    function dropDownAdminsData() {
        let d = new $.Deferred();
        const lookupAdminSource = {
            store: new DevExpress.data.CustomStore({
                key: "Id",
                loadMode: "raw",
                load: function () {
                    return $.ajax({
                        url: "/User/GetAdmins",
                        type: "GET",
                        data: "{}",
                        contentType: 'application/json; charset=utf-8',
                        success: (data) => {
                            d.resolve(data);
                        },
                        error: (data) => {
                            d.reject(data);
                        }
                    });
                }
            }),
            sort: "Email"
        }
        return lookupAdminSource;
    }

    function dropDownUserData() {
        let d = new $.Deferred();
        const lookupUserSource = {
            store: new DevExpress.data.CustomStore({
                key: "Id",
                loadMode: "raw",
                load: function () {
                    return $.ajax({
                        url: "/User/UsersDropdown",
                        type: "GET",
                        data: "{}",
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: (data) => {
                            d.resolve(data);
                        },
                        error: (data) => {
                            d.reject(data);
                        }
                    });
                }
            }),
            sort: "Email"
        }
        return lookupUserSource;
    }

    function dropDownStockData() {
        let d = new $.Deferred();
        const lookupStockSource = {
            store: new DevExpress.data.CustomStore({
                key: "Id",
                loadMode: "raw",
                load: function () {
                    return $.ajax({
                        url: "/Stock/StockDropdown",
                        type: "GET",
                        data: "{}",
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: (data) => {
                            d.resolve(data);
                        },
                        error: (data) => {
                            d.reject(data);
                        }
                    });
                }
            }),
            sort: "text"
        }
        return lookupStockSource;
    };

    $("#tenderGrid").dxDataGrid({
        dataSource: tender,
        showBorders: true,
        paging: {
            pageSize: 10
        },
        pager: {
            visible: true,
            allowedPageSizes: [10, 15, 50, 100],
            showPageSizeSelector: true,
            showInfo: true,
            showNavigationButtons: true
        },
        editing: {
            mode: "popup",
            allowUpdating: true,
            allowDeleting: false,
            allowAdding: true,
            popup: {
                title: "Tender Info",
                showTitle: true,
                width: 1000,
                height: 525,
            },
            form: {
                items: [
                    {
                        itemType: "group",
                        colCount: 2,
                        colSpan: 2,
                        items: [
                            {
                                dataField: "CreatedDate",
                                colSpan: 2,
                                dataType: "date"
                            },
                            {
                                dataField: "UserId",
                                colSpan: 2,
                            },
                            {
                                dataField: "TenderNo",
                                colSpan: 2,
                            },
                            {
                                dataField: "TenderUserId",
                                colSpan: 2,
                            },
                            {
                                dataField: "TenderStockId",
                                colSpan: 2,
                            },
                            {
                                dataField: "OpenDate",
                                colSpan: 2,
                            },
                            {
                                dataField: "CloseDate",
                                colSpan: 2,
                            },
                            {
                                dataField: "StatusId",
                                colSpan: 2,
                            }

                        ]
                    },
                ]
            },
        },
        columns: [
            {
                dataField: "Id",
                caption: "Number",
                width: "10%",
                dataType: "text",
                visible: false,
                allowEditing: false,
                formItem: {
                    visible: false
                }
            },
            {
                dataField: "CreatedDate",
                caption: "Created Date",
                dataType: "date"
            },
            {
                dataField: "UserId",
                caption: "Created By",
                lookup:
                {
                    dataSource: dropDownAdminsData(),
                    valueExpr: "Id",
                    displayExpr: "FullName"
                }
            },
            {
                dataField: "TenderNo",
                caption: "Tender Number",
            },
            {
                dataField: "TenderUserId",
                visible: false,
                caption: "Invite Bidders",
                lookup: {
                    dataSource: dropDownUserData(),
                    valueExpr: "Id",
                    displayExpr: "Email"
                }
            },
            {
                dataField: "TenderStockId",
                visible: false,
                caption: "Attach Stock",
                lookup: {
                    dataSource: dropDownStockData(),
                    valueExpr: "Id",
                    displayExpr: "FullCarName"
                }
            },
            {
                dataField: "OpenDate",
                caption: "Open Date",
                dataType: "date"
            },
            {
                dataField: "CloseDate",
                caption: "Close Date",
                dataType: "date"
            },
            {
                dataField: "StatusId",
                caption: "Status",
                dataType: "string",
                lookup:
                {
                    dataSource: dropDownTenderStatusData(),
                    valueExpr: "Id",
                    displayExpr: "Type"
                }

            }
        ],
        onEditorPreparing: (e) => {
            if (e.dataField == 'TenderStockId' && e.parentType == 'dataRow') {
                e.editorName = 'dxDropDownBox';
                e.editorOptions.dropDownOption = {
                    height: 500
                };
                e.editorOptions.contentTemplate = (args, container) => {
                    var value = args.component.option("value"),
                        $dataGrid = $("<div>").dxDataGrid({
                            width: '100%',
                            dataSource: args.component.option("dataSource"),
                            keyExpr: "Id",
                            columns: ["CarModel", "Manufacturer", "ModelNo", "Year", "Price", "Mileage", "Comments", "RegNo", "Location"],
                            hoverStateEnabled: true,
                            paging: { enabled: true, pageSize: 10 },
                            filterRow: { visible: true },
                            scrolling: { mode: "infinite" },
                            height: '100%',
                            width: '100%',
                            showRowLines: true,
                            showBorders: true,
                            selection: { mode: "multiple" },
                            selectedRowKeys: value,
                            onSelectionChanged: function (selectedItems) {
                                var keys = selectedItems.selectedRowKeys;
                                args.component.option("value", keys);
                            }
                        });
                    var dataGrid = $dataGrid.dxDataGrid("instance");

                    args.component.on("valueChanged", function (args) {
                        var value = args.value;

                        dataGrid.selectRows(value, true);
                    });
                    container.append($dataGrid);
                    $("<div>").dxButton({
                        text: "Close",

                        onClick: function (ev) {
                            args.component.close();
                        }
                    }).css({ float: "right", marginTop: "10px" }).appendTo(container);
                    return container;
                }
            };

            if (e.dataField == 'TenderUserId' && e.parentType == 'dataRow') {
                e.editorName = 'dxDropDownBox';
                e.editorOptions.dropDownOption = {
                    height: 500
                };
                e.editorOptions.contentTemplate = (args, container) => {

                    var value = args.component.option("value"),
                        $dataGrid = $("<div>").dxDataGrid({
                            width: '100%',
                            dataSource: args.component.option("dataSource"),
                            keyExpr: "Id",
                            columns: ["Email"],
                            hoverStateEnabled: true,
                            paging: { enabled: true, pageSize: 10 },
                            filterRow: { visible: true },
                            scrolling: { mode: "infinite" },
                            height: '90%',
                            width: '100%',
                            showRowLines: true,
                            showBorders: true,
                            selection: { mode: "multiple" },
                            selectedRowKeys: value,
                            value: "onClick",
                            onSelectionChanged: function (selectedItems) {
                                var keys = selectedItems.selectedRowKeys;
                                args.component.option("value", keys);
                            }
                        });
                    var dataGrid = $dataGrid.dxDataGrid("instance");

                    args.component.on("valueChanged", function (args) {
                        var value = args.value;

                        dataGrid.selectRows(value, false);
                    });
                    container.append($dataGrid);
                    $("<div>").dxButton({
                        text: "Close",

                        onClick: function (ev) {
                            args.component.close();
                        }
                    }).css({ float: "right", marginTop: "10px" }).appendTo(container);
                    return container;
                }
            }

        }
    });
}