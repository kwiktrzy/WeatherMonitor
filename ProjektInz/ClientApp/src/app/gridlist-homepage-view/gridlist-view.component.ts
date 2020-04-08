import { Component, Inject } from '@angular/core';
import { WebsocketService } from '../websocket/websocket.service';
import { ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';
import { HttpClient } from '@angular/common/http';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'gridlist-view',
  styleUrls: ['gridlist-view.component.css'],
  templateUrl: 'gridlist-view.component.html',
})

export class GridListView {
  public todayTemperatureData: ChartDataSets[] = [{ data: [], label: "Temperatura godzinowo:" }];
  public todayHumidityData: ChartDataSets[] = [{ data: [], label: "Wilgotność godzinowo:" }];
  public todayTemperatureLabel: Label[];
  
  public currentValues: Forecast;
  public todayTemperatureHourly: Axis;
  public todayHumidityHourly: Axis;
  //Params for current temperature from websocket
  public currentTemperature: number = 10; //bind to gauge
  public currentTemperatureConverted: number = Math.abs(-40 - this.currentTemperature) * 0.8;
  public labelTemperatureGauge: string = this.currentTemperature + "°C";
  public temperatureOptions = {//gauge config
    hasNeedle: true,
    needleUpdateSpeed: 1000,
    arcColors: ['rgb(19, 180, 239)', 'rgb(61, 204, 91)','rgb(255, 84, 84)'],
    arcDelimiters: [(Math.abs(-40 - 5) * 0.8), Math.abs(-40 - 27) * 0.8],
    rangeLabel: ['-40°C', '85°C'], //chip display range
    needleStartValue: 50
  }

  //Params for current humidity from websocket
  public currentHumidity: number = 40;
  public labelHumidityGauge: string = this.currentHumidity + " %";
  public humidityOptions = { //gauge config 
    hasNeedle: true,
    needleUpdateSpeed: 1100,
    arcColors: ['rgb(217, 238, 255)', 'rgb(145, 205, 254)', 'rgb(120, 193, 254)','rgb(44, 158, 253)'],
    arcDelimiters: [20,50, 80],
    rangeLabel: ['0 %', '100 %'],
    needleStartValue: 50,
  }
  //Params for current presure from websocket 
  public currentPressure: number = 999;
  public currentPressureConverted: number = Math.abs(300 - this.currentPressure) * 0.125; //bind to gauge 
  public labelPressureGauge: string = this.currentPressure + " hPa";
  public pressureOptions = { //gauge config 
    hasNeedle: true,
    needleUpdateSpeed: 1000,
    arcColors: ['rgb(221, 142, 245)'],
    rangeLabel: ['300 hPa', '1100 hPa'], //chip display range
    needleStartValue: 50,
  }

  //Params for current altitude
  public currentAltitude: number=40;//bind gauges there 
  public currentAltitudeConverted: number = this.currentAltitude * 0.0112;
  public labelAltitudeGauge: string = this.currentAltitude + " m";
  public altitudeOptions = {
    hasNeedle: true,
    needleUpdateSpeed: 1000,
    arcColors: ['rgb(251, 242, 212)', 'rgb(247, 228, 165)', 'rgb(243, 214, 119)', 'rgb(241, 207, 95)','rgb(239, 200, 72)'],
    arcDelimiters: [100*0.0112, 500*0.0112, 1500*0.0112, 5000* 0.0112],
    rangeLabel: ['0m', '9000m'], //chip display range
    needleStartValue: 50
  } 

  constructor(private wsService: WebsocketService, http: HttpClient,
    @Inject(DOCUMENT) private document: Document) {
    this.getHourlyTemperature(http, document);
    this.getHourlyHumidity(http, document);
    wsService.createObservableSocket().subscribe(m => {
      try {
        this.currentValues = JSON.parse(m);


        this.currentTemperature = this.currentValues.temperature;
        this.currentTemperatureConverted = Math.abs(-40 - this.currentTemperature) * 0.8;
        this.labelTemperatureGauge = this.currentValues.temperature.toString() + "°C";
        this.temperatureOptions.needleStartValue = this.currentTemperatureConverted + 1;



        this.currentHumidity = this.currentValues.humidity;
        this.labelHumidityGauge = this.currentHumidity + " %";
        this.humidityOptions.needleStartValue = this.currentHumidity+2;
 

        this.currentPressure = this.currentValues.pressure;
        this.labelPressureGauge = this.currentPressure + " hPa";
        this.pressureOptions.needleStartValue = this.currentPressureConverted + 3; 

        this.currentAltitude = this.currentValues.altitude;
        this.labelAltitudeGauge = this.currentAltitude + " m";
        this.altitudeOptions.needleStartValue =  this.currentAltitudeConverted + 2;
      }
      catch (e) {
        console.log("ERROR: " + e);
        console.log("Recieved data: " + m);
      }
    })
  }

  getHourlyTemperature(http: HttpClient, document: Document) {
    const baseUrl = document.location.protocol + '//' + document.location.hostname + ':' + document.location.port;
    http.get<Axis>(baseUrl + '/api/weather/todaysensorreadtemperature').subscribe(result => {
      this.todayTemperatureHourly = result;
      console.log(this.todayTemperatureHourly.ListOfXs);
      console.log(this.todayTemperatureHourly.ListOfYs);
      this.todayTemperatureLabel = this.todayTemperatureHourly.ListOfXs;
      this.todayTemperatureData = [{ data: this.todayTemperatureHourly.ListOfYs, label: "Temperatura" }];
    }, error => console.log("GET ERROR: " + error));
    
  }
  getHourlyHumidity(http: HttpClient, document: Document) {
    const baseUrl = document.location.protocol + '//' + document.location.hostname + ':' + document.location.port;
    http.get<Axis>(baseUrl + '/api/weather/TodaySensorReadHumidity').subscribe(result => {
      this.todayHumidityHourly = result;
      console.log(this.todayHumidityHourly.ListOfXs);
      console.log(this.todayHumidityHourly.ListOfYs);
      this.todayHumidityData = [{ data: this.todayHumidityHourly.ListOfYs, label: "Wilgotność" }];
    }, error => console.log("GET ERROR: " + error));
  }

}
