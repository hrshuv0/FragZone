import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  ActivatedRouteSnapshot
} from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { PaginatedResult } from "../../_models/pagination";
import { IPublisher } from "../../_models/publisher";
import { AlertifyService } from "../../_services/alertify.service";
import { PublisherService } from "../../_services/publisher.service";

@Injectable({
  providedIn: 'root'
})
export class PublisherListResolver implements Resolve<PaginatedResult<IPublisher[]>> {

  constructor(private publisherService: PublisherService,
              private router: Router,
              private alertify: AlertifyService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<IPublisher[]>> {
    let pageNumber = 1;
    let pageSize = 5;

    return this.publisherService.getPublisherList(pageNumber, pageSize)
      .pipe(
        catchError(error =>{
          this.alertify.error('Problem retrieving data!');
          this.router.navigate(['']);
          return of();
        })
      );

  }
}
