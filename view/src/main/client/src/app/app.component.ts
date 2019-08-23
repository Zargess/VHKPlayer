import { Component } from '@angular/core';
import {JavaBridgeService} from "./services/java-bridge.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';

  constructor(private javaBridgeService: JavaBridgeService) {
    console.log(javaBridgeService.getNode().getName());
  }
}
