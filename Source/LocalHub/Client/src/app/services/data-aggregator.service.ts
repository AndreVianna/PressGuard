import { Injectable } from '@angular/core';
import { WebSocketService } from './websocket.service';
import { map } from 'rxjs/operators';
import { DeviceData } from 'src/models/deviceData';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataAggregatorService {
  constructor(private webSocketService: WebSocketService) {
    this.init();
  }

  public latestData: BehaviorSubject<DeviceData> = new BehaviorSubject<DeviceData>({} as DeviceData);
  public error: BehaviorSubject<Event> = new BehaviorSubject<Event>({} as Event);

  private init(): void {
    this.webSocketService.getObservable()
      .pipe(
        map(event => this.parse(event.data))
      )
      .subscribe({
        next: this.processData,
        error: this.processError
      });
  }

  private parse(message: string): DeviceData {
    const parts = message.split(',');
    if (parts.length !== 10) {
      throw new Error('Invalid message format.');
    }

    const [deviceId, timestamp, ...sensorValues] = parts;

    const deviceData: DeviceData = {
      Id: Number(deviceId),
      Timestamp: timestamp,
      SensorValues: sensorValues.map(Number)
    };

    return deviceData;  }

  private processData(data: DeviceData): void {
    console.log('Received data:', data);
    this.latestData.next(data);
  }

  private processError(data: Event): void {
    console.log('Received error:', data);
    this.error.next(data);
  }
}


