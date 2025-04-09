import {Component, OnInit, Input} from '@angular/core';
import {Product} from '../../../../models/product.model';
import {ProductService} from '../../../../services/product.service'
import {NgForOf, NgIf, CurrencyPipe} from '@angular/common';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {CartService} from '../../../../services/cart.service';


@Component({
  selector: 'app-products',
  standalone: true,
    imports: [
        NgForOf,
        RouterLink,
        NgIf,
        CurrencyPipe,
    ],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  slug: string = ''; // Slug danh mục từ URL
  productId: number;
  quantity: number = 1; // Khởi tạo giá trị số lượng sản phẩm ban đầu là 1

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    // Lấy slug từ route parameter
    this.route.paramMap.subscribe(params => {
      this.slug = params.get('categorySlug')!;
      this.loadProducts();
    });
  }

  // Hàm tải sản phẩm theo slug
  loadProducts(): void {
    this.productService.getProductsByCategory(this.slug).subscribe(
      (data: Product[]) => {
        this.products = data;
        console.log(data);
      },
      (error) => {
        console.error('Lỗi khi lấy sản phẩm:', error);
      }
    );
  }

  addToCart(productId: number, quantity: number): void {
    // gọi service để thêm sản phẩm vào giỏ hàng
    this.cartService.addToCart(productId, quantity).subscribe({
      next: (respond) => {
        console.log('Sản phẩm đã được thêm vào giỏ hàng ', respond);
        alert('Thêm thành công');
      },
      error: (err) => {
        console.log('Lỗi khi thêm sản phẩm vào giỏ hàng', err);
        alert('LỖI thêm sản phẩm vào giỏ hàng');
      }
    });
  }
}
