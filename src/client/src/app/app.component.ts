import { Component, OnInit } from '@angular/core';
import { AccountService } from "./_services/account.service";
import { JwtHelperService } from "@auth0/angular-jwt";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title!: 'FragZone';
  jwtHelper = new JwtHelperService();


  constructor(private authService: AccountService) {
  }
  ngOnInit(): void {
    const token = localStorage.getItem('token');
    const user = JSON.parse(localStorage.getItem('user')!);
    if(token){
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if(user){
      this.authService.currentUser = user;
      this.authService.changeMemberPhoto(user.photoUrl);
      console.log(user);
    }
  }
}
