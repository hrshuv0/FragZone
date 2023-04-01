import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { JwtModule } from "@auth0/angular-jwt";
import { PaginationModule } from "ngx-bootstrap/pagination";
import { PublisherModule } from "./publisher/publisher.module";

import { AppComponent } from './app.component';
import { CategoryModule } from "./category/category.module";
import { HttpClientModule } from "@angular/common/http";
import { HomeModule } from "./home/home.module";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppRoutingModule } from "./app-routing.module";
import { CoreModule } from "./core/core.module";
import { ErrorInterceptorProvider } from "./core/_interceptors/error.interceptor";

export function tokenGetter(){
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HomeModule,
    CategoryModule,
    PublisherModule,
    CoreModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: ['localhost:5000/api/auth']
      }
    }),
    PaginationModule.forRoot()
  ],
  providers: [
    ErrorInterceptorProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
