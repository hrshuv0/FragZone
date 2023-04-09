import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { AlertifyService } from "../../_services/alertify.service";
import { UserService } from "../../_services/user.service";
import { AccountService } from "../../_services/account.service";
import { IUser } from "../../_models/user";

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.scss']
})
export class MemberEditComponent implements OnInit{

  user!: IUser;
  photoUrl!: string;

  constructor(private route: ActivatedRoute,
              private alertify: AlertifyService,
              private userService: UserService,
              private authService: AccountService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.user = data['user'];
    });
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
    console.log(this.photoUrl);
  }

  updateUser() {
    console.log(this.user);

  }
}
