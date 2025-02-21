import {Component, OnInit} from '@angular/core';
import { Category } from '../../../../models/category.model'
import {CategoryService} from '../../../../services/category.service';
import {ProductService} from '../../../../services/product.service';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';
import {SubscriptionType} from '../../../../enums/subscription-type.enum';

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
  subscriptionTypes = Object.keys(SubscriptionType).filter(key => isNaN(Number(key))); // lọc các giá trị là chuỗi (không phải là số)
  selectedSubscription: SubscriptionType;

  selectedSubscriptionTypeInOption: number;

  productForm: FormGroup;

  imagePreview: string | undefined;
  selectedFile: File | null = null;

  /*
  * Tạo ra một đối tượng để ánh xạ
  * các giá trị của enum
  * */
  displaySubscriptionTypesInVietnamese = {
    [SubscriptionType.Perpetual]: 'Vĩnh viễn',
    [SubscriptionType.Rental]: 'Thuê'
  }

  constructor(
    private categoryService : CategoryService,
    private productService: ProductService,
    private formGroup: FormBuilder,
  ) {
    this.productForm = this.formGroup.group({
      name: ['', Validators.required],
      categoryId: [null, Validators.required],
      price: [''],
      subscriptionType: ['', Validators.required],
      monthlyRentalPrice: [''],
      yearlyRentalPrice: [''],
      discount: [''],
      slug: ['', Validators.required],
      imagePath: [null],
      description: [''],
      stockQuantity: ['', Validators.required],
      isActive: [true, Validators.required],
    });

    // Khởi tạo giá trị mặc định là "vĩnh viễn".
    this.selectedSubscription =  SubscriptionType.Perpetual;
  }

  ngOnInit(): void {
    this.loadCategories();
    this.productForm.get('subscriptionType')?.valueChanges.subscribe(value => {
      this.onOptionChange();
    })
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
      formData.append("price", this.productForm.get("price")?.value);
      formData.append("subscriptionType", this.productForm.get("subscriptionType")?.value);
      formData.append("monthlyRentalPrice", this.productForm.get("monthlyRentalPrice")?.value);
      formData.append("yearlyRentalPrice", this.productForm.get("yearlyRentalPrice")?.value);
      formData.append("discount", this.productForm.get("discount")?.value);
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

  isRentalDisabled = false;
  isPerpetualDisabled = false;

  // Hàm cập nhật trạng thái disable của input khi thay đổi giá trị trong select
  onOptionChange() {
    const optionValue = this.productForm.get("subscriptionType").value;

    if (optionValue == 0) {
      this.isRentalDisabled = true;
      this.isPerpetualDisabled = false;
      this.productForm.get('monthlyRentalPrice')?.disable();
      this.productForm.get('yearlyRentalPrice')?.disable();
      this.productForm.get('price')?.enable();
    }
    else
    {
      this.isRentalDisabled = false;
      this.isPerpetualDisabled = true;
      this.productForm.get('monthlyRentalPrice')?.enable();
      this.productForm.get('yearlyRentalPrice')?.enable();
      this.productForm.get('price')?.disable();
    }
  }

  // isButtonDisabled() {
  //   return this.isPerpetualDisabled || this.isRentalDisabled; // isPerpetualDisabled = false, isRentalDisabled = false
  // }

  protected readonly SubscriptionType = SubscriptionType;
}
