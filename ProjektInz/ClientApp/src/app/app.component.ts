import { Component, Injectable } from '@angular/core';
import { DialogContentWindowModule } from './dialog-window/dialog-content-window';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})

export class AppComponent {

  constructor(dialogWindow: DialogContentWindowModule) {
    this.promptWindow(dialogWindow);      
    }
  promptWindow(dialogWindow: DialogContentWindowModule) {
    dialogWindow.openDialog(); 
  }

}
