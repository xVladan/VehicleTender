$(document).ready(() => {

    insertDataIntoTable();
});


function insertDataIntoTable() {
    var car = new DevExpress.data.DataSource({
        key: 'Id',
        load: function () {
            var d = new $.Deferred();

            $.ajax({
                type: "GET",
                url: "/Stock/CarEntries",
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
                url: "/Stock/CreateCar",
                type: "POST",
                data: JSON.stringify({ carData: values }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
        },
        update: function (key, values) {
            let carArray = car.items();
            let editedCar = carArray.find(item => item.Id === key)
            editedCar = {
                ...editedCar,
                ModelName: values.ModelName ? values.ModelName : editedCar.ModelName,
                ModelNo: values.ModelNo ? values.ModelNo : editedCar.ModelNo,
                ManufacturerId: values.ManufacturerId ? values.ManufacturerId : editedCar.ManufacturerId
            }
            $.ajax({
                url: "/Stock/EditCar",
                type: "POST",
                data: JSON.stringify(editedCar),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
        },
        remove: function (key) {
            $.ajax({
                url: "/Stock/DeleteCar",
                type: "POST",
                data: JSON.stringify({ Id: key }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
        }
    });
    function dropDownData() {
        let d = new $.Deferred();
        const lookupManufacturerSource = {
            store: new DevExpress.data.CustomStore({
                key: "id",
                loadMode: "raw",
                load: function () {
                    return $.ajax({
                        url: "/Stock/ManufacturerDropdown",
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
        return lookupManufacturerSource;
    }

    $("#dataGrid").dxDataGrid({
        dataSource: car,
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
                title: "Car Info",
                showTitle: true,
                width: 500,
                height: 525,
            },
            form: {
                items: [
                    {
                        itemType: "group",
                        colCount: 1,
                        colSpan: 2,
                        items: [
                            {
                                dataField: "ModelName",
                                colSpan: 2,
                            },
                            {
                                dataField: "ModelNo",
                                colSpan: 2,
                            },
                            {
                                dataField: "ManufacturerId",
                                colSpan: 2,
                            }]
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
                /*visible: false,*/
                allowEditing: false,
                formItem: {
                    visible: false
                }
            },
            {
                dataField: "ModelName",
                width: "20%",
            },
            {
                dataField: "ModelNo",
                caption: "Model Number",
            },
            {
                dataField: "ManufacturerId",
                caption: "Manufacturer",
                lookup:
                {
                    dataSource: dropDownData(),
                    valueExpr: "id",
                    displayExpr: "text",

                }
            },
        ],
    });
}