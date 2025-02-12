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
