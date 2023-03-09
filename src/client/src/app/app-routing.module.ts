import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home/home.component";
import { CategoryListComponent } from "./category/category-list/category-list.component";


const routes: Routes = [
  { path : '', component: HomeComponent},
  { path : 'category', component: CategoryListComponent},

  { path : '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class AppRoutingModule { }
