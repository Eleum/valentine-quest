import { Injectable } from '@angular/core';
import { v1 as uuidv1 } from 'uuid';
import { GeoLocationExtremums } from 'src/app/models/geo-location-extremums';

declare var turf: any;

@Injectable({
    providedIn: 'root'
})
export class LayoutService {
    constructor() { }

    HeartPoints: any;
    HeartPolygon: any;
    BorderBox: any;

    public generateAreas(borderBox: any, heartPoints: any, heartPolygon: any) {
        const mapExtremums = this.getHeartExtremums(heartPoints);
        const innerPoints = this.generateInnerPointsFeatures(heartPolygon, mapExtremums);

        this.BorderBox = borderBox;
        this.HeartPoints = heartPoints;
        this.HeartPolygon = heartPolygon;

        const options = {
            bbox: borderBox
        };

        const pointsForPolygons = {
            type: 'FeatureCollection',
            features: heartPoints.features.concat(innerPoints)
        };

        const voronoiPolygons = turf.voronoi(pointsForPolygons, options);

        // tslint:disable-next-line: no-shadowed-variable
        function runAreasGeneration(heartPolygon, voronoiPolygons) {
            for (let i = 0; i < voronoiPolygons.features.length; i++) {
                voronoiPolygons.features[i] = turf.intersect(voronoiPolygons.features[i], heartPolygon);
                voronoiPolygons.features[i].properties.id = uuidv1();
                // tslint:disable-next-line: no-bitwise
                voronoiPolygons.features[i].properties.completion = ~~(Math.random() * 10) * 10;
                // voronoiPolygons.features[i].properties.completion = 10;
            }
        }

        runAreasGeneration(heartPolygon.features[0], voronoiPolygons);

        return voronoiPolygons;
    }

    public getHeartExtremums(heartPointsGeoJson: any): GeoLocationExtremums {
        const extremums = new GeoLocationExtremums();

        const flattenLng = heartPointsGeoJson.features.reduce((one, other) => one.concat(other.geometry.coordinates[0]), []);
        const flattenLat = heartPointsGeoJson.features.reduce((one, other) => one.concat(other.geometry.coordinates[1]), []);

        function sortNumbersAsc(left, right) {
            return left - right;
        }

        flattenLng.sort(sortNumbersAsc);
        extremums.LngMin = flattenLng[0];
        extremums.LngMax = flattenLng[flattenLng.length - 1];

        flattenLat.sort(sortNumbersAsc);
        extremums.LatMin = flattenLat[0];
        extremums.LatMax = flattenLat[flattenLat.length - 1];

        return extremums;
    }

    public generateInnerPointsFeatures(heartPolygonGeoJson: any, extremums: GeoLocationExtremums): any {
        const innerPoints = [];
        const polygon = heartPolygonGeoJson.features[0];

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
}
