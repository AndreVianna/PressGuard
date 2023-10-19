import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Environment } from 'src/environments/environment';
import { Device, Configuration } from 'src/models';

@Injectable({
  providedIn: 'root'
})
export class WebsocketService {
  private ws: WebSocket;
  private subject!: Subject<MessageEvent>;
  private config: Configuration = Environment;

  constructor(deviceId: number) {
    const device = this.config.Devices[deviceId];
    if (!deviceId) {
      throw new Error(`Invalid device is.`);
    }
    const wsUrl = `ws://${device.Address}:${deviceId}`;
    this.ws = new WebSocket(wsUrl);
    this.connect();
  }

  public connect(): Subject<MessageEvent> {
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
    return this.subject.asObservable();
  }

  public send(data: string): void {
    this.ws.send(data);
  }
}
