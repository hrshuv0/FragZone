import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameListComponent } from './game-list/game-list.component';
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../_guards/auth.guard";

const routes: Routes = [
  { path : '', component: GameListComponent, canActivate:[AuthGuard]},

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
  ]
})
export class GameModule { }
