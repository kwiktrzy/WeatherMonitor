import { Component } from '@angular/core';
import { WebsocketService } from '../../websocket/websocket.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'about',
  templateUrl: './about.component.html'
})

export class AboutComponent {
  title = "O witrynie";
}

