import { Component, OnInit } from '@angular/core';
import { IGame } from "../../_models/game";
import { Pagination } from "../../_models/pagination";
import { GameService } from "../../_services/game.service";
import { ActivatedRoute } from "@angular/router";
import { AlertifyService } from "../../_services/alertify.service";

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.scss']
})
export class GameListComponent implements OnInit{

  games! :IGame[];
  pagination!: Pagination;

  constructor(private gameService: GameService,
              private route: ActivatedRoute,
              private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data =>{
      this.games = data['games'].result;
      this.pagination = data['games'].pagination;
    });

    console.log(this.games);
  }

}
