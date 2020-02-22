import { Injectable } from '@angular/core';
import { v4 as uuidv4 } from 'uuid';
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
                voronoiPolygons.features[i].properties.id = uuidv4();
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

    public convertToMapAreas(rawAreas: any[]) {
        const areasPoints = this.createAreasPoints(rawAreas);
        const polygons = this.createPolygons(areasPoints);
        return this.createFeatureCollection(polygons);
    }

    private createAreasPoints(rawAreas: any[]) {
        const areasPoints = [];

        rawAreas.forEach((area: any) => {
            area.geoPoints.sort((one, other) => (one.position > other.position) ? 1 : -1);
            const areaPoints = [];
            area.geoPoints.forEach((geoPoint: any) => {
                areaPoints.push([geoPoint.latitude, geoPoint.longitude]);
            });
            areasPoints.push({ id: area.id, points: areaPoints });
        });

        return areasPoints;
    }

    private createPolygons(areasPoints: any[]) {
        const polygons = [];

        areasPoints.forEach(areaPoints => {
            const polygon: any = {
                type: 'Feature',
                properties: {},
                geometry: {
                    type: 'Polygon',
                    coordinates: [areaPoints.points]
                }
            };
            // TODO: completion property
            polygon.properties.id = areaPoints.id;
            polygon.properties.completion = 0;
            polygons.push(polygon);
        });

        return polygons;
    }

    private createFeatureCollection(polygons: any[]) {
        return {
           type: 'FeatureCollection',
           features: polygons
        };
    }
}
