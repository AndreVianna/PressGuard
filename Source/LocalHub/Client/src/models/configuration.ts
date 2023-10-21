import { Device } from './device';

export interface Configuration {
    UseFakeWebSocket: boolean,
    IsProduction: boolean;
    MaxLogLevel: number;
    Devices: Map<number, Device>;
}
