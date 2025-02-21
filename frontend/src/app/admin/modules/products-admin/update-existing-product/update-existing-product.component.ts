import {Component, OnInit} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {Category} from '../../../../models/category.model';
import {CategoryService} from '../../../../services/category.service';
import {ProductService} from '../../../../services/product.service';
import {ActivatedRoute} from '@angular/router';
import {Product} from '../../../../models/product.model';
import {SubscriptionType} from '../../../../enums/subscription-type.enum';
import {Observable} from 'rxjs';

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
  categories: Category[] = [];                // Danh mục sản phẩm
  editProductForm: FormGroup;                 // Form cập nhật thông tin sản phẩm
  imagePreview: string | undefined;           // Xem hình ảnh
  selectedFile: File | null = null;           //
  productDetails: Product = {} as Product;    // = {} as Product: Phần này thực hiện khởi tạo giá trị cho biến productDetails. Dấu {} tạo ra một đối tượng trống và as Product là cú pháp TypeScript để ép kiểu (type assertion), nói rằng đối tượng trống này sẽ có kiểu là Product.
  subscriptionTypes = Object.keys(SubscriptionType).filter(key => isNaN(Number(key))); // lọc các giá trị là chuỗi (không phải là số)
  backendApi: string = "http://localhost:5252/";

  constructor(
    private categoryService: CategoryService,
    private productService: ProductService,
    private formGroup: FormBuilder,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    // Lấy productId từ param
    const productId = this.route.snapshot.paramMap.get('productId');
    // Lấy thông tin sản phẩm theo id từ API
    this.productService.getProductById(parseInt(productId)).subscribe(
      (product) => {
        this.editProductForm = this.formGroup.group({
          name: [product.name, Validators.required],
          categoryId: [product.categoryId, Validators.required],
          price: [product.price],
          subscriptionType: [product.subscriptionType, Validators.required],
          monthlyRentalPrice: [product.monthlyRentalPrice],
          yearlyRentalPrice: [product.yearlyRentalPrice],
          discount: [product.discount, Validators.required],
          slug: [product.slug, Validators.required],
          imagePath: [product.imagePath, Validators.required],
          description: [product.description],
          stockQuantity: [product.stockQuantity, Validators.required],
          isActive: [product.isActive, Validators.required],
        });
      },
      (error) => {
        console.log('Error fetching product data ',error);
      }
    );

    // Liệt kê toàn bộ danh mục sản phẩm
    this.categoryService.getCategories().subscribe(categories => this.categories = categories);

    // Lấy id của sản phẩm trên param từ localhost/api/products/update_product/{productId}
    // this.route.paramMap.subscribe(params=> {
    //   this.productId = parseInt(params.get('productId'));
    //   console.log(params.get('productId'));
    // });

    // this.getProductDetailsById(); // Hiển thị những thông tin chi tiết của sản phẩm cụ thể

    // this.editProductForm = this.formGroup.group({
    //   name: [this.productDetails.name, Validators.required],
    //   categoryId: [this.productDetails.categoryId, Validators.required],
    //   price: [this.productDetails.price],
    //   subscriptionType: ['', Validators.required],
    //   monthlyRentalPrice: [''],
    //   yearlyRentalPrice: [''],
    //   discount: [''],
    //   slug: ['', Validators.required],
    //   imagePath: [null],
    //   description: [''],
    //   stockQuantity: ['', Validators.required],
    //   isActive: [true, Validators.required],
    // });
  }



  // getProductDetailsById() {
  //   this.productService.getProductDetailsById(this.productId)
  //     .subscribe(product => {this.productDetails = product
  //       console.log(product)})
  // }

  // getProductById(id: number): Observable<Product> {
  //   return this.productService.getProductById(id);
  // }

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

  displaySubscriptionTypesInVietnamese = {
    [SubscriptionType.Perpetual]: 'Vĩnh viễn',
    [SubscriptionType.Rental]: 'Thuê'
  }

  onSubmit(): void {
    if (this.editProductForm.invalid) {
      return; // Nếu form không hợp lệ thì sẽ không tiếp tục
    }
    else if (this.editProductForm.valid) {
      const formData = new FormData();
      const productId = this.route.snapshot.paramMap.get('productId');

      // Thêm dữ liệu từ form vào FormData
      formData.append("name", this.editProductForm.get("name")?.value);
      formData.append("categoryId", this.editProductForm.get("categoryId")?.value);
      formData.append("price", this.editProductForm.get("price")?.value);
      formData.append("subscriptionType", this.editProductForm.get("subscriptionType")?.value);
      formData.append("monthlyRentalPrice", this.editProductForm.get("monthlyRentalPrice")?.value);
      formData.append("yearlyRentalPrice", this.editProductForm.get("yearlyRentalPrice")?.value);
      formData.append("discount", this.editProductForm.get("discount")?.value);
      formData.append("slug", this.editProductForm.get("slug")?.value);
      formData.append("description", this.editProductForm.get("description")?.value);
      formData.append("stockQuantity", this.editProductForm.get("stockQuantity")?.value);
      formData.append("isActive", this.editProductForm.get("isActive")?.value);
      formData.append('imagePath', this.selectedFile)

      this.productService.updateExistedProduct(parseInt(productId), formData).subscribe(
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

  protected readonly SubscriptionType = SubscriptionType;
}
