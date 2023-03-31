import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryEditComponent } from './category-edit/category-edit.component';
import { RouterLink, RouterModule, Routes } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CategoryCreateComponent } from './category-create/category-create.component';
import { AuthGuard } from "../_guards/auth.guard";
import { PaginationModule } from "ngx-bootstrap/pagination";
import { CategoryListResolver } from "./category-list/category-list.resolver";


const routes: Routes = [
  { path : '', component: CategoryListComponent, canActivate:[AuthGuard], resolve:{categories:CategoryListResolver}},
  { path : 'create', component: CategoryCreateComponent, canActivate:[AuthGuard]},
  { path : 'edit/:id', component: CategoryEditComponent, canActivate:[AuthGuard]},

];

@NgModule({
  declarations: [
    CategoryListComponent,
    CategoryEditComponent,
    CategoryCreateComponent
  ],
  exports: [
    CategoryListComponent,
    RouterModule
  ],
  imports: [
    CommonModule,
    RouterLink,
    FormsModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    PaginationModule
  ],
  providers:[
    CategoryListResolver
  ]
})
export class CategoryModule { }
