import {Routes} from "@angular/router";
import {HomeComponent} from "./home/home/home.component";
import {CategoryListComponent} from "./category/category-list/category-list.component";


export const appRoutes : Routes = [
  { path : 'home', component: HomeComponent},
  { path : 'category', component: CategoryListComponent},

  { path : '**', redirectTo: 'home', pathMatch: 'full'}
]
