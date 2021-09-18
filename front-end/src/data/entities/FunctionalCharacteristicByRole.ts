import { FunctionalCharacteristic } from "./FunctionalCharacteristic";

export interface FunctionalCharacteristicByRole {
  id: number;
  publicationDate: string;
  roleId: number;
  functionalCharacteristicId: number;
  functionalCharacteristic: FunctionalCharacteristic;
}
