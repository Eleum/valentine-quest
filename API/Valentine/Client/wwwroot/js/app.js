var map;

function InitializeControls(boundsNE, boundsSW) {
    debugger;
    InitializeMapBox(boundsNE, boundsSW);
}

function SetMapAreasData(areas) {
    console.log(areas);
    map.getSource('areas').setData(areas);
}

async function ReturnToAppKeyModalAsync() {
    const shownPromise = new Promise((resolve, reject) => {
        $("#app-key-modal").on('shown.bs.modal', (event) => {
            resolve();
        });
    });
    $("#app-map-modal-close").click();

    await shownPromise;
}

function ShowHideAppKeyModal(show) {
    if (show) {
        $('#app-key-modal').modal('show')
        return;
    }

    $('#app-key-modal').modal('hide')
}
