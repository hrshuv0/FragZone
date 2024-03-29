import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AccountService } from "../_services/account.service";
import { AlertifyService } from "../_services/alertify.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AccountService,
              private router: Router,
              private alertify: AlertifyService) {
  }

  canActivate(): boolean {
    if(this.authService.loggedIn()){
      return true;
    }

    this.alertify.error('You shall not pass!!!')
    this.router.navigate([''])
    return false;
  }

}
