import { Injectable } from '@angular/core';
import { environment } from "../../environments/environment.development";
import { HttpClient } from "@angular/common/http";
import { IEnum } from "../core/common/enum";

@Injectable({
  providedIn: 'root'
})
export class CommonHelperService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getGameModes(){
    return this.http.get<IEnum[]>(this.baseUrl + 'commonHelper/game-modes');
  }





}
