import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  ActivatedRouteSnapshot
} from '@angular/router';
import {catchError, Observable, of} from 'rxjs';
import { PaginatedResult } from "../../_models/pagination";
import { IGame } from "../../_models/game";
import { AlertifyService } from "../../_services/alertify.service";
import { GameService } from "../../_services/game.service";

@Injectable({
  providedIn: 'root'
})
export class GameListResolver implements Resolve<PaginatedResult<IGame[]>> {

  constructor(private gameService: GameService,
              private router: Router,
              private alertify: AlertifyService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<IGame[]>> {
    let pageNumber = 1;
    let pageSize = 5;

    return this.gameService.getGameList(pageNumber, pageSize)
      .pipe(
        catchError(error =>{
          this.alertify.error('Problem retrieving data!');
          this.router.navigate(['']);
          return of();
        })
      );

  }
}
