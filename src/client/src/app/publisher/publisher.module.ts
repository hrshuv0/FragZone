import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from "@angular/router";
import { PublisherListComponent } from './publisher-list/publisher-list.component';
import { AuthGuard } from "../_guards/auth.guard";
import { PublisherListResolver } from "./publisher-list/publisher-list.resolver";
import { PaginationModule } from "ngx-bootstrap/pagination";
import { FormsModule } from "@angular/forms";


const routes: Routes = [
  { path : '', component: PublisherListComponent, canActivate:[AuthGuard], resolve:{publishers:PublisherListResolver}},


]

@NgModule({
  declarations: [
    PublisherListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    PaginationModule,
    FormsModule,
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
