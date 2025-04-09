import {Component, OnInit} from '@angular/core';
import {ProductService} from '../../../../services/product.service';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {CurrencyPipe, NgIf} from '@angular/common';
import {Product} from '../../../../models/product.model';
import {CartService} from '../../../../services/cart.service';


@Component({
  selector: 'app-detail-product',
  standalone: true,
  imports: [NgIf, CurrencyPipe],
  templateUrl: './detail-product.component.html',
  styleUrl: './detail-product.component.css'
})
export class DetailProductComponent implements OnInit {
  product: any;
  error: string | null = null;
  backendPath = "http://localhost:5252";

  quantity: number = 1;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartService:CartService,
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

  increaseQuantity() {
    this.quantity++;
  }

  decreaseQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
}
