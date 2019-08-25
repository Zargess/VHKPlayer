import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import {FileNode} from "./app/models/file-node";
import {JavaBridge} from "./app/models/java-bridge";
import {Observer} from "./app/models/observer";
import {Subscription} from "rxjs";

console.log(environment.name);
if (environment.production) {
  enableProdMode();
} else {
  let bridge: JavaBridge = {
    test(o: Observer<FileNode>): Subscription {
      return null;
    },
    getNode(): FileNode {
      return new class implements FileNode {
        exists(): boolean {
          return false;
        }

        getName(): string {
          return "Bob";
        }

        getPath(): string {
          return "Full/Path/Bob";
        }
      };
    }
  };
  window.javabridge = bridge;
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
