import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable()
export class WebsocketService {
  url = "wss://" + location.host + "/ws";
  constructor() {
    console.log("This is url: " + this.url);
  }
  public ws: WebSocket;
  public createObservableSocket(): Observable<string> {
    this.ws = new WebSocket(this.url);
    return Observable.create(observer => {
      this.ws.onopen = event => console.log(event);
      this.ws.onmessage = event => {
        observer.next(event.data);
        console.log(event.data);
      };
      this.ws.onerror = event => observer.error(event);
      this.ws.onclose = event => observer.complete();
    })
  };

}
