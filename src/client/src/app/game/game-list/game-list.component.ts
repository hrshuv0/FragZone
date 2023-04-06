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
  }

  getGameList() {
    this.gameService.getGameList(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe(data =>{
      this.games = data.result;
    }, error => {
      console.log(error);
    });
  }

  deleteGame(id: number) {
    this.alertify.confirm("Are you sure want to delete?", () =>{
      this.gameService.deleteGame(id).subscribe(() =>{
        this.getGameList();
        this.alertify.warning("Deleted Successfully");
      }, error => {
        this.alertify.error(error);
      });
    });
  }

  pageChanged(event: any) {
    this.pagination.currentPage = event.page;
    this.getGameList();
  }
}
