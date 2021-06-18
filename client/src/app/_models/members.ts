import { Photo } from "./photo";

export interface Member {
  id: number;
  username: string;
  photoUrl: string;
  age: number;
  dateOfBirth: Date;
  knownAs: string;
  created: Date;
  lastactive: Date;
  gender: string;
  introduction: string;
  lookingFor: string;
  interest: string;
  city: string;
  country: string;
  photos: Photo[];
}


