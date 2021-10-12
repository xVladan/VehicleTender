$(document).ready(() => {
    insertDataIntoTable();
});
var allData;
function insertDataIntoTable() {
    var mf = new DevExpress.data.DataSource({
        key: 'Id',
        load: function (loadOptions) {
            var d = new $.Deferred();

            $.ajax({
                type: "GET",
                url: "/Location/LocationEntries",
                contentType: "application/json; charset=utf-8",
                data: "{}",
                dataType: "json",
                success: (data) => {
                    allData = data;
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
                url: "/Location/AddLocation",
                type: "POST",
                data: JSON.stringify({ locationData: values }),
                /*dataType: 'json',*/
                contentType: 'application/json; charset=utf-8'
            });

        },
        update: function (key, values) {
            let a = allData.find(item => item.Id == key);
            let editedItem = {
                ...a,
                City: values.City ? values.City : a.City,
                ZipCode: values.ZipCode ? values.ZipCode : a.ZipCode
            };
            $.ajax({
                url: "/Location/EditLocation",
                type: "POST",
                data: JSON.stringify({ Id: key, LocName: editedItem }),
                /* dataType: 'json',*/
                contentType: 'application/json; charset=utf-8',
            });

        },
        remove: function (key) {
            $.ajax({
                url: "/Location/DeleteLocation",
                type: "POST",
                data: JSON.stringify({ Id: key }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
        }
    });

    $("#locationGrid").dxDataGrid({
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
                title: "Location",
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
                                dataField: "City",
                                colSpan: 2,
                            },
                            {
                                dataField: "Zip Code",
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
                /*visible: false,*/
                allowEditing: false,
                formItem: {
                    visible: false
                }
            },
            {
                dataField: "City",
                width: "50%",
            },
            {
                dataField: "ZipCode",
                width: "40%",
            }
        ],
    });
}