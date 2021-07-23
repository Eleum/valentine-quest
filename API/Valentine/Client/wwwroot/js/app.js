var map;

function InitializeControls(boundsNE, boundsSW) {
    InitializeMapBox(boundsNE, boundsSW);
}

function SetMapAreasData(areas) {
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

function TriggerAppKeyModal(show) {
    setTimeout(() => {
        if (show) {
            $('#app-key-modal').modal('show')
            return;
        }

        $('#app-key-modal').modal('hide')
    }, 0);
}

function TriggerToastr(elementId, show) {
    setTimeout(() => {
        if (show) {
            $(elementId).toast('show');
            return;
        }
        $(elementId).toast('hide');
    }, 0);
}

function ShowLayoutToast() {
    setTimeout(() => {
        $('#area-generator').toast('show');
    }, 0);
}

function ShowNavigatorToast() {
    setTimeout(() => {
        $('#area-navigator').toast('show');
    }, 0);
}
