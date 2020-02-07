$(document).ready(function () {
    var bounds = [
        [27.282656504916332, 53.8284771143484], // Southwest coordinates
        [27.844537863849382, 54.00195438196859] // Northeast coordinates
    ];

    mapboxgl.accessToken = 'pk.eyJ1IjoibmV2ZXJlbmQxbmciLCJhIjoiY2swaWR2d2Y3MGI5YjNjcW1ncGtuaXN4MCJ9.rQ4HN2r10RzPKeuO3TH06w';
    var map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/streets-v9',
        center: [27.563054951207278, 53.901454446609534],
        zoom: 10.90,
        minZoom: 10.90,
        maxBounds: bounds,
        attributionControl: false
    });

    var size = 150;
    var speedFactor = 2;

    // implementation of CustomLayerInterface to draw a pulsing dot icon on the map
    // see https://docs.mapbox.com/mapbox-gl-js/api/#customlayerinterface for more info
    var pulsingDot = {
        width: size,
        height: size,
        data: new Uint8Array(size * size * 4),

        // get rendering context for the map canvas when layer is added to the map
        onAdd: function () {
            var canvas = document.createElement('canvas');
            canvas.width = this.width;
            canvas.height = this.height;
            this.context = canvas.getContext('2d');
        },

        // called once before every frame where the icon will be used
        render: function () {
            var duration = 1000;
            var t = (performance.now() % duration) / duration;

            var radius = (size / 2) * 0.3;
            var outerRadius = (size / 2) * 0.4 * t + radius;
            var context = this.context;

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

    // Create a GeoJSON source with an empty lineString.
    var geojson = {
        "type": "FeatureCollection",
        "features": []
    };

    var innerGeoJson = {
        "type": "FeatureCollection",
        "features": []
    };

    map.on('load', async function () {
        map.addImage('pulsing-dot', pulsingDot, { pixelRatio: 2 });

        map.addSource('points', {
            'type': 'geojson',
            'data': {
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
                    // 6
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
                    // 7
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
            }
        });
        map.addSource('polygon-area', {
            'type': 'geojson',
            'data': {
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
                                    [27.410390949657664, 53.911338937344766]
                                ]
                            ]
                        }
                    },
                ]
            }
        });
        map.addLayer({
            'id': 'points',
            'type': 'symbol',
            'source': 'points',
            'layout': {
                'icon-image': 'pulsing-dot'
            }
        });
        map.addLayer({
            'id': 'line-animation',
            'type': 'line',
            'source': {
                'type': 'geojson',
                'data': geojson
            },
            'layout': {
                'line-cap': 'round',
                'line-join': 'round'
            },
            'paint': {
                'line-color': '#ff3334',
                'line-width': 6
            }
        });
        // map.addLayer({
        //     'id': 'area',
        //     'type': 'fill',
        //     'source': 'polygon-area',
        //     'layout': {},
        //     'paint': {
        //         'fill-color': '#088',
        //         'fill-opacity': 0.8
        //     }
        // });

        let points = map.getSource('points');

        let lngMin = -1;
        let lngMax = -1;
        let latMin = -1;
        let latMax = -1;

        function calculateMaxMin() {
            const flattenLng = points._data.features.reduce(function (a, b) { return a.concat(b.geometry.coordinates[0]); }, []);
            const flattenLat = points._data.features.reduce(function (a, b) { return a.concat(b.geometry.coordinates[1]); }, []);

            function numbersSortAsc(a, b) {
                return a - b;
            }

            flattenLng.sort(numbersSortAsc);
            lngMin = flattenLng[0];
            lngMax = flattenLng[flattenLng.length - 1];

            flattenLat.sort(numbersSortAsc);
            latMin = flattenLat[0];
            latMax = flattenLat[flattenLat.length - 1];
        }

        function generateRandomInnerPoints() {
            const polygon = map.getSource('polygon-area')._data.features[0];

            function generatePoint() {
                const lng = parseFloat((Math.random() * (lngMax - lngMin) + lngMin).toFixed(15));
                const lat = parseFloat((Math.random() * (latMax - latMin) + latMin).toFixed(15));

                const point = {
                    'type': 'Feature',
                    'properties': {},
                    'geometry': {
                        'type': 'Point',
                        'coordinates': []
                    }
                };
                point.geometry.coordinates.push(lng, lat);

                const isInside = turf.inside(point, polygon)

                if (isInside) {
                    return point;
                } else {
                    return generatePoint();
                }
            }

            for (var i = 0; i < 6; i++) {
                innerGeoJson.features.push(generatePoint());
            }
        }

        calculateMaxMin();
        generateRandomInnerPoints();


        // var p = turf.randomPoint(30, { bbox: [latMin, lngMin, latMax, latMin] });

        for (var i = 0; i < innerGeoJson.features.length; i++) {
            innerGeoJson.features[i].properties.z = ~~(Math.random() * 9);
        }

        var tin = turf.tin(innerGeoJson);

        tin.features.forEach(function (d) {
            innerGeoJson.features[i].properties.a = ~~(Math.random() * 9);
            innerGeoJson.features[i].properties.b = ~~(Math.random() * 9);
            innerGeoJson.features[i].properties.c = ~~(Math.random() * 9);
        });

        // add each triangle with the right color
        // tin.features.forEach(function (d) {
        //     // create a hex color code from the z values the 3 triangle vertices
        //     d.properties.fill = '#' + d.properties.a +
        //         d.properties.b + d.properties.c;
        // });

        // debugger;

        map.addSource('tin', {
            'type': 'geojson',
            'data': tin
        });
        map.addLayer({
            'id': 'tin',
            'type': 'fill',
            'source': 'tin',
            'layout': {},
            'paint': {
                // 'line-color': '#000',
                'fill-color': '#088',
                'fill-opacity': 0.8
            }
        });

        map.addSource('inner-points', {
            'type': 'geojson',
            'data': innerGeoJson
        });
        map.addLayer({
            'id': 'inner-points',
            'type': 'circle',
            'source': 'inner-points',
            'paint': {
                'circle-color': '#007cbf',
                'circle-radius': 5
            }
        });

        for (var i = 0, j = i + 1; i < points._data.features.length; i++ , j++) {
            if (j == points._data.features.length) {
                j = 0;
            }

            let emptyFeature = {
                "type": "Feature",
                "geometry": {
                    "type": "LineString",
                    "coordinates": []
                }
            };

            geojson.features.push(emptyFeature);

            await drawConnectionLine(
                points._data.features[i].geometry.coordinates,
                points._data.features[j].geometry.coordinates,
                i
            );
        }

        function drawConnectionLine(start, end, feature) {
            let startPoint = [start[0], start[1]];
            let endPoint = [end[0], end[1]];

            let diffX = endPoint[0] - startPoint[0];
            let diffY = endPoint[1] - startPoint[1];

            let sfX = diffX / speedFactor;
            let sfY = diffY / speedFactor;

            let i = 0;
            let j = 0;

            let lineCoordinates = [];

            if (diffX > 0) {
                while (i < diffX || Math.abs(j) < Math.abs(diffY)) {
                    lineCoordinates.push([startPoint[0] + i, startPoint[1] + j]);

                    if (i < diffX) {
                        i += sfX;
                    }

                    if (Math.abs(j) < Math.abs(diffY)) {
                        j += sfY;
                    }
                }
            } else {
                while (i > diffX || Math.abs(j) < Math.abs(diffY)) {
                    lineCoordinates.push([startPoint[0] + i, startPoint[1] + j]);

                    if (i > diffX) {
                        i += sfX;
                    }

                    if (Math.abs(j) < Math.abs(diffY)) {
                        j += sfY;
                    }
                }
            }

            // to complete the line
            lineCoordinates.push([startPoint[0] + diffX, startPoint[1] + diffY]);

            let animationCounter = 0;

            return new Promise((resolve, reject) => {
                function step() {
                    if (animationCounter < lineCoordinates.length) {
                        geojson.features[feature].geometry.coordinates.push(lineCoordinates[animationCounter]);
                        map.getSource('line-animation').setData(geojson);

                        requestAnimationFrame(step);
                        animationCounter++;
                    }
                    else {
                        resolve();
                    }
                }
                requestAnimationFrame(step);
            });
        }
    });

    map.on('mousemove', function (e) {
        document.getElementById('info').innerHTML =
            JSON.stringify(e.point) +
            '<br />' +
            JSON.stringify(e.lngLat.wrap()) +
            '<br />' +
            JSON.stringify(map.getZoom());
    });

    map.on('contextmenu', function (e) {
        const el = document.createElement('textarea');
        el.value = `[${e.lngLat.lng}, ${e.lngLat.lat}]`;
        el.setAttribute('readonly', '');
        el.style.position = 'absolute';
        el.style.left = '-9999px';
        document.body.appendChild(el);
        const selected =
            document.getSelection().rangeCount > 0
                ? document.getSelection().getRangeAt(0)
                : false;
        el.select();
        document.execCommand('copy');
        document.body.removeChild(el);
        if (selected) {
            document.getSelection().removeAllRanges();
            document.getSelection().addRange(selected);
        }
    });

    // function createGrid(size) {
    //     debugger;
    //     var ratioW = Math.floor($(window).width()/size),
    //         ratioH = Math.floor($(window).height()/size);

    //     var parent = $('<div />', {
    //         class: 'grid', 
    //         width: ratioW  * size, 
    //         height: ratioH  * size
    //     }).addClass('grid').appendTo('body');

    //     for (var i = 0; i < ratioH; i++) {
    //         for(var p = 0; p < ratioW; p++){
    //             $('<div />', {
    //                 width: size - 1, 
    //                 height: size - 1
    //             }).appendTo(parent);
    //         }
    //     }
    // }

    // createGrid(50);
});
