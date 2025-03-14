import {Component, OnInit} from '@angular/core';
import { Category } from '../../../../models/category.model';
import { SubscriptionType } from '../../../../models/product.model';
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

  /*
  * Chuyển enum thành array để dùng *ngFor
  -   Object.keys(SubscriptionType) trả về một mảng chứa các keys của enum SubscriptionType.
  -   Ví dụ: enum SubscriptionType { Basic = 1, Premium = 2, Enterprise = 3 }
    thì sẽ được trả về mảng [ 'Basic', 'Premium', 'Enterprise', '1', '2', '3' ]
  -   .filter(key => isNaN(+key))
      +key chuyển mỗi key thành số. Ví dụ "1" thành 1, "Premium" vẫn là một chuỗi
  * */
  subscriptionTypes = Object.keys(SubscriptionType).filter(key => isNaN(+key));
  selectedSubscriptionType: SubscriptionType;   // Biến để lưu giá trị được chọn


  constructor(
    private categoryService : CategoryService,
    private productService: ProductService,
    private formGroup: FormBuilder,
  ) {
    this.productForm = this.formGroup.group({
      name: ['', Validators.required],
      categoryId: [null, Validators.required],
      subscriptionType: ['', Validators.required],
      price: [''],
      yearlyRentalPrice: [''],
      discount: [''],
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
    if (this.productForm.valid) {
      const formData = new FormData();
      formData.append("name", this.productForm.get("name")?.value);
      formData.append("categoryId", this.productForm.get("categoryId")?.value);
      formData.append("subscriptionType", this.productForm.get("subscriptionType")?.value);
      formData.append("price", this.productForm.get("price")?.value);
      formData.append("yearlyRentalPrice", this.productForm.get("yearlyRentalPrice")?.value);
      formData.append("slug", this.productForm.get("slug")?.value);
      formData.append("description", this.productForm.get("description")?.value);
      formData.append("stockQuantity", this.productForm.get("stockQuantity")?.value);
      formData.append("isActive", this.productForm.get("isActive")?.value);
      formData.append('imagePath', this.selectedFile)

      this.productService.createNewProduct(formData).subscribe(
        (response) => {
          console.log('Sản phẩm đã được tạo thành công:', response);
          console.log(formData);
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
