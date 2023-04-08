import { IPhoto } from "./photo";

export interface IUser {
  id: string,
  userName: string,
  email: string,
  displayName : string,
  inGameName: string,
  age: number,
  createdTime: Date,
  lastActive: Date,
  photos?: IPhoto[]
}
