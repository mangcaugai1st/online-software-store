import {Component, OnInit} from '@angular/core';
import {ProductService} from '../../../services/product.service'
import {Product} from '../../../models/product.model';
import {NgForOf} from '@angular/common';

@Component({
  selector: 'app-products-admin',
  standalone: true,
  imports: [
    NgForOf
  ],
  templateUrl: './products-admin.component.html',
  styleUrl: './products-admin.component.css'
})
export class ProductsAdminComponent implements OnInit {
  products: Array<Product> = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.productService.getProducts().subscribe({
      next: (products: Product[]) => {this.products = products;},
      error: error => console.log(error)
    })
  }

}
