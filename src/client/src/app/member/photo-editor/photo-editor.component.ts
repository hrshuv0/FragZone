import { Component, Input, OnInit } from '@angular/core';
import { IPhoto } from "../../_models/photo";

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.scss']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos!: IPhoto[] | undefined;

  constructor() {
  }

  ngOnInit(): void {
    console.log(this.photos);
  }


  deletePhoto(id: string) {

  }
}
