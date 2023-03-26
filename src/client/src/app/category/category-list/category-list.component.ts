import { Component, OnInit } from '@angular/core';
import { CategoryService } from "../../_services/category.service";

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit{

  categories!: any[];

  constructor(private categoryService: CategoryService) {
  }

  ngOnInit(): void {
    this.GetCategoryList();
  }

  GetCategoryList() {
    this.categoryService.getCategoryList().subscribe(data =>{
      this.categories = data;
      // console.log(data);
    }, error => {
      console.log(error);
    });

  }

}
