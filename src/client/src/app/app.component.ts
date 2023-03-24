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
    if(token){
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}
