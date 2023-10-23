import { Environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { DataAggregatorService } from './services/data-aggregator.service';
import { WebSocketService } from './services/websocket.service';
import { Device, DeviceData, Log, Configuration } from 'src/models';
import { ViewChild, AfterViewInit } from '@angular/core';
import { LoggerComponent } from 'src/app/components/logger/logger.component';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy, AfterViewInit {
    private config: Configuration = Environment;
    public devices;

    @ViewChild(LoggerComponent)
    private logger: LoggerComponent = new LoggerComponent;

    constructor(
    private http: HttpClient,
    private webSocketService: WebSocketService,
    private dataAggregatorService: DataAggregatorService) {
        this.dataAggregatorService.log.subscribe(log => this.logger.addLog(log));
        this.devices = new Map<number, DeviceData>();
        this.config.Devices.forEach((v: Device, k: number) => {
            this.devices.set(k, { Id: k } as DeviceData);
        });
        this.webSocketService.connect(9876);
    }

    ngAfterViewInit() {
        this.logger.addLog({ Level: 0, Message: 'initialized.' });
    }

    ngOnInit() {
        this.dataAggregatorService.latestData.subscribe(data => {
            if (data.Id == undefined) return;
            this.devices.set(data.Id, data);
        });
    }

    ngOnDestroy(): void {
        this.dataAggregatorService.latestData.unsubscribe();
        this.dataAggregatorService.log.unsubscribe();
    }
}
