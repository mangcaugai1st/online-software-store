import {Component, OnInit} from '@angular/core';
import { Category } from '../../../../models/category.model'
import {CategoryService} from '../../../../services/category.service';
import {ProductService} from '../../../../services/product.service';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';
@Component({
  selector: 'app-create-new-products',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './create-new-products.component.html',
  styleUrl: './create-new-products.component.css'
})
export class CreateNewProductsComponent implements OnInit {
  categories: Category[];
  productForm: FormGroup;

  imagePreview: string | undefined;
  selectedFile: File | null = null;

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
      imagePath: [null],
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

  onFileSelect(event: any): void {
    // if (event.target.files && event.target.files[0]) {
    //   this.selectedFile = event.target.files[0];
    //
    //   const reader = new FileReader();
    //   reader.onload = (e) => {
    //     this.imagePreview = e.target.result as string;
    //   };
    //   reader.readAsDataURL(this.selectedFile);
    const file: any = event.target.files[0];
    this.selectedFile = file;

    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.imagePreview = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit(): void {
    // if (this.productForm.valid) {
    //   this.productService.createNewProduct(this.productForm.value).subscribe(response => {
    //     console.log('Sản phẩm đã được tạo', response);
    //   });
    // }
    if (this.productForm.valid) {
      const formData = new FormData();
      formData.append("name", this.productForm.get("name")?.value);
      formData.append("categoryId", this.productForm.get("categoryId")?.value);
      formData.append("price", this.productForm.get("price")?.value);
      formData.append("slug", this.productForm.get("slug")?.value);
      formData.append("description", this.productForm.get("description")?.value);
      formData.append("stockQuantity", this.productForm.get("stockQuantity")?.value);
      formData.append("isActive", this.productForm.get("isActive")?.value);
      formData.append('imagePath', this.selectedFile)

      // if (this.selectedFile) {
      //   formData.append('imagePath', this.selectedFile);
      // }

      this.productService.createNewProduct(formData).subscribe(
        (response) => {
          console.log('Sản phẩm đã được tạo thành công:', response);
          this.productForm.reset();
          this.selectedFile = null;
          this.imagePreview = null
        },
        (error) => {
          console.error('Lỗi khi tạo sản phẩm', error);
        }
      );
    }
  }
}
