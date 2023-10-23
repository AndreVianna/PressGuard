import { Component, Input } from '@angular/core';
import { Log, Configuration } from 'src/models';
import { Environment } from 'src/environments/environment';

@Component({
    selector: 'logger',
    templateUrl: './logger.component.html',
    styleUrls: ['./logger.component.css']
})
export class LoggerComponent {
    @Input()
    logs: string[] = [];
    private config: Configuration = Environment;

    addLog(log: Log): void {
        if (!log || log.Level == undefined || (log.Level > this.config.MaxLogLevel)) return;
        this.logs.push(`[${this.getLevelName(log.Level)}] ${log.Message}\n`);
    }

    private getLevelName(level: number): string {
        switch (level) {
            case 0: return 'DEBUG';
            case 1: return 'INFO';
            case 2: return 'WARN';
            default: return 'ERROR';
        }
    }
}
