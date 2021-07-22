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

    map.on('click', 'areas', e => {
        const point = {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: []
            }
        };
        point.geometry.coordinates.push(e.lngLat.lng, e.lngLat.lat);

        const selectedArea = map.getSource('areas')._data.features.find(polygon => {
            return turf.inside(point, polygon);
        });

        const center = turf.centerOfMass(selectedArea);
        const coordinates = center.geometry.coordinates;
        map.flyTo({
            center: coordinates,
            essential: true // this animation is considered essential with respect to prefers-reduced-motion
        });
    });
}

function AddSources(map) {
    AddSourceAreas(map);
    
}

function AddLayersData(map) {
    AddLayersAreas(map);
    
}

