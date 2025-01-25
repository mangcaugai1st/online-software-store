import {Component, OnInit} from '@angular/core';
import { ProductService } from '../../../services/product.service'
import { Product } from '../../../models/product.model';
import { CreateNewProductsButtonComponent } from '../../shared/create-new-products-button/create-new-products-button.component'
import {NgForOf} from '@angular/common';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-products-admin',
  standalone: true,
  imports: [
    NgForOf,
    CreateNewProductsButtonComponent,
    RouterLink,
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
