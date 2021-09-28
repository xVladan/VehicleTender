$(document).ready(() => {
    loadData();
});

function loadData() {
    if ($("#tenderInfo").length > 0) {
        LoadTenderData();
    } else {
        LoadAdminTenderData();
    }
}
function LoadAdminTenderData() {
}

function LoadTenderData() {
}