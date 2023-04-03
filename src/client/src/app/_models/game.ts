import { ICategory } from "./category";
import { IPublisher } from "./publisher";

export interface IGame {
  id: number,
  name: string,
  mode: string,
  category: ICategory,
  publisher: IPublisher,
  updatedTime: Date,
  status: string

}
