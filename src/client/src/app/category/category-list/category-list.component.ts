import { Component, OnInit } from '@angular/core';
import { CategoryService } from "../../_services/category.service";
import { AlertifyService } from "../../_services/alertify.service";
import { ICategory } from "../../_models/category";
import { Pagination } from "../../_models/pagination";
import { ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit{

  categories!: ICategory[];
  pagination!: Pagination;

  constructor(private categoryService: CategoryService,
              private route: ActivatedRoute,
              private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data =>{
      this.categories = data['categories'].result;
      this.pagination = data['categories'].pagination;
    });
  }

  getCategoryList() {
    this.categoryService.getCategoryList(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe(data =>{
      this.categories = data.result;
    }, error => {
      console.log(error);
    });

  }

  deleteCategory(id: number) {
    this.alertify.confirm("Are you sure want to delete?", () =>{
      this.categoryService.deleteCategory(id).subscribe(() =>{
        this.getCategoryList();
        this.alertify.warning("Deleted Successfully");
      }, error => {
        console.log(error);
        this.alertify.error(error);
      });
    });



  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getCategoryList();
  }
}
