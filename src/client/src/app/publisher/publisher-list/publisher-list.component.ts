import { Component, OnInit } from '@angular/core';
import { PublisherService } from "../../_services/publisher.service";
import { ActivatedRoute } from "@angular/router";
import { AlertifyService } from "../../_services/alertify.service";
import { IPublisher } from "../../_models/publisher";
import { Pagination } from "../../_models/pagination";

@Component({
  selector: 'app-publisher-list',
  templateUrl: './publisher-list.component.html',
  styleUrls: ['./publisher-list.component.scss']
})
export class PublisherListComponent implements OnInit{

  publishers!: IPublisher[];
  pagination!: Pagination;

  constructor(private publisherService: PublisherService,
              private route: ActivatedRoute,
              private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data =>{
      this.publishers = data['publishers'].result;
      this.pagination = data['publishers'].pagination;
    });

  }


  getPublisherList() {
    this.publisherService.getPublisherList(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe(data =>{
      this.publishers = data.result;

    }, error => {
      console.log(error);
    });

  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getPublisherList();
  }

  deletePublisher(id: number) {

  }
}
