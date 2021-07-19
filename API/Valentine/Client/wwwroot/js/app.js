var map;

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
        ShowHideAppKeyModal(true);

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
