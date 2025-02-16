import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {CartService} from '../../../services/cart.service';
import {Cart} from '../../../models/cart.model';
import {CurrencyPipe, NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [
    NgForOf,
    CurrencyPipe,
    NgIf
  ],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {
  cartItems: Cart[] = [];
  loading: boolean = false;
  error: string | null = null;
  shippingFee = 25000; // VND
  increase1ProductQuantity = 1;
  decrease1ProductQuantity = 1;

  constructor(
    private http: HttpClient,
    private cartService: CartService,
  ) { }

  ngOnInit() {
    this.getCart();
  }

  getCart(): void {
    this.cartService.getProductsInCart().subscribe({
      next: (cartItems: Cart[]) => {
        this.cartItems = cartItems;
      },
      error: (error) => {
        this.error = "Không thể tải giỏ hàng. Vui lòng thử lại sau.";
        this.loading = false;
        console.error(error);
      }
    });
  }

  // Tăng số lượng sản phẩm trong giỏ hàng
  increaseQuantity(productId: number): void {
    this.cartService.increaseQuantity(productId, this.increase1ProductQuantity).subscribe({
      next: (response) => {
        this.increase1ProductQuantity++;
        console.log('+1 sản phẩm thêm thành công', response);
        window.location.reload();
      },
      error: (error) => {
        console.error('Đã xảy ra lỗi: ', error.message);
      }
    });
  }

  decreaseQuantity(productId: number): void {
    this.cartService.decreaseQuantity(productId, this.decrease1ProductQuantity).subscribe({
      next: (response) => {
        this.decrease1ProductQuantity--;
        window.location.reload();
      },
      error: (error) => {
        console.log('Đã xảy ra lỗi: ', error.message);
      }
    })
  }

  getSubtotal(): number {
    return this.cartItems.reduce((total, item) => total + (item.product.price * item.quantity), 0);
  }

  getTotal(): number {
    return this.getSubtotal() + this.shippingFee;
  }

  removeItem(item: Cart): void {

  }

  checkout() {

  }
}
