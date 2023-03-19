import { Component, OnInit } from '@angular/core';
import { AccountService } from "../../../_services/account.service";

@Component({
  selector: 'app-nav-admin',
  templateUrl: './nav-admin.component.html',
  styleUrls: ['./nav-admin.component.scss']
})
export class NavAdminComponent implements OnInit{

  constructor(public authService: AccountService) {
  }

  ngOnInit(): void {
  }



  loggedIn(){
    return this.authService.loggedIn();
  }

  logout()
  {
    return this.authService.logout();
  }

}
