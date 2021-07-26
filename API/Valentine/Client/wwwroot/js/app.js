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
    $('#app-key-modal').modal(show ? 'show' : 'hide');
}

async function TriggerToastr(elementId, show) {
    const transitionPromise = new Promise((resolve, reject) => {
        if ($(elementId).is(":hidden") && !show) {
            resolve();
            return;
        }

        $(elementId).on(show ? 'shown.bs.toast' : 'hidden.bs.toast', (event) => {
            resolve();
        });
    });

    $(elementId).toast(show ? 'show' : 'hide');
    await transitionPromise;
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
