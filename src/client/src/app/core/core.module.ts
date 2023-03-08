import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavUserComponent } from './nav/nav-user/nav-user.component';
import { NavAdminComponent } from './nav/nav-admin/nav-admin.component';



@NgModule({
  declarations: [
    NavUserComponent,
    NavAdminComponent
  ],
  exports: [
    NavUserComponent,
    NavAdminComponent
  ],
  imports: [
    CommonModule
  ]
})
export class CoreModule { }
