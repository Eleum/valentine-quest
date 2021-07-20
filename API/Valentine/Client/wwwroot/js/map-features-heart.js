function AddFeatureHeart(map) {
    AddPulsingDot(map);
    AddSourceHeart(map);
    AddLayersHeart(map);
}

function AddSourceHeart(map) {
    const geoJsonHeartPoints = {
        'type': 'FeatureCollection',
        'features': [
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [27.47307399170134, 53.95398000106455]
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [27.52078943603584, 53.95132860970111]
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [27.556421840925708, 53.94088200384468]
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [27.596762012691784, 53.97262788574715]
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [27.637796122302973, 53.969194592551294]
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [27.704922508801815, 53.93044212805984]
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [27.673934377188004, 53.859921373043164]
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [27.574525802199076, 53.83548617934562]
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [27.46877178095783, 53.852118710423005]
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [27.410390949657664, 53.911338937344766]
                }
            },
        ]
    };
    map.addSource('heart-points', {
        'type': 'geojson',
        'data': geoJsonHeartPoints
    });

    const geoJsonHeartPolygon = {
        'type': 'FeatureCollection',
        'features': [
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Polygon',
                    'coordinates': [
                        [
                            [27.47307399170134, 53.95398000106455],
                            [27.52078943603584, 53.95132860970111],
                            [27.556421840925708, 53.94088200384468],
                            [27.596762012691784, 53.97262788574715],
                            [27.637796122302973, 53.969194592551294],
                            [27.704922508801815, 53.93044212805984],
                            [27.673934377188004, 53.859921373043164],
                            [27.574525802199076, 53.83548617934562],
                            [27.46877178095783, 53.852118710423005],
                            [27.410390949657664, 53.911338937344766],
                            [27.47307399170134, 53.95398000106455]
                        ]
                    ]
                }
            },
        ]
    };
    map.addSource('polygon-area', {
        'type': 'geojson',
        'data': geoJsonHeartPolygon
    });    
}

function AddLayersHeart(map) {
    map.addLayer({
        id: 'heart-points',
        type: 'symbol',
        source: 'heart-points',
        layout: {
            'icon-image': 'pulsing-dot',
            'icon-allow-overlap': true,
            'text-allow-overlap': true
        }
    });
}

function AddPulsingDot(map) {
    const size = 150;
    const pulsingDot = {
        'width': size,
        'height': size,
        'data': new Uint8Array(size * size * 4),

        // get rendering context for the map canvas when layer is added to the map
        onAdd: function () {
            const canvas = document.createElement('canvas');
            canvas.width = this.width;
            canvas.height = this.height;
            this.context = canvas.getContext('2d');
        },

        // called once before every frame where the icon will be used
        render: function () {
            const duration = 1000;
            const t = (performance.now() % duration) / duration;

            const radius = (size / 2) * 0.3;
            const outerRadius = (size / 2) * 0.4 * t + radius;
            const context = this.context;

            // draw outer circle
            context.clearRect(0, 0, this.width, this.height);
            context.beginPath();
            context.arc(
                this.width / 2,
                this.height / 2,
                outerRadius,
                0,
                Math.PI * 2
            );
            context.fillStyle = 'rgba(255, 200, 200,' + (1 - t) + ')';
            context.fill();

            // draw inner circle
            context.beginPath();
            context.arc(
                this.width / 2,
                this.height / 2,
                radius,
                0,
                Math.PI * 2
            );
            context.fillStyle = 'rgba(255, 100, 100, 1)';
            context.strokeStyle = 'white';
            context.lineWidth = 2 + 4 * (1 - t);
            context.fill();
            context.stroke();

            // update this image's data with data from the canvas
            this.data = context.getImageData(
                0,
                0,
                this.width,
                this.height
            ).data;

            // continuously repaint the map, resulting in the smooth animation of the dot
            map.triggerRepaint();

            // return `true` to let the map know that the image was updated
            return true;
        }
    };
    map.addImage('pulsing-dot', pulsingDot, { 'pixelRatio': 2 });
}