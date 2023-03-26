import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home/home.component";
import { CategoryListComponent } from "./category/category-list/category-list.component";
import { AuthGuard } from "./_guards/auth.guard";
import { CategoryEditComponent } from "./category/category-edit/category-edit.component";


const routes: Routes = [
  { path : '', component: HomeComponent},
  { path : 'category', component: CategoryListComponent, canActivate:[AuthGuard]},
  { path : 'category/edit/:id', component: CategoryEditComponent, canActivate:[AuthGuard]},

  { path : 'account', loadChildren : () => import('./account/account.module').then(mod => mod.AccountModule)},
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
