import {Component, OnInit, Input} from '@angular/core';
import {Product} from '../../../../models/product.model';
import {ProductService} from '../../../../services/product.service'
import {NgForOf} from '@angular/common';
import {ActivatedRoute, RouterLink} from '@angular/router';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink
  ],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  slug: string = ''; // Slug danh mục từ URL

  constructor(
    private productService: ProductService,
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
}
