import { Injectable } from '@angular/core';
import { environment } from "../../environments/environment.development";
import { BehaviorSubject, map} from "rxjs";
import { HttpClient } from "@angular/common/http";
import { IUser } from "../_models/user";
import { JwtHelperService } from "@auth0/angular-jwt";
import { AlertifyService } from "./alertify.service";
import { Router } from "@angular/router";

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


  constructor(private http: HttpClient,
              private alertify: AlertifyService,
              private router: Router) { }

  changeMemberPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }


  login(model: any){
    return this.http.post(this.baseUrl + 'auth/login', model).pipe(
      map((response: any) =>{
        const user = response;
        if(user){
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user.user));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser = user.user;
          this.changeMemberPhoto(this.currentUser?.photoUrl!);
        }
      })
    );
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.decodedToken = null;
    this.currentUser = null;
    this.alertify.message('logged out');
    this.router.navigate([''])
      .then(r => console.log(r));
  }

  loggedIn(){
    const token = localStorage.getItem('token');

    return !this.jwtHelper.isTokenExpired(token);
  }


  register(user: IUser) {
    return this.http.post(this.baseUrl + 'auth/register', user);
  }
}
