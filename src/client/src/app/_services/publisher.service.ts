import { Injectable } from '@angular/core';
import { environment } from "../../environments/environment.development";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { PaginatedResult } from "../_models/pagination";
import { IPublisher } from "../_models/publisher";

@Injectable({
  providedIn: 'root'
})
export class PublisherService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getPublisherList(page?:number, itemsPerPage?:number): Observable<PaginatedResult<IPublisher[]>>{
    const paginatedResult: PaginatedResult<IPublisher[]> = new PaginatedResult<IPublisher[]>();
    let params = new HttpParams();

    if(page != null && itemsPerPage != null){
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<IPublisher[]>(this.baseUrl + 'publisher', {observe: 'response', params})
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

  getPublisher(id: number): Observable<IPublisher> {
    return this.http.get<IPublisher>(this.baseUrl + 'publisher/' + id);
  }

  createPublisher(publisher: IPublisher) {
    return this.http.post(this.baseUrl + 'publisher/create/', publisher);
  }

  deletePublisher(id: number) {
    return this.http.delete(this.baseUrl + 'publisher/delete/' + id);
  }

  updatePublisher(id: number, publisher: IPublisher) {
    return this.http.put(this.baseUrl + 'publisher/edit/' + id, publisher);
  }
}
