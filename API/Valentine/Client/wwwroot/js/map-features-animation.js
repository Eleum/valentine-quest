async function AddLayersAnimation(map) {
    const heartPoints = map.getSource('heart-points');
    const lineFeatures = {
        type: 'FeatureCollection',
        features: []
    };

    map.addLayer({
        id: 'line-animation',
        type: 'line',
        source: {
            type: 'geojson',
            data: lineFeatures
        },
        layout: {
            'line-cap': 'round',
            'line-join': 'round'
        },
        paint: {
            'line-color': '#ff3334',
            'line-width': 6
        }
    });

    for (let i = 0, j = i + 1; i < heartPoints._data.features.length; i++, j++) {
        if (j === heartPoints._data.features.length) {
            j = 0;
        }

        const emptyFeature = {
            type: 'Feature',
            geometry: {
                type: 'LineString',
                coordinates: []
            }
        };

        lineFeatures.features.push(emptyFeature);

        await drawConnectionLine(
            lineFeatures,
            heartPoints._data.features[i].geometry.coordinates,
            heartPoints._data.features[j].geometry.coordinates,
            i
        );
    }
}

function drawConnectionLine(features, start, end, iteration) {
    const speedFactor = 10;

    const startPoint = [start[0], start[1]];
    const endPoint = [end[0], end[1]];

    const diffX = endPoint[0] - startPoint[0];
    const diffY = endPoint[1] - startPoint[1];

    const speedFactorX = diffX / speedFactor;
    const speedFactorY = diffY / speedFactor;

    let i = 0;
    let j = 0;

    const lineCoordinates = [];

    if (diffX > 0) {
        while (i < diffX || Math.abs(j) < Math.abs(diffY)) {
            lineCoordinates.push([startPoint[0] + i, startPoint[1] + j]);

            if (i < diffX) {
                i += speedFactorX;
            }

            if (Math.abs(j) < Math.abs(diffY)) {
                j += speedFactorY;
            }
        }
    } else {
        while (i > diffX || Math.abs(j) < Math.abs(diffY)) {
            lineCoordinates.push([startPoint[0] + i, startPoint[1] + j]);

            if (i > diffX) {
                i += speedFactorX;
            }

            if (Math.abs(j) < Math.abs(diffY)) {
                j += speedFactorY;
            }
        }
    }

    // to complete the line
    lineCoordinates.push([startPoint[0] + diffX, startPoint[1] + diffY]);

    let animationIteration = 0;

    return new Promise((resolve, reject) => {
        function step() {
            if (animationIteration < lineCoordinates.length) {
                features.features[iteration].geometry.coordinates.push(lineCoordinates[animationIteration]);
                map.getSource('line-animation').setData(features);

                requestAnimationFrame(step);
                animationIteration++;
            } else {
                resolve(animationIteration);
            }
        }
        requestAnimationFrame(step);
    });
}