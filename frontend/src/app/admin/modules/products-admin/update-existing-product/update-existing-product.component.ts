import {Component, OnInit} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {Category} from '../../../../models/category.model';
import {CategoryService} from '../../../../services/category.service';
import {ProductService} from '../../../../services/product.service';
import {ActivatedRoute} from '@angular/router';
import {Product, SubscriptionType} from '../../../../models/product.model';

@Component({
  selector: 'app-update-existing-product',
  standalone: true,
    imports: [
        NgForOf,
        NgIf,
        ReactiveFormsModule,
        FormsModule
    ],
  templateUrl: './update-existing-product.component.html',
  styleUrl: './update-existing-product.component.css'
})
export class UpdateExistingProductComponent implements OnInit {
  productId: number;
  categories: Category[];
  editProductForm: FormGroup;
  imagePreview: string | undefined;
  selectedFile: File | null = null;
  // Lấy chi tiết sản phẩm
  productDetails: Product;

  subscriptionTypes = Object.keys(SubscriptionType).filter(key => isNaN(+key));

  constructor(
    private categoryService: CategoryService,
    private productService: ProductService,
    private formGroup: FormBuilder,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.loadCategories();
    // Lấy id của sản phẩm trên param
    this.route.paramMap.subscribe(params=> {
      this.productId = parseInt(params.get('productId'));
      console.log(params.get('productId'));
    });
    this.getProductDetailsById();
    this.editProductForm = this.formGroup.group({
      name: ['', Validators.required],
      categoryId: [null, Validators.required],
      price: ['', Validators.required],
      slug: ['', Validators.required],
      imagePath: [null],
      description: [''],
      stockQuantity: ['', Validators.required],
      isActive: [true, Validators.required],
    });

  }

  loadCategories() {
    this.categoryService.getCategories().subscribe(categories => this.categories = categories);
  }

  getProductDetailsById() {
    this.productService.getProductDetailsById(this.productId)
      .subscribe(product => {this.productDetails = product
        console.log(product)})
  }

  getCategoryName(categoryId: number): string {
    const category = this.categories.find(c => c.id === categoryId);
    return category ? category.name : 'Không có danh mục';
  }

  onFileSelect(event: any): void {
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
    if (this.editProductForm.invalid) {
      return; // Nếu form không hợp lệ thì sẽ không tiếp tục
    }
    else if (this.editProductForm.valid) {
      const formData = new FormData();
      // Thêm dữ liệu từ form vào FormData
      formData.append("name", this.editProductForm.get("name")?.value);
      formData.append("categoryId", this.editProductForm.get("categoryId")?.value);
      formData.append("price", this.editProductForm.get("price")?.value);
      formData.append("slug", this.editProductForm.get("slug")?.value);
      formData.append("description", this.editProductForm.get("description")?.value);
      formData.append("stockQuantity", this.editProductForm.get("stockQuantity")?.value);
      formData.append("isActive", this.editProductForm.get("isActive")?.value);
      formData.append('imagePath', this.selectedFile)

      this.productService.updateExistedProduct(this.productId, formData).subscribe(
        (response) => {
          console.log('Sản phẩm đã cập nhật thành công: ',response);
          console.log(formData);
          // Chuyển hướng về trang quản lý sản phẩm
          // this.router.navigate(['']);
        },
        (error) => {
          console.error('Lỗi khi tạo sản phẩm', error);
        }
      )
    }
  }
}
