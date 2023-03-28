import { Component, OnInit, ViewChild } from '@angular/core';
import { CategoryService } from "../../_services/category.service";
import { AlertifyService } from "../../_services/alertify.service";
import { ActivatedRoute } from "@angular/router";
import { ICategory } from "../../_models/category";

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent implements OnInit{
  category!: ICategory;

  constructor(private categoryService: CategoryService,
              private alertify: AlertifyService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.loadCategory();
  }

  loadCategory(){
    this.categoryService.getCategory(+this.route.snapshot.params['id']).subscribe((category: any) =>{
      this.category = category;
    }, error => {
      this.alertify.error(error);
    });
  }

  update() {
    this.categoryService.updateCategory(+this.route.snapshot.params['id'], this.category).subscribe(() =>{
      this.alertify.success("Category updated successfully");
    }, error => {
      this.alertify.error(error);
    });
  }


}
