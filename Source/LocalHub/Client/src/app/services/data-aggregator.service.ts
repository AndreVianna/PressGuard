import { Injectable } from '@angular/core';
import { WebSocketService } from './websocket.service';
import { map } from 'rxjs/operators';
import { Log, DeviceData } from 'src/models';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class DataAggregatorService {
    constructor(private webSocketService: WebSocketService) {
        this.init();
    }

    public latestData: BehaviorSubject<DeviceData> = new BehaviorSubject<DeviceData>({} as DeviceData);
    public log: BehaviorSubject<Log> = new BehaviorSubject<Log>({} as Log);

    private init(): void {
        this.webSocketService.getLog()
            .subscribe(log => this.processLog(log));
        this.webSocketService.getSubject()
            .pipe(
                map(event => this.parse(event.data))
            )
            .subscribe({
                next: this.processData.bind(this),
                error: this.processError.bind(this)
            });
    }

    private processLog(log: Log): void {
        this.log.next(log);
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

        return deviceData;
    }

    private processData(data: DeviceData): void {
        this.log.next({ Level: 0, Message: `${data.Id},${data.Timestamp},${data.SensorValues.join(',')}` });
        this.latestData.next(data);
    }

    private processError(error: string): void {
        this.log.next({ Level: 0, Message: error });
    }
}


