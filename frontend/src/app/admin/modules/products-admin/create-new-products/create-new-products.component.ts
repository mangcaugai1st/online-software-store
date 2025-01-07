import {Component, OnInit} from '@angular/core';
import { Category } from '../../../../models/category.model'
import {CategoryService} from '../../../../services/category.service';
import {ProductService} from '../../../../services/product.service';
import {FormsModule} from '@angular/forms';
import {NgForOf} from '@angular/common';
@Component({
  selector: 'app-create-new-products',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf
  ],
  templateUrl: './create-new-products.component.html',
  styleUrl: './create-new-products.component.css'
})
export class CreateNewProductsComponent implements OnInit {
  categories: Category[];

  constructor(
    private categoryService : CategoryService,
    private productService: ProductService,
  ) { }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe(categories => this.categories = categories);
  }
}
