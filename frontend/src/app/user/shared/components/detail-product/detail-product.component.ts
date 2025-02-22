import {Component, OnInit} from '@angular/core';
import {ProductService} from '../../../../services/product.service';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {CurrencyPipe, NgIf} from '@angular/common';
import {Product} from '../../../../models/product.model';
import {CartService} from '../../../../services/cart.service';
@Component({
  selector: 'app-detail-product',
  standalone: true,
  imports: [RouterLink, NgIf, CurrencyPipe],
  templateUrl: './detail-product.component.html',
  styleUrl: './detail-product.component.css'
})
export class DetailProductComponent implements OnInit {
  product: any;
  error: string | null = null;
  backendPath = "http://localhost:5252";    // URL của backend
  showMonthlyRentalPrice: boolean = false;
  showYearlyRentalPrice: boolean = false;
  selectedTimeToRental: number;                     // Chọn giá thuê theo thời hạn (theo tháng hoặc năm)
  quantity: number = 1;                             // Khởi tạo giá trị mặc định của số lượng sản phẩm là 1

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartService: CartService
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const productSlug = params.get('productSlug');

      if (productSlug) {
        this.productService.getProductBySlugName(productSlug)
          .subscribe(data=> {
            this.product = data;
          });
      }
    });
  }

  loadProductDetailBySlugName(productSlug: string) {
    this.productService.getProductBySlugName(productSlug)
    .subscribe(
      (data: Product[]) => {
        this.product = data;
      },
      (error) => {
        console.error('Lỗi không lấy được chi tiết sản phẩm:', error);
      }
    )
  }

  // Cập nhật giá khi người dùng chọn thời hạn thuê
  setPrice(type: string): void {
    if (type === 'monthly') {
      this.selectedTimeToRental = this.product.monthlyRentalPrice;
      this.showMonthlyRentalPrice = true;
      this.showYearlyRentalPrice = false;
    }
    else if (type === 'yearly') {
      this.selectedTimeToRental = this.product.yearlyRentalPrice;
      this.showMonthlyRentalPrice = false;
      this.showYearlyRentalPrice = true;
    }
  }

  // Tăng 1 số lượng sản phẩm trong giỏ hàng
  increaseQuantity(productId: number): void {
    this.quantity++;
  }

  // Giảm 1 số lượng sản phẩm trong giỏ hàng
  decreaseQuantity(productId: number): void {
    this.quantity--;
  }

  // Thêm vào giỏ hàng
  addToCart(productId: number, quantity: number): void {
    this.cartService.addToCart(productId, quantity).subscribe({
      next: (respond) => {
        console.log('Sản phẩm đã được thêm vào giỏ hàng', respond);
      },
      error: (error) => {
        alert('Lỗi thêm sản phẩm vào giỏ hàng.');
      }
    })
  }

  // addToCart(productId: number, subscriptionType: any,  monthlyRentalPrice: number, yearlyRentalPrice: number, quantity: number): void {
  //   this.cartService.addToCartInProductDetails(productId, subscriptionType)
  // }
}
