﻿function InitializeMapBox(boundsNE, boundsSW) {
    mapboxgl.accessToken = '';
    map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/streets-v9',
        center: [27.563054951207278, 53.901454446609534],
        zoom: 10.90,
        minZoom: 10,
        maxBounds: [boundsNE, boundsSW],
        attributionControl: false
    });

    map.on('load', async () => {
        TriggerAppKeyModal(true);

        AddFeatureHeart(map);
        AddSources(map);
        AddLayersData(map);
        AddLayersAnimation(map);
    });

    map.on('mouseenter', 'areas', () => {
        map.getCanvas().style.cursor = 'pointer';
    });

    map.on('mouseleave', 'areas', () => {
        map.getCanvas().style.cursor = '';
    });
}

function AddSources(map) {
    AddSourceAreas(map);
    
}

function AddLayersData(map) {
    AddLayersAreas(map);
    
}
