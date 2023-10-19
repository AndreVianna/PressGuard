import { Device } from "./device";

export interface Configuration {
  IsProduction: boolean;
  Devices: {
    [Id: number]: Device;
  };
}
