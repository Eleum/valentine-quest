import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import * as $ from 'jquery';
import { v4 as uuidv4 } from 'uuid';
import { LayoutService } from './services/layout.service';

declare var $: $;
declare var mapboxgl: any;
declare var turf: any;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
    map: any;
    files: any[] = [];

    constructor(private http: HttpClient, private layout: LayoutService) { }

    ngOnInit() {
        $('#app-key-modal').on('shown.bs.modal', () => {
            $('#app-key-input').trigger('focus');
        });

        const bounds = [
            [26.955186661198155, 53.76002532703791], // Southwest coordinates
            [28.067600169786317, 54.08218624607514] // Northeast coordinates
        ];

        mapboxgl.accessToken = 'pk.eyJ1IjoibmV2ZXJlbmQxbmciLCJhIjoiY2swaWR2d2Y3MGI5YjNjcW1ncGtuaXN4MCJ9.rQ4HN2r10RzPKeuO3TH06w';
        const map = new mapboxgl.Map({
            container: 'map',
            style: 'mapbox://styles/mapbox/streets-v9',
            center: [27.563054951207278, 53.901454446609534],
            zoom: 10.90,
            minZoom: 10,
            maxBounds: bounds,
            attributionControl: false
        });
        this.map = map;

        const size = 150;
        const speedFactor = 2;

        // implementation of CustomLayerInterface to draw a pulsing dot icon on the map
        // see https://docs.mapbox.com/mapbox-gl-js/api/#customlayerinterface for more info
        const pulsingDot = {
            width: size,
            height: size,
            data: new Uint8Array(size * size * 4),

            // get rendering context for the map canvas when layer is added to the map
            onAdd() {
                const canvas = document.createElement('canvas');
                canvas.width = this.width;
                canvas.height = this.height;
                this.context = canvas.getContext('2d');
            },

            // called once before every frame where the icon will be used
            render() {
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

        // Create a GeoJSON source with an empty lineString.
        const geojson = {
            type: 'FeatureCollection',
            features: []
        };

        const geoJsonInnerPoints = {
            type: 'FeatureCollection',
            features: []
        };

        const geoJsonHeartPolygon = {
            type: 'FeatureCollection',
            features: [
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Polygon',
                        coordinates: [
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

        const geoJsonHeartPoints = {
            type: 'FeatureCollection',
            features: [
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: [27.47307399170134, 53.95398000106455]
                    }
                },
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: [27.52078943603584, 53.95132860970111]
                    }
                },
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: [27.556421840925708, 53.94088200384468]
                    }
                },
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: [27.596762012691784, 53.97262788574715]
                    }
                },
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: [27.637796122302973, 53.969194592551294]
                    }
                },
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: [27.704922508801815, 53.93044212805984]
                    }
                },
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: [27.673934377188004, 53.859921373043164]
                    }
                },
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: [27.574525802199076, 53.83548617934562]
                    }
                },
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: [27.46877178095783, 53.852118710423005]
                    }
                },
                {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: [27.410390949657664, 53.911338937344766]
                    }
                },
            ]
        };

        map.on('load', async () => {
            map.addImage('pulsing-dot', pulsingDot, { pixelRatio: 2 });
            map.addSource('heart-points', {
                type: 'geojson',
                data: geoJsonHeartPoints
            });
            map.addSource('polygon-area', {
                type: 'geojson',
                data: geoJsonHeartPolygon
            });
            map.addLayer({
                id: 'heart-points',
                type: 'symbol',
                source: 'heart-points',
                layout: {
                    'icon-image': 'pulsing-dot'
                }
            });
            map.addLayer({
                id: 'line-animation',
                type: 'line',
                source: {
                    type: 'geojson',
                    data: geojson
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

            const points = map.getSource('heart-points');

            const bbox = turf.bbox(geoJsonHeartPolygon);
            const areas = this.layout.generateAreas(bbox, geoJsonHeartPoints, geoJsonHeartPolygon);

            map.addSource('bbox', {
                type: 'geojson',
                data: {
                    type: 'FeatureCollection',
                    features: [bbox]
                }
            });
            map.addLayer({
                id: 'bbox',
                type: 'fill',
                source: 'bbox',
                layout: {},
                paint: {
                    'fill-color': '#000',
                    'fill-opacity': 0.2
                }
            });
            map.addSource('areas', {
                type: 'geojson',
                data: areas
            });
            map.addLayer({
                id: 'areas',
                type: 'fill',
                source: 'areas',
                layout: {},
                paint: {
                    'fill-color': [
                        'let',
                        'completionPercentage',
                        ['get', 'completion'],
                        [
                            'interpolate',
                            ['linear'],
                            ['var', 'completionPercentage'],
                            0,
                            ['to-color', '#ffebeb'],
                            100,
                            ['to-color', '#ff3334']
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
                            0,
                            0.3,
                            100,
                            1
                        ]
                    ]
                }
            });
            map.addLayer({
                id: 'areas-lines',
                type: 'line',
                source: 'areas',
                layout: {},
                paint: {
                    'line-width': 1
                }
            });
            map.addSource('inner-points', {
                type: 'geojson',
                data: geoJsonInnerPoints
            });

            map.on('mouseenter', 'areas', () => {
                map.getCanvas().style.cursor = 'pointer';
            });
            map.on('mouseleave', 'areas', () => {
                map.getCanvas().style.cursor = '';
            });
            map.on('click', 'areas', (e) => {
                const point = {
                    type: 'Feature',
                    geometry: {
                        type: 'Point',
                        coordinates: []
                    }
                };
                point.geometry.coordinates.push(e.lngLat.lng, e.lngLat.lat);

                const selectedArea = map.getSource('areas')._data.features.find((polygon) => {
                    return turf.inside(point, polygon);
                });

                const center = turf.centerOfMass(selectedArea);
                const coordinates = center.geometry.coordinates.slice();
                map.flyTo({
                    center: center.geometry.coordinates,
                    essential: true // this animation is considered essential with respect to prefers-reduced-motion
                });

                const description = `<strong>Completion: ${selectedArea.properties.completion}</strong>
                <div class="container" id="area-container" data-area=${selectedArea.properties.id}>
                    <div class="row">
                        <div class="card" style="width: 18rem;">
                            <div class="card-body">
                                <!-- <h5 class="card-title">Card title</h5>
                                <h6 class="card-subtitle mb-2 text-muted">Card subtitle</h6> -->
                                <p class="card-text">Some quick example text to build on the card title and make up the bulk of
                                    the card's
                                    content.</p>
                                <button class="btn btn-link" id="add-images-button">Add images</button>
                                <button class="btn btn-link" id="upload-images-button">Upload images</button>
                                <input id="images-input" type="file" multiple hidden/>
                            </div>
                        </div>
                    </div>
                </div>`;

                new mapboxgl.Popup()
                    .setLngLat(coordinates)
                    .setHTML(description)
                    .addTo(map);

                // doesn't work with a custom class for some reason
                $('.mapboxgl-popup-close-button')
                    .css('font-size', '1.5rem')
                    .css('font-weight', '700')
                    .css('line-height', '1')
                    .css('opacity', '0.5');

                $('#add-images-button').on('click', () => {
                    $('#images-input').trigger('click');
                });

                $('#images-input').on('change', (event: any) => {
                    this.onAddFiles(event);
                });

                $('#upload-images-button').on('click', () => {
                    const areaId = $('#area-container').data('area');
                    this.uploadFiles(areaId).subscribe(result => {
                    }, err => {
                        console.error(err);
                    });
                });

                $('.progress').each(() => {
                    const value = $(this).attr('data-value');
                    const left = $(this).find('.progress-left .progress-bar');
                    const right = $(this).find('.progress-right .progress-bar');

                    if (value > 0) {
                        if (value <= 50) {
                            right.css('transform', 'rotate(' + percentageToDegrees(value) + 'deg)');
                        } else {
                            right.css('transform', 'rotate(180deg)');
                            left.css('transform', 'rotate(' + percentageToDegrees(value - 50) + 'deg)');
                        }
                    }
                });

                function percentageToDegrees(percentage) {
                    return percentage / 100 * 360;
                }
            });

            for (let i = 0, j = i + 1; i < points._data.features.length; i++ , j++) {
                if (j === points._data.features.length) {
                    j = 0;
                }

                const emptyFeature = {
                    type: 'Feature',
                    geometry: {
                        type: 'LineString',
                        coordinates: []
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
                const startPoint = [start[0], start[1]];
                const endPoint = [end[0], end[1]];

                const diffX = endPoint[0] - startPoint[0];
                const diffY = endPoint[1] - startPoint[1];

                const sfX = diffX / speedFactor;
                const sfY = diffY / speedFactor;

                let i = 0;
                let j = 0;

                const lineCoordinates = [];

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
                        } else {
                            resolve();
                        }
                    }
                    requestAnimationFrame(step);
                });
            }

        });

        map.on('mousemove', (e: any) => {
            document.getElementById('info').innerHTML =
                JSON.stringify(e.point) +
                '<br />' +
                JSON.stringify(e.lngLat.wrap()) +
                '<br />' +
                JSON.stringify(map.getZoom());
        });

        map.on('contextmenu', (e: any) => {
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
    }

    public showGuideToast() {
        setTimeout(() => {
            $('#guide').toast('show');
        }, 0);
    }

    public showLayoutToast() {
        setTimeout(() => {
            $('#area-generator').toast('show');
        }, 0);
    }

    public getUserMapsByAppKey() {
        debugger;
        const appKey = $('#app-key-input').val();

        this.http.get('https://localhost:44394/api/maps?appkey=' + appKey)
            .subscribe(result => {
                $('#app-key-input').addClass('is-valid');
            }, err => {
                $('#app-key-input').addClass('is-invalid');
                console.error(err);
            });
    }

    public generateNewAreas() {
        const areas = this.layout.generateAreas(
            this.layout.BorderBox,
            this.layout.HeartPoints,
            this.layout.HeartPolygon
        );

        this.map.getSource('areas').setData(areas);
    }

    public saveGeneratedAreas() {
        const json = { data: [] };

        Array.from(this.map.getSource('areas')._data.features).forEach((areaFeature: any) => {
            Array.from(areaFeature.geometry.coordinates[0]).forEach((coords: Array<number>, index: number) => {
                json.data.push({
                    areaId: areaFeature.properties.id,
                    position: index,
                    latitude: coords[0],
                    longitude: coords[1]
                });
            });
        });

        let headers = new HttpHeaders();
        headers = headers.append('content-type', 'application/json');

        this.http.post('https://localhost:44394/api/areas', JSON.stringify(json), { headers })
            .subscribe(response => {
            }, err => {
                console.error(err);
            });
    }

    public onAddFiles(e: any) {
        if (e.target.files && e.target.files[0]) {
            Array.from(e.target.files).forEach((file: File) => {
                this.files.push({ fileData: file, id: uuidv4() });
            });
        }
    }

    public removeFile(idx: number) {
        this.files.splice(idx, 1);
    }

    public uploadFiles(areaId: string): Observable<any> {
        const formData = new FormData();
        this.files.forEach((file: any) => {
            formData.append('files', file.fileData);
            formData.append('ids', file.id);
            formData.append('areaid', areaId);
        });

        return this.http.post('https://localhost:44394/api/files', formData);
    }
}
