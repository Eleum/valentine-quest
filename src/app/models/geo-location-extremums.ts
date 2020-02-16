export class GeoLocationExtremums {
    public LatMin: number;
    public LatMax: number;
    public LngMin: number;
    public LngMax: number;

    constructor();
    constructor(latMin: number, lngMin: number, latMax: number, lngMax: number);
    constructor(latMin?: number, lngMin?: number, latMax?: number, lngMax?: number) {
        if (latMin === undefined && lngMin === undefined && latMax === undefined && lngMax === undefined) {
            this.LatMin = -1;
            this.LatMax = -1;
            this.LngMin = -1;
            this.LngMax = -1;
        } else {
            this.LatMin = latMin;
            this.LatMax = latMax;
            this.LngMin = lngMin;
            this.LngMax = lngMax;
        }
    }
}
