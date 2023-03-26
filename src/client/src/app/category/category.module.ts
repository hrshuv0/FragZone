import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryEditComponent } from './category-edit/category-edit.component';
import { RouterLink } from "@angular/router";
import { FormsModule } from "@angular/forms";



@NgModule({
  declarations: [
    CategoryListComponent,
    CategoryEditComponent
  ],
  exports: [
    CategoryListComponent
  ],
  imports: [
    CommonModule,
    RouterLink,
    FormsModule
  ]
})
export class CategoryModule { }
