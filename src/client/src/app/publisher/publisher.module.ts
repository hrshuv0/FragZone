import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from "@angular/router";
import { PublisherListComponent } from './publisher-list/publisher-list.component';
import { AuthGuard } from "../_guards/auth.guard";
import { PublisherListResolver } from "./publisher-list/publisher-list.resolver";
import { PaginationModule } from "ngx-bootstrap/pagination";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { PublisherCreateComponent } from './publisher-create/publisher-create.component';
import { PublisherEditComponent } from './publisher-edit/publisher-edit.component';


const routes: Routes = [
  { path : '', component: PublisherListComponent, canActivate:[AuthGuard], resolve:{publishers:PublisherListResolver}},
  { path : 'create', component: PublisherCreateComponent, canActivate:[AuthGuard]},
  { path : 'edit/:id', component: PublisherEditComponent, canActivate:[AuthGuard]},


]

@NgModule({
  declarations: [
    PublisherListComponent,
    PublisherCreateComponent,
    PublisherEditComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    PaginationModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  exports:[
    PublisherListComponent,
    RouterModule
  ],
  providers:[
    PublisherListResolver
  ]
})
export class PublisherModule { }
