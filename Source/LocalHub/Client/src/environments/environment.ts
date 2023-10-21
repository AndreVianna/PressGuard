import { Configuration, Device } from 'src/models';

export const Environment : Configuration = {
    IsProduction: false,
    UseFakeWebSocket: true,
    MaxLogLevel: 0,
    Devices: new Map<number, Device>([
        [9870, {
            Address: '192.168.2.40',
        }],
        [9871, {
            Address: '192.168.2.41',
        }],
        [9872, {
            Address: '192.168.2.42',
        }],
        [9873, {
            Address: '192.168.2.43',
        }],
        [9874, {
            Address: '192.168.2.44',
        }],
        [9875, {
            Address: '192.168.2.45',
        }],
        [9876, {
            Address: '192.168.2.46',
        }],
        [9877, {
            Address: '192.168.2.47',
        }],
        [9878, {
            Address: '192.168.2.48',
        }],
        [9879, {
            Address: '192.168.2.49',
        }],
        [9880, {
            Address: '192.168.2.50',
        }],
        [9881, {
            Address: '192.168.2.51',
        }]
    ]),
};
