export interface User {
  id: number;
  username: string;
  knowAs: string;
  gender: string;
  created: Date;
  lastActive: Date;
  photoUrl: string;
  city: string;
  interests?: string;
  introduction?: string;
}
