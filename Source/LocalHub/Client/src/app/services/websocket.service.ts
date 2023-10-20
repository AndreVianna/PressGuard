import { Injectable } from '@angular/core';
import { SignalGeneratorService } from './signal-generator.servce';
import { Observable, Subject, EMPTY } from 'rxjs';
import { Environment } from 'src/environments/environment';
import { Log, Configuration } from 'src/models';

@Injectable({ providedIn: 'root' })
export class WebSocketService {
    private config: Configuration = Environment;
    private ws!: WebSocket;
    private subject: Subject<MessageEvent<string>>;
    private log: Subject<Log>;
    private isConnected: boolean = false;

    constructor(private signalGenerator: SignalGeneratorService) {
        this.subject = new Subject();
        this.log = new Subject();
    }

    public connect(deviceId: number): void {
        this.log.next({ Level: 1, Message: `connecting to device ${deviceId} ...` });
        if (this.config.UseFakeWebSocket) {
            this.signalGenerator.start(this.subject, this.log);
        }
        else {
            this.initialiseWebSocked(deviceId);
        }
        this.log.next({ Level: 0, Message: 'connected.' });
    }

    private initialiseWebSocked(id: number): void {
        if (!id) {
            throw new Error('Invalid device id.');
        }
        const device = this.config.Devices.get(id)!;
        const wsUrl = `ws://${device.Address}:${id}`;
        this.log.next({ Level: 0, Message: `connecting to ${wsUrl}.` });
        this.ws = new WebSocket(wsUrl, 'websockets');

        this.ws.onopen = () => {
            this.isConnected = true;
            this.log.next({ Level: 0, Message: 'connected.' });
        };

        this.ws.onmessage = (event) => {
            this.subject.next(event);
            this.log.next({ Level: 0, Message: event.data });
        };

        this.ws.onerror = () => {
            this.log.next({ Level: 3, Message: 'Error receiving data from the websocket.' });
        };

        this.ws.onclose = () => {
            this.subject.complete();
            this.log.next({ Level: 0, Message: 'closed.' });
            this.isConnected = false;
        };
    }

    public getSubject(): Observable<MessageEvent<string>> {
        if (!this.subject) {
            this.log.next({ Level: 3, Message: 'Connection has not been established yet.' });
            return EMPTY;
        }
        return this.subject.asObservable();
    }

    public getLog(): Observable<Log> {
        return this.log.asObservable();
    }

    public send(data: string): void {
        if (this.isConnected) this.ws.send(data);
    }
}
