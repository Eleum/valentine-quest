function InitializeMapBox(boundsNE, boundsSW) {
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
        ShowHideAppKeyModal(true);

        AddFeatureHeart(map);
        AddSources(map);
        AddLayers(map);
    });
}

function AddSources(map) {
    AddSourceAreas(map);
    
}

function AddLayers(map) {
    AddLayersAreas(map);
    
}
