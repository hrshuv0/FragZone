import { Component, OnInit } from '@angular/core';
import { ICategory } from "../../_models/category";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { CategoryService } from "../../_services/category.service";
import { AlertifyService } from "../../_services/alertify.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.scss']
})
export class CategoryCreateComponent implements OnInit{

  category!: ICategory;
  categoryForm!: FormGroup;

  constructor(private fb: FormBuilder,
              private categoryService: CategoryService,
              private alertify: AlertifyService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.createCategoryForm();
  }

  createCategoryForm(){
    this.categoryForm = this.fb.group({
      name: new FormControl('', Validators.required)
    });

  }


  create() {
    this.category = Object.assign({}, this.categoryForm.value);
    this.categoryService.createCategory(this.category).subscribe(() =>{
      this.alertify.success("Category Created Successfully");
      this.router.navigate(['/category']);
    }, error => {
      this.alertify.error(error);
    });
  }
}
