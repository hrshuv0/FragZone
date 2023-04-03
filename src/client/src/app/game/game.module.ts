import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameListComponent } from './game-list/game-list.component';
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../_guards/auth.guard";
import { GameListResolver } from "./game-list/game-list.resolver";

const routes: Routes = [
  { path : '', component: GameListComponent, canActivate:[AuthGuard], resolve:{games:GameListResolver}},

];

@NgModule({
  declarations: [
    GameListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ],
  exports:[
    RouterModule,
    GameListComponent
  ],
  providers:[
    GameListResolver
  ]
})
export class GameModule { }
