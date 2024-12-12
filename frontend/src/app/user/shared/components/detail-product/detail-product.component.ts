import {Component, OnInit} from '@angular/core';
import {ProductService} from '../../../../services/product.service';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {NgIf} from '@angular/common';
import {Product} from '../../../../models/product.model';

@Component({
  selector: 'app-detail-product',
  standalone: true,
  imports: [RouterLink, NgIf],
  templateUrl: './detail-product.component.html',
  styleUrl: './detail-product.component.css'
})
export class DetailProductComponent implements OnInit {
  product: any;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
  ) {}

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
}