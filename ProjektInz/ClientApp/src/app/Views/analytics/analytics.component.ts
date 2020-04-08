import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DOCUMENT } from '@angular/common';
import { FormControl } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Label, Color, BaseChartDirective } from 'ng2-charts';
import { ChartViewComponent } from '../../chart-view/chart-view.component';

@Component({
  selector: 'analytics',
  templateUrl: './analytics.component.html',
  styleUrls: ['./analytics.component.css']
})
export class AnalyticsComponent implements OnInit {
  constructor(private http: HttpClient, @Inject(DOCUMENT) private document: Document) {
   // this.getTemperatureAxesByDate(new Date().getTime().toLocaleString());
  }
  //~~~~~~~~~~

  public lineChartLegend = true;
  public lineChartType = 'line';
  public lineChartOptions: (ChartOptions) = {
    responsive: true,
  };
  public chartClicked({ event, active }: { event: MouseEvent, active: {}[] }): void {
  }
  public chartHovered({ event, active }: { event: MouseEvent, active: {}[] }): void {
  }

  public changeColor() {
    console.log(this.chart);
    this.chart[0].borderColor = `rgba(69, 67, 11)`;
    this.chart[0].backgroundColor = `rgba(126, 126, 126)`;
    this.chart[1].borderColor = `rgba(255, 119, 119)`;
    this.chart[1].backgroundColor = `rgba(255, 217, 217)`;
    this.chart[2].borderColor = `rgba(0, 197, 0)`;
    this.chart[2].backgroundColor = `rgba(239, 239, 252)`;
  }
  //~~~~~~~~~~


  events: string[] = [];
  date = new FormControl(new Date());
  private sensorReadByDate: Axis;
  private forecastWeatherByDate: Axis;
  private weatherByDate: Axis;
  public chart: ChartDataSets[] = [{ data: [], label: "Temperatura" }];
  public chartLabel: Label[] = [" "];
  public axesColors: Color[] = [{ // grey
    backgroundColor: 'rgba(148,159,177,0.2)',
    borderColor: 'rgba(148,159,177,1)',
    pointBackgroundColor: 'rgba(148,159,177,1)',
    pointBorderColor: '#fff',
    pointHoverBackgroundColor: '#fff',
    pointHoverBorderColor: 'rgba(148,159,177,0.8)'
  },
    { // dark grey
      backgroundColor: 'rgba(77,83,96,0.2)',
      borderColor: 'rgba(77,83,96,1)',
      pointBackgroundColor: 'rgba(77,83,96,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(77,83,96,1)'
    },
    { // red
      backgroundColor: 'rgba(255,0,0,0.3)',
      borderColor: 'red',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }];
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
    console.log(this.axesColors);
    console.log(this.axesColors);
  }


  ngOnInit() {
  }

}
