import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DOCUMENT } from '@angular/common';
import { FormControl } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { ChartDataSets } from 'chart.js';
import { Label, Color } from 'ng2-charts';

@Component({
  selector: 'analytics',
  templateUrl: './analytics.component.html',
  styleUrls: ['./analytics.component.css']
})
export class AnalyticsComponent implements OnInit {
  constructor(private http: HttpClient, @Inject(DOCUMENT) private document: Document) {
   // this.getTemperatureAxesByDate(new Date().getTime().toLocaleString());
  }
  events: string[] = [];
  date = new FormControl(new Date());
  private sensorReadByDate: Axis;
  private forecastWeatherByDate: Axis;
  private weatherByDate: Axis;
  public chart: ChartDataSets[] = [{ data: [], label: "Temperatura" }];
  public chartLabel: Label[] = [" "];
  public axesColors: Color[] = [{ backgroundColor: ['#b60095', '#0095b6', '#95b600'] }];
  dateChange(type: string, event: MatDatepickerInputEvent<Date>) {
    this.events.push(`${type}: ${event.value}`);
    console.log(event.value.toLocaleDateString());
    this.getTemperatureAxesByDate(event.value.toLocaleDateString());
  }
 
  getTemperatureAxesByDate(date: string) {
    this.chart = [];
    this.chartLabel = [];
    const url = this.document.location.protocol + '//' + this.document.location.hostname + ':'
      + this.document.location.port;
    console.log(url + '/api/weather/sensorreadtemperaturebydate/' + date);
    this.http.get<Axis>(url + '/api/weather/sensorreadtemperaturebydate/' + date).subscribe(result => {
      this.sensorReadByDate = result;
      console.log("From sensor read:");
      console.log(this.sensorReadByDate);
      this.chartLabel = this.sensorReadByDate.ListOfXs;
      this.chart.push({ data: this.sensorReadByDate.ListOfYs, label: "Odczyt z czujnika" });
    }, error => console.log("GET ERROR: " + error));
    this.http.get<Axis>(url + '/api/weather/forecastbydate/' + date).subscribe(result => {
      this.forecastWeatherByDate = result;
      this.chart.push({ data: this.forecastWeatherByDate.ListOfYs, label: "Przewidywana pogoda" });
    }, error => console.log("Get error:" +error));
    this.http.get<Axis>(url + '/api/weather/currentweatherbydate/' + date).subscribe(result => {
      this.weatherByDate = result;
      this.chart.push({ data: this.weatherByDate.ListOfYs, label: "Stan faktyczny" });
    }, error => console.log("get error:" + error));
    //set colors i know wrong place;
    this.axesColors = [{ backgroundColor: ['#b60095', '#0095b6', '#95b600'] }]; 
  }

  ngOnInit() {
  }

}
