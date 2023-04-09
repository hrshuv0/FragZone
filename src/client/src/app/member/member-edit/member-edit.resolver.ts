import { Injectable } from '@angular/core';
import {
  Resolve,
  ActivatedRouteSnapshot, Router
} from '@angular/router';
import {catchError, Observable, of} from 'rxjs';
import { IUser } from "../../_models/user";
import { UserService } from "../../_services/user.service";
import { AlertifyService } from "../../_services/alertify.service";
import { AccountService } from "../../_services/account.service";

@Injectable({
  providedIn: 'root'
})
export class MemberEditResolver implements Resolve<IUser> {

  constructor(private userService: UserService,
              private router: Router,
              private alertify: AlertifyService,
              private authService: AccountService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<IUser> {
    return this.userService.getUser(this.authService.decodedToken.nameid).pipe(
      catchError(error =>{
        this.alertify.error('Problem retrieving data');
        this.router.navigate(['']);
        return of();
      })
    );
  }
}
