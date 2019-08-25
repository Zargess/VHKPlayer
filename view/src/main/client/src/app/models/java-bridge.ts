import {FileNode} from "./file-node";
import {Observer} from "./observer";
import {Subscription} from "rxjs";

export interface JavaBridge {
  getNode(): FileNode;
  test(o: Observer<FileNode>): Subscription;
}
