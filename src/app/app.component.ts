import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';

declare var $: $;
declare var map: any;
declare var mapboxgl: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'valentine';
  files = [];

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

    map.on('load', async () => {
      console.log("map loaded");
    });
  }

  public showToast() {
    setTimeout(() => {
      $('.toast').toast('show');
    }, 0);
  }

  public onAddFiles(e: any) {
    if (e.target.files && e.target.files[0]) {
      const filesCount = e.target.files.length;
      Array.from(e.target.files).forEach((file: File) => {
        this.files.push(file);
      });
    }
  }

  public removeFile(idx: number) {
    this.files.splice(idx, 1);
  }
}
