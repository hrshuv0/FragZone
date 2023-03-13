import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { RouterModule, Routes } from "@angular/router";
import {ReactiveFormsModule} from "@angular/forms";

const routes:Routes = [
  { path : 'login', component : LoginComponent},
  { path : 'register', component : RegisterComponent},
]

@NgModule({
  declarations: [
    RegisterComponent,
    LoginComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule
  ],
  exports: [
    RouterModule
  ]
})
export class AccountModule { }
