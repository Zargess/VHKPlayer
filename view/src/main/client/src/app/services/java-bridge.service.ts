import { Injectable } from '@angular/core';
import {FileNode} from "../models/file-node";

@Injectable({
  providedIn: 'root'
})
export class JavaBridgeService {

  constructor() { }

  getNode(): FileNode {
    return (window as any).javabridge.getNode();
  }
}
