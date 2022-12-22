import { Address } from "./address.model";
import { Gender } from "./gender.model";

export interface Student {
  id: string,
  name: string,
  dateOfBirth: string,
  email: string,
  profileImg: string,
  genderId: string,
  gender: Gender,
  address: Address
}
