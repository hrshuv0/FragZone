import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IPhoto } from "../../_models/photo";
import { FileUploader } from "ng2-file-upload";
import { environment } from "../../../environments/environment.development";
import { AccountService } from "../../_services/account.service";
import { UserService } from "../../_services/user.service";
import { AlertifyService } from "../../_services/alertify.service";

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.scss']
})
export class PhotoEditorComponent implements OnInit {
  baseUrl = environment.apiUrl;
  currentMain!: IPhoto;

  @Input() photos!: IPhoto[] | undefined;
  @Output() getMemberPhotoChange = new EventEmitter<string>();

  uploader!:FileUploader;
  hasBaseDropZoneOver = false;

  constructor(private authService: AccountService,
              private userService: UserService,
              private alertify: AlertifyService){  }

  ngOnInit(): void {
    this.initializeUploader();
  }

  public fileOverBase(e:any):void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'photos/' + this.authService.decodedToken.nameid,
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: IPhoto = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          isMain: res.isMain
        }

        this.photos?.push(photo);
        if(photo.isMain){
          this.authService.changeMemberPhoto(photo.url);
          this.authService.currentUser!.photoUrl! = photo.url;
          localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
        }
      }
    }
  }

  deletePhoto(id: string) {

  }

  setMainPhoto(photo: IPhoto) {
    this.userService.setMainPhoto(this.authService.decodedToken.nameid, photo.id).subscribe(() => {
      this.currentMain = this.photos?.filter(p => p.isMain === true)[0]!;
      this.currentMain.isMain = false;
      photo.isMain = true;
      this.authService.changeMemberPhoto(photo.url);
      this.authService.currentUser!.photoUrl! = photo.url;
      localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
    }, error => {
      this.alertify.error(error);
    })
  }


}
