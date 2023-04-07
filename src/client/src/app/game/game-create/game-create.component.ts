import { Component, OnInit } from '@angular/core';
import { IGame } from "../../_models/game";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { GameService } from "../../_services/game.service";
import { AlertifyService } from "../../_services/alertify.service";
import { Router } from "@angular/router";
import { PublisherService } from "../../_services/publisher.service";
import { CategoryService } from "../../_services/category.service";
import { ICategory } from "../../_models/category";
import { IPublisher } from "../../_models/publisher";

@Component({
  selector: 'app-game-create',
  templateUrl: './game-create.component.html',
  styleUrls: ['./game-create.component.scss']
})
export class GameCreateComponent implements OnInit {

  game!: IGame;
  gameForm!: FormGroup;
  categories!: ICategory[];
  publishers!: IPublisher[];

  constructor(private fb: FormBuilder,
              private gameService: GameService,
              private categoryService: CategoryService,
              private publisherService: PublisherService,
              private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit(): void {
    this.loadCategory();
    this.loadPublisher();
    this.createGameForm();

  }

  createGameForm(){
    this.gameForm = this.fb.group({
      "name": new FormControl('', Validators.required),
      "categoryId": new FormControl('', Validators.required),
      "publisherId": new FormControl(''),
      "mode": new FormControl('', Validators.required),
    });
  }

  create() {

  }

  loadCategory(){
    this.categoryService.getCategoryList().subscribe(data =>{
      this.categories = data.result;
    }, error => {
      console.log(error);
    });
  }

  loadPublisher(){
    this.publisherService.getPublisherList().subscribe(data =>{
      this.publishers = data.result;
    }, error => {
      console.log(error);
    });
  }



}
