import { Component, Input } from '@angular/core';
import { DeviceData } from 'src/models';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
    @Input()
    devices!: Map<number, DeviceData>;
}
