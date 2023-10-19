import { Device, Configuration } from 'src/models';

export const Environment : Configuration = {
  IsProduction: false,
  Devices: {
    9876: {
      Address: '192.168.2.40',
      Delay: 20
    }
  }
};
