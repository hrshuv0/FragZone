import { Component, OnInit } from '@angular/core';
import { CategoryService } from "../../_services/category.service";
import { AlertifyService } from "../../_services/alertify.service";

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit{

  categories!: any[];

  constructor(private categoryService: CategoryService, private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.getCategoryList();
  }

  getCategoryList() {
    this.categoryService.getCategoryList().subscribe(data =>{
      this.categories = data;
      // console.log(data);
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
}
