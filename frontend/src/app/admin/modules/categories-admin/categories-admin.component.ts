import {Component, OnInit} from '@angular/core';
import { CategoryService } from '../../../services/category.service'
import { Category } from '../../../models/category.model';
import { NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {CreateNewCategoryFormComponent} from '../../shared/create-new-category-form/create-new-category-form.component';
import {UpdateExistedCategoryModalComponent} from '../../shared/update-existed-category-modal/update-existed-category-modal.component';
import {DeleteExistedCategoryComponent} from '../../shared/delete-existed-category/delete-existed-category.component';

@Component({
  selector: 'app-categories-admin',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    FormsModule,
    CreateNewCategoryFormComponent,
    UpdateExistedCategoryModalComponent,
    DeleteExistedCategoryComponent,
  ],
  templateUrl: './categories-admin.component.html',
  styleUrl: './categories-admin.component.css'
})

export class CategoriesAdminComponent implements OnInit {
  categories: Array<Category> = [];

  // category: Category = {
  //   id : 0,
  //   name: "",
  //   slug: "",
  //   description: ""
  // };

  constructor (private categoryService: CategoryService) { }

  ngOnInit() {
    this.GetCategories();
  }

  GetCategories () {
    this.categoryService.getCategories().subscribe({
      next : (categories: Category[]) => { this.categories = categories },
      error : error => console.log(error)
    })
  }
}
