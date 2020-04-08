import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { AppComponent } from './app.component';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GaugesModule } from '@progress/kendo-angular-gauges';
import { ArcGaugeModule } from '@progress/kendo-angular-gauges';
import { MatButtonModule, MatCheckboxModule, MatToolbarModule, MatSidenavModule, MatIconModule, MatListModule, MatDialogModule } from '@angular/material';
import { MainNavComponent } from './main-nav/main-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { DialogContentWindowModule, DialogContentPrompt } from './dialog-window/dialog-content-window';
import { MatGridListModule } from '@angular/material/grid-list';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import { ChartsModule, Color } from 'ng2-charts'
import { MatNativeDateModule } from '@angular/material';
import { AboutComponent } from './Views/about/about.component';
import { HomeComponent } from './Views/homepage/home.component';
import { GridListView } from './gridlist-homepage-view/gridlist-view.component';
import { ChartViewComponent } from './chart-view/chart-view.component';
import { WebsocketService } from './websocket/websocket.service';
import { AnalyticsComponent } from './Views/analytics/analytics.component';
import { RatingMeterComponent } from './rating-meter/rating-meter.component';
import { GaugeChartModule } from 'angular-gauge-chart'
@NgModule({
  declarations: [
    AppComponent,
    MainNavComponent,
    DialogContentWindowModule,
    DialogContentPrompt,
    AboutComponent,
    HomeComponent,
    GridListView,
    ChartViewComponent,
    AnalyticsComponent,
    RatingMeterComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule, GaugeChartModule,
    FormsModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    ButtonsModule,
    BrowserAnimationsModule,
    GaugesModule,
    ArcGaugeModule,
    MatButtonModule, MatCheckboxModule, LayoutModule, MatToolbarModule, MatSidenavModule, MatIconModule, MatListModule, MatDialogModule, MatGridListModule, FlexLayoutModule,
    MatCardModule,
    ChartsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'about', component: AboutComponent },
      { path: 'analytics', component: AnalyticsComponent }

    ])
  ],
  entryComponents: [DialogContentWindowModule, DialogContentPrompt],
  providers: [DialogContentWindowModule, WebsocketService],
  bootstrap: [AppComponent]
})
export class AppModule { }
