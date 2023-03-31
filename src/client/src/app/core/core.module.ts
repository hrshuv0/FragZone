import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavUserComponent } from './nav/nav-user/nav-user.component';
import { NavAdminComponent } from './nav/nav-admin/nav-admin.component';
import { AccountModule } from "../account/account.module";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { RouterLink, RouterLinkActive } from "@angular/router";
import { ReactiveFormsModule } from "@angular/forms";
import { FooterComponent } from './footer/footer.component';



@NgModule({
  declarations: [
    NavUserComponent,
    NavAdminComponent,
    FooterComponent
  ],
    exports: [
        NavUserComponent,
        NavAdminComponent,
        AccountModule,
        FooterComponent
    ],
  imports: [
    CommonModule,
    BsDropdownModule,
    RouterLink,
    RouterLinkActive,
    AccountModule,
    ReactiveFormsModule
  ]
})
export class CoreModule { }
