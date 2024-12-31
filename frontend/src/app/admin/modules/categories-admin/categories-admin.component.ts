import {Component, OnInit} from '@angular/core';
import {CategoryService} from '../../../services/category.service'
import {Category} from '../../../models/category.model';
import {NgForOf, NgIf} from '@angular/common';


@Component({
  selector: 'app-categories-admin',
  standalone: true,
  imports: [
    NgForOf,
    NgIf
  ],
  templateUrl: './categories-admin.component.html',
  styleUrl: './categories-admin.component.css'
})
export class CategoriesAdminComponent implements OnInit {
  categories: Array<Category> = [];

  isVisible: boolean = false;

  constructor(private categoryService: CategoryService) {}

  ngOnInit() {
    this.GetCategories();

    this.openModal();
    this.closeModal();
  }

  GetCategories () {
    this.categoryService.getCategories().subscribe({
      next : (categories: Category[]) => { this.categories = categories },
      error : error => console.log(error)
    })
  }
  // Mở modal
  openModal() {
    this.isVisible = true;
  }

  // Đóng modal
  closeModal() {
    this.isVisible = false;
  }

  protected readonly close = close;
}
