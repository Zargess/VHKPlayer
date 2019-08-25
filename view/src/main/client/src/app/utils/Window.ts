import {JavaBridgeService} from "../services/java-bridge.service";

export {};

declare global {
  interface Window {
    javabridge: JavaBridgeService;
  }
}
