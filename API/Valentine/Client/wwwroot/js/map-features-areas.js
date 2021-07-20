function AddSourceAreas(map) {
    map.addSource('areas', {
        'type': 'geojson',
        'data': null
    });
}

function AddLayersAreas(map) {
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
}