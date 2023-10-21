import { Environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { DataAggregatorService } from './services/data-aggregator.service';
import { WebSocketService } from './services/websocket.service';
import { Device, DeviceData, Log, Configuration } from 'src/models';
import { interval, Subscription } from 'rxjs';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
    private config: Configuration = Environment;
    private dateTimeSubscription!: Subscription;

    public title = 'Local Hub';
    public devices;
    public logs: string[] = [];
    public currentDateTime: string = '';

    constructor(
    private http: HttpClient,
    private webSocketService: WebSocketService,
    private dataAggregatorService: DataAggregatorService) {
        this.dataAggregatorService.log.subscribe(log => this.addLog(log));
        this.devices = new Map<number, DeviceData>();
        this.config.Devices.forEach((v: Device, k: number) => {
            this.devices.set(k, { Id: k } as DeviceData);
        });
        this.webSocketService.connect(9876);
    }

    addLog(log: Log): void {
        if (!log || log.Level == undefined || (log.Level > this.config.MaxLogLevel)) return;
        this.logs.push(`[${this.getLevelName(log.Level) }] ${log.Message}\n`);
    }

    private getLevelName(level: number): string {
        switch (level) {
            case 0: return 'DEBUG';
            case 1: return 'INFO';
            case 2: return 'WARN';
            default: return 'ERROR';
        }
    }

    ngOnInit() {
        this.addLog({ Level: 0, Message: 'initializing...'});
        this.dataAggregatorService.latestData.subscribe(data => {
            if (data.Id == undefined) return;
            this.devices.set(data.Id, data);
        });
        this.dateTimeSubscription = interval(1000).subscribe(() => {
            this.currentDateTime = new Date().toLocaleString();
        });
        this.addLog({ Level: 0, Message: 'initialized.' });
    }

    ngOnDestroy(): void {
        this.dateTimeSubscription.unsubscribe();
        this.dataAggregatorService.latestData.unsubscribe();
        this.dataAggregatorService.log.unsubscribe();
    }
}
