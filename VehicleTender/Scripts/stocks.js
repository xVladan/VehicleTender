﻿$(document).ready(() => {

    insertDataIntoTable();
});


function insertDataIntoTable() {
    var stock = new DevExpress.data.DataSource({
        key: 'Id',
        load: function () {
            var d = new $.Deferred();

            $.ajax({
                type: "GET",
                url: "/Stock/StockEntries",
                contentType: "application/json; charset=utf-8",
                data: "{}",
                dataType: "json",
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
                url: "/Stock/CreateStock",
                type: "POST",
                data: JSON.stringify({ stockData: values }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
            window.location.reload()
        },
        update: function (key, values) {
            let stockArray = stock.items();
            let editedStock = stockArray.find(item => item.Id === key)
            editedStock = {
                ...editedStock,
                ModelLineId: values.ModelLineId ? values.ModelLineId : editedStock.ModelLineId,
                Mileage: values.Mileage ? values.Mileage : editedStock.Mileage,
                Price: values.Price ? values.Price : editedStock.Price,
                Comments: values.Comments ? values.Comments : editedStock.Comments,
                LocationId: values.LocationId ? values.LocationId : editedStock.LocationId,
                RegNo: values.RegNo ? values.RegNo : editedStock.RegNo,
                IsSold: values.IsSold ? values.IsSold : editedStock.IsSold,
                Year: values.Year ? values.Year : editedStock.Year,
            }
            $.ajax({
                url: "/Stock/EditStock",
                type: "POST",
                data: JSON.stringify(editedStock),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
            window.location.reload()
        },
        remove: function (key) {
            $.ajax({
                url: "/Stock/DeleteStock",
                type: "POST",
                data: JSON.stringify({ Id: key }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
            window.location.reload()
        }
    });
    function dropDownData() {
        let d = new $.Deferred();
        const lookupCarModelSource = {
            store: new DevExpress.data.CustomStore({
                key: "id",
                loadMode: "raw",
                load: function () {
                    return $.ajax({
                        url: "/Stock/CarModelDropdown",
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
        return lookupCarModelSource;
    };

    function dropDownLocationData() {
        let d = new $.Deferred();
        const lookupLocationSource = {
            store: new DevExpress.data.CustomStore({
                key: "id",
                loadMode: "raw",
                load: function () {
                    return $.ajax({
                        url: "/Location/LocationDropdown",
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
    console.log(dropDownLocationData());

    $("#dataGrid").dxDataGrid({
        dataSource: stock,
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
            allowDeleting: true,
            allowAdding: true,
            popup: {
                title: "Stock Info",
                showTitle: true,
                width: 700,
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
                                dataField: "ModelLineId",
                                colSpan: 2,
                                validationRules: [{ type: 'required' }]
                            },
                            {
                                dataField: "Year",
                                colSpan: 2,
                                validationRules: [{ type: 'required' }]
                            },
                            {
                                dataField: "Mileage",
                                colSpan: 2,
                                validationRules: [{ type: 'required' }]
                            },
                            {
                                dataField: "Price",
                                colSpan: 2,
                                validationRules: [{ type: 'required' }]
                            },
                            {
                                dataField: "LocationId",
                                colSpan: 2,
                                validationRules: [{ type: 'required' }]
                            },
                            {
                                dataField: "RegNo",
                                colSpan: 2,
                                validationRules: [{ type: 'required' }]
                            },
                            {
                                datafield: "IsSold",
                                colSpan: 2,
                                validationRules: [{ type: 'required' }]
                            },
                            {
                                datafield: "SaledDate",
                                colSpan: 2,
                                validationRules: [{ type: 'required' }]
                            },
                            {
                                dataField: "Comments",
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
                width: "6%",
                dataType: "text",
                sortIndex: 1,
                sortOrder: "asc",
                /*visible: false,*/
                allowEditing: false,
                formItem: {
                    visible: false
                }
            },
            {
                dataField: "ModelLineId",
                caption: "Model",
                lookup:
                {
                    dataSource: dropDownData(),
                    valueExpr: "id",
                    displayExpr: "text"
                },
                width:"20%"
            },
            {
                dataField: "Year",
                dataType: "text",
                caption: "Year",
                width: "5%",
            },
            {
                dataField: "Mileage",
                width: "8%",
                dataType: "text",
            },
            {
                dataField: "Price",
                dataType: "text",
                caption: "Price",
                width: "8%",
            },
            {
                dataField: "RegNo",
                caption: "Registration Number",
                witdh: "8%",
            },
            {
                dataField: "LocationId",
                caption: "Location",
                lookup:
                {
                    dataSource: dropDownLocationData(),
                    valueExpr: 'id',
                    displayExpr: 'text'
                }
            },
            {
                dataField: "Comments",
            },
            
            //{
            //    dataField: "SaledDate",
            //    caption: "Saled Date",
            //    dataType: "DateTime",
            /*},*/
            //{
            //    dataField: "IsSold",
            //    caption: "Sold",
            //    dataType: "boolean",
            //    visible: true,
            //    width: "5%",
            //},
        ],
    });
}