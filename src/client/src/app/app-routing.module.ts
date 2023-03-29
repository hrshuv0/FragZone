import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home/home.component";

const routes: Routes = [
  { path : '', component: HomeComponent},
  { path : 'category', loadChildren: () => import('./category/category.module').then(mod => mod.CategoryModule)},

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
