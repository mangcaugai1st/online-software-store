import {Component, OnInit} from '@angular/core';
import {Product} from '../../../models/product.model';
import {ProductService} from '../../../services/product.service';
import {RouterLink} from '@angular/router';
import {NgForOf, NgIf} from '@angular/common';
import {CartService} from '../../../services/cart.service';

@Component({
  selector: 'app-shop-product',
  standalone: true,
  imports: [
    RouterLink,
    NgForOf,
    NgIf
  ],
  templateUrl: './shop-product.component.html',
  styleUrl: './shop-product.component.css'
})
export class ShopProductComponent implements OnInit {
  products: Product[] = [];
  sortOrder: string = 'asc';  // Giá trị mặc định là ASC
  productId: number;
  quantity: number = 1;       // Khởi tạo giá trị số lượng sản phẩm ban đầu là 1

  constructor(private productService: ProductService,
              private cartService: CartService
  ) { }

  ngOnInit() {
    this.getAllProductsInShop();
  }

  getAllProductsInShop() {
    this.productService.getProducts(this.sortOrder).subscribe({
      next: (products: Product[]) => {this.products = products;},
      error: (error) => console.log("Không có sản phẩm nào \n", error)
    });
  }

  // Hàm thay đổi thứ tự sắp xếp
  toggleSortOrder() {
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
    this.getAllProductsInShop();
  }

  addToCart(productId: number, quantity: number): void {
    this.cartService.addToCart(productId, quantity).subscribe({
      next: (respond) => {
        console.log('Sản phẩm đã được thêm vào giỏ hàng ', respond);
      },
      error: (error) => {
        console.log('Lỗi khi thê sảm phẩm vào giỏ hàng');
      }
    });
  }
}
