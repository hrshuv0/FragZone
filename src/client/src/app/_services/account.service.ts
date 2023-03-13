import { Injectable } from '@angular/core';
import { environment } from "../../environments/environment.development";
import { BehaviorSubject, map} from "rxjs";
import { HttpClient } from "@angular/common/http";
import {IUser} from "../_models/user";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<IUser>(null!);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any){
    return this.http.post(this.baseUrl + 'auth/login', model).pipe(
      map((response: any) =>{
        const user = response;
        if(user){
          localStorage.setItem('token', user.token);
          console.log(user);
        }
      })
    );
  }
}
