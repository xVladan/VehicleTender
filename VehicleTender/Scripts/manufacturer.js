$(document).ready(() => {
    insertDataIntoTable();
});

function insertDataIntoTable() {
    var mf = new DevExpress.data.DataSource({
        key: 'Id',
        load: function (loadOptions) {
            var d = new $.Deferred();

            $.ajax({
                type: "GET",
                url: "/Stock/ManufacturerEntries",
                contentType: "application/json; charset=utf-8",
                data: "{}",
                dataType: "json",
                success: (data) => {
                    d.resolve(data);
                },
                error: (data) => {
                    d.reject(data);
                }

            });
            return d.promise();
        },
        insert: function (values) {
            $.ajax({
                url: "/Stock/AddManufacturer",
                type: "POST",
                data: JSON.stringify({ manufacturerData: values }),
                contentType: 'application/json; charset=utf-8'
            });
        },
        update: function (key, values) {
            $.ajax({
                url: "/Stock/EditManufacturer",
                type: "POST",
                data: JSON.stringify({ Id: key, MFName: values.ManufacturerName }),
                contentType: 'application/json; charset=utf-8',
            });
        },
        remove: function (key) {
            $.ajax({
                url: "/Stock/DeleteManufacturer",
                type: "POST",
                data: JSON.stringify({ Id: key }),
                contentType: 'application/json; charset=utf-8',
            });
        }
    });

    $("#manufacturerGrid").dxDataGrid({
        dataSource: mf,
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
            refreshMode: "full",
            mode: "popup",
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            popup: {
                title: "Manufacturer Info",
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
                                dataField: "ManufacturerName",
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
                dataField: "ManufacturerName",
                width: "80%",
            },
        ],
    });
}