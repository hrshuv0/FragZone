import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameListComponent } from './game-list/game-list.component';
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../_guards/auth.guard";
import { GameListResolver } from "./game-list/game-list.resolver";
import { PaginationModule } from "ngx-bootstrap/pagination";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { GameCreateComponent } from './game-create/game-create.component';

const routes: Routes = [
  { path : '', component: GameListComponent, canActivate:[AuthGuard], resolve:{games:GameListResolver}},
  { path : 'create', component: GameCreateComponent, canActivate:[AuthGuard]},

];

@NgModule({
  declarations: [
    GameListComponent,
    GameCreateComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    PaginationModule,
    FormsModule,
    ReactiveFormsModule,
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
