import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberEditComponent } from './member-edit/member-edit.component';
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../_guards/auth.guard";


const routes: Routes = [
  { path : '', component: MemberEditComponent, canActivate:[AuthGuard]},

];

@NgModule({
  declarations: [
    MemberEditComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class MemberModule { }
