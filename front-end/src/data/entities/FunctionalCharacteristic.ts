import { FunctionalCharacteristicByRole } from "./FunctionalCharacteristicByRole";
import { Role } from "./Role";

export interface FunctionalCharacteristic {
  id: number;
  name: string;
  roles: Role[];
  functionalCharacteristicsByRole: FunctionalCharacteristicByRole[];
}
