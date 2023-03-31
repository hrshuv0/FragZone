import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  ActivatedRouteSnapshot
} from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { ICategory } from "../../_models/category";
import { CategoryService } from "../../_services/category.service";
import { PaginatedResult } from "../../_models/pagination";
import { AlertifyService } from "../../_services/alertify.service";

@Injectable({
  providedIn: 'root'
})
export class CategoryListResolver implements Resolve<PaginatedResult<ICategory[]>> {

  constructor(private categoryService: CategoryService,
              private router: Router,
              private alertify: AlertifyService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<ICategory[]>> {
    let pageNumber = 1;
    let pageSize = 5;

    return this.categoryService.getCategoryList(pageNumber, pageSize)
      .pipe(
        catchError(error =>{
          this.alertify.error('Problem retrieving data!');
          this.router.navigate(['']);
          return of();
        })
      );

  }
}
