import {Component, OnInit} from '@angular/core';
import {Category} from '../../../../models/category.model'
import {CategoryService} from '../../../../services/category.service'
import {NgForOf} from '@angular/common';
@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [
    NgForOf
  ],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent implements OnInit{
  categories : Category[]  = [];

  constructor(private categoryService : CategoryService) {}

  ngOnInit() {
    this.GetCategories();
  }

  GetCategories(){
    this.categoryService.getCategories().subscribe({
      next : (categories) =>{
        this.categories = categories
      },
      error : error => console.log(error)
    })
  }
}
