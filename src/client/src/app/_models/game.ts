import { ICategory } from "./category";
import { IPublisher } from "./publisher";

export interface IGame {
  id: number,
  name: string,
  mode: string,
  category: string,
  publisher: string,
  updatedTime: Date,
  status: string

}
