import {Component, OnInit} from '@angular/core';
import { Category } from '../../../../models/category.model'
import {CategoryService} from '../../../../services/category.service';
import {ProductService} from '../../../../services/product.service';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgForOf} from '@angular/common';
@Component({
  selector: 'app-create-new-products',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    ReactiveFormsModule
  ],
  templateUrl: './create-new-products.component.html',
  styleUrl: './create-new-products.component.css'
})
export class CreateNewProductsComponent implements OnInit {
  categories: Category[];
  productForm: FormGroup;

  constructor(
    private categoryService : CategoryService,
    private productService: ProductService,
    private formGroup: FormBuilder,
  ) {
    this.productForm = this.formGroup.group({
      name: ['', Validators.required],
      categoryId: [null, Validators.required],
      price: ['', Validators.required],
      slug: ['', Validators.required],
      imagePath: [''],
      description: [''],
      stockQuantity: ['', Validators.required],
      isActive: [true, Validators.required],
    })
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe(categories => this.categories = categories);
  }

  onSubmit() {
    if (this.productForm.valid) {
      this.productService.createNewProduct(this.productForm.value).subscribe(response => {
        console.log('Sản phẩm đã được tạo', response);
      });
    }
  }
}
