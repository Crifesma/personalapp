import { FunctionalCharacteristic } from "./FunctionalCharacteristic";
import { FunctionalCharacteristicByRole } from "./FunctionalCharacteristicByRole";

export interface Role {
  id: number;
  name: string;
  functionalCharacteristics: FunctionalCharacteristic[];
  functionalCharacteristicsByRole: FunctionalCharacteristicByRole[];
}
