import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-rating-meter',
  templateUrl: './rating-meter.component.html',
  styleUrls: ['./rating-meter.component.css'],

})
export class RatingMeterComponent implements OnInit {
  constructor() { }

  ngOnInit() {
  }

  public canvasWidth = 200;
  @Input() public needleValue: number;
  @Input() public centralLabel: string;
  @Input() public needleStartValue: string;
  public name = '';
  public bottomLabel: string = "";
  @Input() public options: object = {
    hasNeedle: true,
    needleColor: 'gray',
    needleUpdateSpeed: 1000,
    arcColors: ['rgb(44, 151, 222)', 'lightgray'],
    arcDelimiters: [60],
    rangeLabel: ['-10', '100'],
    needleStartValue: 50,
  };
}
