import {Injectable} from '@angular/core';
import {FileNode} from "../models/file-node";
import {Observer} from "../models/observer";

@Injectable({
  providedIn: 'root'
})
export class JavaBridgeService {

  constructor() { }

  getNode(): FileNode {
    return window.javabridge.getNode();
  }

  test(o: Observer<FileNode>): void {
    window.javabridge.test(o);
  }
}
