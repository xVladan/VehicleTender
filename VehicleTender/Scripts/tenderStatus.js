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
                url: "/TenderStatus/TenderStatusEntries",
                contentType: "application/json; charset=utf-8",
                data: "{}",
                dataType: "json",
                success: (data) => {
                    console.log(data);
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
                url: "TenderStatus/AddTenderStatus",
                type: "POST",
                data: JSON.stringify({ tenderStatusData: values }),
                /*dataType: 'json',*/
                contentType: 'application/json; charset=utf-8'
            });
            window.location.reload()

        },
        update: function (key, values) {
            let a = allData.find(item => item.Id == key);
            let editedItem = {
                ...a,
                Type: values.Type ? values.Type : a.Type
            };
            console.log(editedItem);

            $.ajax({
                url: "/TenderStatus/EditTenderStatusInDB",
                type: "POST",
                data: JSON.stringify({ Id: key, tenderStatus: editedItem }),
                /* dataType: 'json',*/
                contentType: 'application/json; charset=utf-8',
            });
            console.log(editedItem);
            window.location.reload()
        },
        remove: function (key) {
            $.ajax({
                url: "/TenderStatus/DeleteTenderStatusInDB",
                type: "POST",
                data: JSON.stringify({ Id: key }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            });
            window.location.reload()
        }
    });

    $("#tenderstatusGrid").dxDataGrid({
        dataSource: mf,
        showBorders: true,
        editing: {
            refreshMode: "full",
            mode: "popup",
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            popup: {
                title: "Tender Status",
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
                                dataField: "Id",
                                colSpan: 1,
                            },
                            {
                                dataField: "Type",
                                colSpan: 2,
                                validationRules: [{ type: 'required' }]
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
                sortIndex: 1,
                sortOrder: "asc",
                /*visible: false,*/
                allowEditing: false,
                formItem: {
                    visible: false
                }
            },
            {
                dataField: "Type",
                width: "40%",
            }
        ],
    });
}