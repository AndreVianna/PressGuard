import { Component, Input } from '@angular/core';
import { DeviceData } from 'src/models';

@Component({
    selector: 'app-device',
    templateUrl: './device.component.html',
    styleUrls: ['./device.component.css']
})
export class DeviceComponent {
    @Input()
    device!: DeviceData;
}
