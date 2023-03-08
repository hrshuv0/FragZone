import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeUserComponent } from './home-user/home-user.component';
import { HomeAdminComponent } from './home-admin/home-admin.component';
import { CoreModule } from "../core/core.module";
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [
    HomeUserComponent,
    HomeAdminComponent,
    HomeComponent
  ],
  exports: [
    HomeAdminComponent,
    HomeUserComponent
  ],
  imports: [
    CommonModule,
    CoreModule
  ]
})
export class HomeModule { }
