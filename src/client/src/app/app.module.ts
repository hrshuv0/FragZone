import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CategoryModule } from "./category/category.module";
import { HttpClientModule } from "@angular/common/http";
import { HomeModule } from "./home/home.module";
import { RouterModule } from "@angular/router";
import { appRoutes } from "./routes";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    HomeModule,
    CategoryModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
