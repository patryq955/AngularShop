import { Photo } from "./photo";

export interface User {
  id: number;
  userName: string;
  knownAs: string;
  gender: string;
  created: Date;
  lastActive: Date;
  dateOfBirth: Date
  urlPhoto: string;
  city: string;
  introduction?: string;
  age: number;
  photos: Photo[];
}
