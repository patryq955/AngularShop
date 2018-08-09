export interface User {
  id: number;
  userName: string;
  knownAs: string;
  gender: string;
  created: Date;
  lastActive: Date;
  photoUrl: string;
  city: string;
  introduction?: string;
  age: number;
}
