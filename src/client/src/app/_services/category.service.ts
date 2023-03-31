import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { environment } from "../../environments/environment.development";
import { ICategory } from "../_models/category";
import { PaginatedResult } from "../_models/pagination";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  getCategoryList(page?:number, itemsPerPage?:number): Observable<PaginatedResult<ICategory[]>>{
    const paginatedResult: PaginatedResult<ICategory[]> = new PaginatedResult<ICategory[]>();
    let params = new HttpParams();

    if(page != null && itemsPerPage != null){
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<ICategory[]>(this.baseUrl + 'category', {observe: 'response', params})
      .pipe(
        map(response =>{
          paginatedResult.result = response.body!;
          if(response.headers.get('Pagination') != null){
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
          }
          return paginatedResult;
        })
      );
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
