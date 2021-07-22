function GenerateRandomMapLayout() {
    const areas = GenerateAreas(
        geoJsonHeartPoints,
        geoJsonHeartPolygon
    );

    this.map.getSource('areas').setData(areas);
}

function GenerateAreas(heartPoints, heartPolygon) {
    const extremums = GetFeatureCollectionExtremums(heartPoints);
    const innerPoints = GenerateRandomInnerFeatureCollectionPoints(heartPolygon, extremums);

    const voronoiPolygonsData = {
        type: 'FeatureCollection',
        features: heartPoints.features.concat(innerPoints)
    };

    const voronoiPolygons = turf.voronoi(voronoiPolygonsData, { bbox: null });
    RunAreasGeneration(heartPolygon.features[0], voronoiPolygons);

    return voronoiPolygons;
}

function GetFeatureCollectionExtremums(featureCollection) {
    const flattenLng = featureCollection.features.reduce((one, other) => one.concat(other.geometry.coordinates[0]), []);
    const flattenLat = featureCollection.features.reduce((one, other) => one.concat(other.geometry.coordinates[1]), []);

    const extremums = {
        LngMin: null,
        LngMax: null,
        LatMin: null,
        LatMax: null
    }

    flattenLng.sort((left, right) => left - right);
    extremums.LngMin = flattenLng[0];
    extremums.LngMax = flattenLng[flattenLng.length - 1];

    flattenLat.sort((left, right) => left - right);
    extremums.LatMin = flattenLat[0];
    extremums.LatMax = flattenLat[flattenLat.length - 1];

    return extremums;
}

function GenerateRandomInnerFeatureCollectionPoints(heartPolygon, extremums) {
    const innerPoints = [];
    const polygon = heartPolygon.features[0];

    function generatePoint() {
        const lng = parseFloat((Math.random() * (extremums.LngMax - extremums.LngMin) + extremums.LngMin).toFixed(15));
        const lat = parseFloat((Math.random() * (extremums.LatMax - extremums.LatMin) + extremums.LatMin).toFixed(15));

        const point = {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: []
            }
        };
        point.geometry.coordinates.push(lng, lat);

        const isInside = turf.inside(point, polygon);

        if (isInside) {
            return point;
        } else {
            return generatePoint();
        }
    }

    for (let i = 0; i < 3; i++) {
        innerPoints.push(generatePoint());
    }

    return innerPoints;
}

function RunAreasGeneration(heartPolygon, voronoiPolygons) {
    for (let i = 0; i < voronoiPolygons.features.length; i++) {
        voronoiPolygons.features[i] = turf.intersect(voronoiPolygons.features[i], heartPolygon);
        voronoiPolygons.features[i].properties.id = generateUUID();
        voronoiPolygons.features[i].properties.completion = 0; //~~(Math.random() * 10) * 10;
    }
}

const generateUUID = () => {
    let
        d = new Date().getTime(),
        d2 = (performance && performance.now && (performance.now() * 1000)) || 0;
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, c => {
        let r = Math.random() * 16;
        if (d > 0) {
            r = (d + r) % 16 | 0;
            d = Math.floor(d / 16);
        } else {
            r = (d2 + r) % 16 | 0;
            d2 = Math.floor(d2 / 16);
        }
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
    });
};