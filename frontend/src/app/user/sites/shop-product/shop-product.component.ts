import {Component, OnInit} from '@angular/core';
import {Product} from '../../../models/product.model';
import {ProductService} from '../../../services/product.service';
import {RouterLink} from '@angular/router';
import {NgForOf, NgIf} from '@angular/common';

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

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.getAllProductsInShop();
  }

  getAllProductsInShop() {
    this.productService.getProducts().subscribe({
      next: (products: Product[]) => {this.products = products;},
      error: (error) => console.log("Không có sản phẩm nào \n", error)
    });
  }
}
