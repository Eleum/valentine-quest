var map;

function InitializeControls(longitude, latitude) {
    InitializeMapBox(longitude, latitude)
    InitializeModals();
}

function InitializeModals() {
    $('#app-map-form').submit(event => {
        event.preventDefault();
        event.stopPropagation();

        $('#app-map-title-group').removeClass('was-validated');
        $('#app-map-title-group').addClass('was-validated');

        if ($('#app-map-form')[0].checkValidity() === false) {
            return;
        }

        this.addNewMap(
            $('#app-map-title').val(),
            $.trim($('#app-map-description').val()),
            $('#app-map-default')[0].checked
        );
    });

    $('#app-map-title').keydown(event => {
        if (event.which === 32 && $.trim($('#app-map-title').val()) === '') {
            event.preventDefault();
        }
    });
}

function InitializeMapBox(longitude, latitude) {
    mapboxgl.accessToken = 'pk.eyJ1IjoibmV2ZXJlbmQxbmciLCJhIjoiY2tyOW43aXl3MTkweDJwbHA1NzlwYjRmaiJ9.hl5K_rQCEC81k1Fp3RjXGg';
    map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/streets-v9',
        center: [27.563054951207278, 53.901454446609534],
        zoom: 10.90,
        minZoom: 10,
        maxBounds: [longitude, latitude],
        attributionControl: false
    });

    map.on('load', async () => {
        $("#welcome-modal-toggler").click();
        /*ShowHideAppKeyModal(true);*/

        map.addSource('areas', {
            'type': 'geojson',
            'data': null
        });

        map.addLayer({
            'id': 'areas',
            'type': 'fill',
            'source': 'areas',
            'paint': {
                'fill-color': [
                    'let',
                    'completionPercentage',
                    ['get', 'completion'],
                    [
                        'interpolate',
                        ['linear'],
                        ['var', 'completionPercentage'],
                        0, ['to-color', '#ffebeb'],
                        100, ['to-color', '#ff3334']
                    ]
                ],
                'fill-opacity': [
                    'let',
                    'completionPercentage',
                    ['get', 'completion'],
                    [
                        'interpolate',
                        ['linear'],
                        ['var', 'completionPercentage'],
                        0, 0.3,
                        100, 1
                    ]
                ]
            }
        });
        map.addLayer({
            'id': 'areas-lines',
            'type': 'line',
            'source': 'areas',
            'layout': {},
            'paint': {
                'line-width': 1
            }
        });
    });
}

function SetMapAreasData(areas) {
    console.log(areas);
    map.getSource('areas').setData(areas);
}

function ShowHideAppKeyModal(show) {
    if (show) {
        $('#app-key-modal').modal('show')
        return;
    }

    $('#app-key-modal').modal('hide')
}
