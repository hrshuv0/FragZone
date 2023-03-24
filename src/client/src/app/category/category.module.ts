import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryEditComponent } from './category-edit/category-edit.component';



@NgModule({
  declarations: [
    CategoryListComponent,
    CategoryEditComponent
  ],
  exports: [
    CategoryListComponent
  ],
  imports: [
    CommonModule
  ]
})
export class CategoryModule { }
