import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavUserComponent } from './nav/nav-user/nav-user.component';
import { NavAdminComponent } from './nav/nav-admin/nav-admin.component';
import { AccountModule } from "../account/account.module";



@NgModule({
  declarations: [
    NavUserComponent,
    NavAdminComponent
  ],
  exports: [
    NavUserComponent,
    NavAdminComponent,
    AccountModule
  ],
  imports: [
    CommonModule
  ]
})
export class CoreModule { }
