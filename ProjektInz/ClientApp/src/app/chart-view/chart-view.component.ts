import { Component, Input,OnInit, EventEmitter, Output } from '@angular/core';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Color, BaseChartDirective, Label } from 'ng2-charts';

@Component({
  selector: 'chart-view',
  templateUrl: './chart-view.component.html',
  styleUrls: ['./chart-view.component.css']
})
export class ChartViewComponent implements OnInit {
  //TODO: REFACTOR CODE AND REMOVE THIS COMPONENT. Colors works crappy
  @Input() public lineChartLabels: Label[] = ['0'];
  @Input() public lineChartData: ChartDataSets[];
   public lineChartColors: Color[] = [];

  constructor() {
  }

  public lineChartOptions: (ChartOptions) = {
    responsive: true,
  };

  public lineChartLegend = true;
  public lineChartType = 'line';
  ngOnInit() {
  }

  // events
  public chartClicked({ event, active }: { event: MouseEvent, active: {}[] }): void {
  }
  public chartHovered({ event, active }: { event: MouseEvent, active: {}[] }): void {
  }
}
