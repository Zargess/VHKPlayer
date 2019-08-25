import { Component } from '@angular/core';
import {JavaBridgeService} from "./services/java-bridge.service";
import {Observable} from "rxjs";
import {FileNode} from "./models/file-node";
import {Observer} from "./models/observer";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';

  constructor(private javaBridgeService: JavaBridgeService) {
    console.log(javaBridgeService.getNode().getName());
    javaBridgeService.test(new Observer<FileNode>((test: FileNode) => console.log(test.getName())));
  }
}
