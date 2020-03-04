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
  public todayTemperatureLabel: Label[] = [" "];
  //TODO: dodanie komponentów widocznych wewnątrz tych elementów
  //wyudaje mi się że powinno pójść subscribe na tą wartość aby obserwować all the time zmieniającą się nia
  public currentValues: Forecast;
  public todayTemperatureHourly: Axis;
  public todayHumidityHourly: Axis;
  private currentTemperature: number;
  private currentHumidity: number;
  private currentAltitude: number;
  private currentPressure: number;
  constructor(private wsService: WebsocketService, http: HttpClient,
    @Inject(DOCUMENT) private document: Document) {
    this.getHourlyTemperature(http,document);
    wsService.createObservableSocket().subscribe(m => {
      try {
        this.currentValues = JSON.parse(m);
        this.currentTemperature = this.currentValues.temperature;
        this.currentHumidity = this.currentValues.humidity;
        this.currentPressure = this.currentValues.pressure;
        this.currentAltitude = this.currentValues.altitude;
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
    http.get<Axis>(baseUrl + '/api/weather/todaysensorreadhumidity').subscribe(result => {
      this.todayHumidityHourly = result;
      console.log(this.todayHumidityHourly.ListOfXs);
      console.log(this.todayHumidityHourly.ListOfYs);
      this.todayHumidityData = [{ data: this.todayTemperatureHourly.ListOfYs, label: "Wilgotność" }];
    }, error => console.log("GET ERROR: " + error));
  }

}
