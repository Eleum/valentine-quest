
$(document).ready(function () {
    mapboxgl.accessToken = 'pk.eyJ1IjoibmV2ZXJlbmQxbmciLCJhIjoiY2swaWR2d2Y3MGI5YjNjcW1ncGtuaXN4MCJ9.rQ4HN2r10RzPKeuO3TH06w';
    var map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/streets-v9',
        center: [27.563054951207278, 53.901454446609534],
        zoom: 11,
        minZoom: 11,
        attributionControl: false
    });

    var size = 150;

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

    // debugger;
    // var polyline = L.polyline([]).addTo(map);

    // // add your points
    // var points = [
    //     {
    //         "type": "Feature",
    //         "geometry": {
    //             "type": "Point",
    //             "coordinates": [27.47307399170134, 53.95097000106455]
    //         },
    //         "properties": {}
    //     },
    //     {
    //         "type": "Feature",
    //         "geometry": {
    //             "type": "Point",
    //             "coordinates": [27.52078943603584, 53.95132860970111]
    //         },
    //         "properties": {}
    //     },
    //     {
    //         "type": "Feature",
    //         "geometry": {
    //             "type": "Point",
    //             "coordinates": [27.556421840925708, 53.94088200384468]
    //         },
    //         "properties": {}
    //     }
    // ]

    // // add a variable for keeping track of points
    // var y = 0;

    // // Start drawing the polyline.
    // add();

    // function add() {

    //     // add a point on the line for the new marker
    //     polyline.addLatLng(
    //         L.latLng(points[y].geometry.coordinates[1],
    //             points[y].geometry.coordinates[0])
    //     );


    //     // Pan the map along with where the line is being added.
    //     map.setView(points[y].geometry.coordinates, 3);

    //     // Continue to draw and pan the map by calling `add()`
    //     // until `y` reaches the number of points
    //     if (++y < points.length) window.setTimeout(add, 1000);
    // }

    // Create a GeoJSON source with an empty lineString.
    var geojson = {
        "type": "FeatureCollection",
        "features": [{
            "type": "Feature",
            "geometry": {
                "type": "LineString",
                "coordinates": []
            }
        }]
    };

    var startPoint = [27.47307399170134, 53.95097000106455]
    var endPoint = [27.52078943603584, 53.95132860970111]

    var framesPerSecond = 20;
    var initialOpacity = 1
    var opacity = initialOpacity;
    var initialRadius = 4;
    var radius = initialRadius;
    var maxRadius = 15;

    var speedFactor = 100 // number of frames per longitude degree
    var animation; // to store and cancel the animation


    map.on('load', function () {
        map.addImage('pulsing-dot', pulsingDot, { pixelRatio: 2 });

        map.addSource('points', {
            'type': 'geojson',
            'data': {
                'type': 'FeatureCollection',
                'features': [
                    // {
                    //     'type': 'Feature',
                    //     'geometry': {
                    //         'type': 'Point',
                    //         'coordinates': [27.518821384407858, 53.93200917348656]
                    //     }
                    // },
                    // {
                    //     'type': 'Feature',
                    //     'geometry': {
                    //         'type': 'Point',
                    //         'coordinates': [27.64614303695123, 53.931181804173264]
                    //     }
                    // },
                    // home
                    // {
                    //     'type': 'Feature',
                    //     'geometry': {
                    //         'type': 'Point',
                    //         'coordinates': [27.5936424683064, 53.91032187347324]
                    //     }
                    // },
                    // {
                    //     'type': 'Feature',
                    //     'geometry': {
                    //         'type': 'Point',
                    //         'coordinates': [27.568568629161973, 53.88259620815066]
                    //     }
                    // },
                    // {
                    //     'type': 'Feature',
                    //     'geometry': {
                    //         'type': 'Point',
                    //         'coordinates': [27.484323896871246, 53.90801811424501]
                    //     }
                    // },
                    // {
                    //     'type': 'Feature',
                    //     'geometry': {
                    //         'type': 'Point',
                    //         'coordinates': [27.488175296105823, 53.909599627589415]
                    //     }
                    // },
                    // {
                    //     'type': 'Feature',
                    //     'geometry': {
                    //         'type': 'Point',
                    //         'coordinates': [27.57535711894309, 53.85792866840478]
                    //     }
                    // },
                    // {
                    //     'type': 'Feature',
                    //     'geometry': {
                    //         'type': 'Point',
                    //         'coordinates': [27.572075519850557, 53.93377672976908]
                    //     }
                    // },
                    // {
                    //     'type': 'Feature',
                    //     'geometry': {
                    //         'type': 'Point',
                    //         'coordinates': [27.609492507709234, 53.92125454783536]
                    //     }
                    // },
                    {
                        'type': 'Feature',
                        'geometry': {
                            'type': 'Point',
                            'coordinates': [27.47307399170134, 53.95097000106455]
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
                            'coordinates': [27.596762012691784, 53.95262788574715]
                        }
                    },
                    {
                        'type': 'Feature',
                        'geometry': {
                            'type': 'Point',
                            'coordinates': [27.637796122302973, 53.949194592551294]
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
                            'coordinates': [27.47877178095783, 53.862118710423005]
                        }
                    },
                    {
                        'type': 'Feature',
                        'geometry': {
                            'type': 'Point',
                            'coordinates': [27.430390949657664, 53.911338937344766]
                        }
                    },
                    {
                        'type': 'Feature',
                        'geometry': {
                            'type': 'Point',
                            'coordinates': [27.673934377188004, 53.859921373043164]
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

        var diffX = endPoint[0] - startPoint[0];
        var diffY = endPoint[1] - startPoint[1];

        var sfX = diffX / speedFactor;
        var sfY = diffY / speedFactor;

        var i = 0;
        var j = 0;

        var lineCoordinates = [];

        while (i < diffX || Math.abs(j) < Math.abs(diffY)) {
            lineCoordinates.push([startPoint[0] + i, startPoint[1] + j]);

            if (i < diffX) {
                i += sfX;
            }

            if (Math.abs(j) < Math.abs(diffY)) {
                j += sfY;
            }
        }

        console.log(lineCoordinates);

        var animationCounter = 0;

        function animateLine() {
            if (animationCounter < lineCoordinates.length) {
                geojson.features[0].geometry.coordinates.push(lineCoordinates[animationCounter]);
                map.getSource('line-animation').setData(geojson);

                requestAnimationFrame(animateLine);
                animationCounter++;
            }
        }

        animateLine();
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
        debugger;
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
});
