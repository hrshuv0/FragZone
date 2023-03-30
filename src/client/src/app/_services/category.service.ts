import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment.development";
import { ICategory } from "../_models/category";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  getCategoryList(): Observable<any[]>{
    return this.http.get<any[]>(this.baseUrl + 'category');
  }

  getCategory(id: number): Observable<ICategory>{
    return this.http.get<ICategory>(this.baseUrl + 'category/' + id);
  }

  createCategory(category: ICategory)
  {
    return this.http.post(this.baseUrl + 'category/create/', category);
  }

  updateCategory(id: number, category: ICategory)
  {
    return this.http.put(this.baseUrl + 'category/edit/' + id, category);
  }

  deleteCategory(id: number)
  {
    return this.http.delete(this.baseUrl + 'category/delete/' + id);
  }
}
