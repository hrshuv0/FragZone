import { Injectable } from '@angular/core';
import { environment } from "../../environments/environment.development";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { PaginatedResult } from "../_models/pagination";
import { IGame } from "../_models/game";

@Injectable({
  providedIn: 'root'
})
export class GameService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  getGameList(page?:number, itemsPerPage?:number): Observable<PaginatedResult<IGame[]>>{
    const paginatedResult: PaginatedResult<IGame[]> = new PaginatedResult<IGame[]>();
    let params = new HttpParams();

    if(page != null && itemsPerPage != null){
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<IGame[]>(this.baseUrl + 'game', {observe: 'response', params})
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

  getGame(id: number): Observable<IGame>{
    return this.http.get<IGame>(this.baseUrl + 'game/' + id);
  }

  createGame(game: IGame)
  {
    return this.http.post(this.baseUrl + 'game/create/', game);
  }

  updateGame(id: number, game: IGame)
  {
    return this.http.put(this.baseUrl + 'game/edit/' + id, game);
  }

  deleteGame(id: number)
  {
    return this.http.delete(this.baseUrl + 'game/delete/' + id);
  }



}
