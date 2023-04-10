import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberEditComponent } from './member-edit/member-edit.component';
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../_guards/auth.guard";
import { MemberEditResolver } from "./member-edit/member-edit.resolver";
import { FormsModule } from "@angular/forms";
import { TabsModule } from "ngx-bootstrap/tabs";
import { PhotoEditorComponent } from './photo-editor/photo-editor.component';


const routes: Routes = [
  { path : '', component: MemberEditComponent, canActivate:[AuthGuard], resolve: {user: MemberEditResolver}}

];

@NgModule({
  declarations: [
    MemberEditComponent,
    PhotoEditorComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    TabsModule
  ],
  exports:[
    RouterModule
  ],
  providers:[
    MemberEditResolver
  ]
})
export class MemberModule { }
