import { Injectable } from '@angular/core';
import { environment } from "../../environments/environment.development";
import { BehaviorSubject, map} from "rxjs";
import { HttpClient } from "@angular/common/http";
import { IUser } from "../_models/user";
import { JwtHelperService } from "@auth0/angular-jwt";
import {AlertifyService} from "./alertify.service";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  currentUser!: IUser | null;
  photoUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoUrl = this.photoUrl.asObservable();


  private currentUserSource = new BehaviorSubject<IUser>(null!);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private alertify: AlertifyService) { }

  login(model: any){
    return this.http.post(this.baseUrl + 'auth/login', model).pipe(
      map((response: any) =>{
        const user = response;
        if(user){
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser = user.user;
          console.log(this.currentUser);
        }
      })
    );
  }

  logout(){
    localStorage.removeItem('token');
    this.alertify.message('logged out');
  }

  loggedIn(){
    const token = localStorage.getItem('token');

    return !this.jwtHelper.isTokenExpired(token);
  }


  register(user: IUser) {
    return this.http.post(this.baseUrl + 'auth/register', user);
  }
}
