import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RatingMeterComponent } from './rating-meter.component';

describe('RatingMeterComponent', () => {
  let component: RatingMeterComponent;
  let fixture: ComponentFixture<RatingMeterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RatingMeterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RatingMeterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
