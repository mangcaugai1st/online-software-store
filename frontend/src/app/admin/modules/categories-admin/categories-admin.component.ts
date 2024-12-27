import {Component, OnInit} from '@angular/core';
import {CategoryService} from '../../../services/category.service'
import {Category} from '../../../models/category.model';
import {NgForOf} from '@angular/common';


@Component({
  selector: 'app-categories-admin',
  standalone: true,
  imports: [
    NgForOf
  ],
  templateUrl: './categories-admin.component.html',
  styleUrl: './categories-admin.component.css'
})
export class CategoriesAdminComponent implements OnInit {
  categories: Array<Category> = [];

  constructor(private categoryService: CategoryService) {}

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
