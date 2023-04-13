import { Component, OnInit } from '@angular/core';
import { AccountService } from "../../../_services/account.service";

@Component({
  selector: 'app-nav-admin',
  templateUrl: './nav-admin.component.html',
  styleUrls: ['./nav-admin.component.scss']
})
export class NavAdminComponent implements OnInit{

  model: any = {};
  photoUrl!: string;

  constructor(public authService: AccountService) {
  }

  ngOnInit(): void {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
    console.log(this.photoUrl);
  }



  loggedIn(){
    return this.authService.loggedIn();
  }

  logout()
  {
    return this.authService.logout();
  }

}
