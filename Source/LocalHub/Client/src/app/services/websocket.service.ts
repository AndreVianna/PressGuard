import { Injectable } from '@angular/core';
import { Observable, Subject, EMPTY } from 'rxjs';
import { Environment } from 'src/environments/environment';
import { Device, Configuration } from 'src/models';

@Injectable({
  providedIn: 'root'
})
export class WebSocketService {
  private config: Configuration = Environment;
  private ws!: WebSocket;
  private subject!: Subject<MessageEvent>;

  constructor() {
  }

  public connect(deviceId: number): Subject<MessageEvent> {
    const device = this.config.Devices[deviceId];
    if (!deviceId) {
      throw new Error(`Invalid device id.`);
    }

    const wsUrl = `ws://${device.Address}:${deviceId}`;
    this.ws = new WebSocket(wsUrl);
    this.subject = new Subject();

    this.ws.onmessage = (event) => {
      this.subject.next(event);
    };

    this.ws.onerror = (event) => {
      this.subject.error(event);
    };

    this.ws.onclose = (event) => {
      this.subject.complete();
    };

    return this.subject;
  }

  public getObservable(): Observable<MessageEvent> {
    if (!this.subject) {
      console.error("Connection has not been established yet. Call the connect() method first.");
      return EMPTY;  // EMPTY is a predefined empty Observable
    }
    return this.subject.asObservable();
  }

  public send(data: string): void {
    this.ws.send(data);
  }
}
