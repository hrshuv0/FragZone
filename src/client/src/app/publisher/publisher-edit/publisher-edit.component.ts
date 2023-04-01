import { Component, OnInit } from '@angular/core';
import { IPublisher } from "../../_models/publisher";
import { PublisherService } from "../../_services/publisher.service";
import { AlertifyService } from "../../_services/alertify.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-publisher-edit',
  templateUrl: './publisher-edit.component.html',
  styleUrls: ['./publisher-edit.component.scss']
})
export class PublisherEditComponent implements OnInit{

  publisher!: IPublisher;

  constructor(private publisherService: PublisherService,
              private alertify: AlertifyService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.loadPublisher();
  }

  loadPublisher(){
    this.publisherService.getPublisher(+this.route.snapshot.params['id']).subscribe((publisher: IPublisher) =>{
      this.publisher = publisher;
    }, error => {
      this.alertify.error(error);
    });
  }

  update() {
    this.publisherService.updatePublisher(+this.route.snapshot.params['id'], this.publisher).subscribe(() =>{
      this.alertify.success("Publisher updated successfully");
    }, error => {
      this.alertify.error(error);
    });
  }
}
