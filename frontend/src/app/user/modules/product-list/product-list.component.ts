import { Component } from '@angular/core';
import {CategoriesComponent} from "../../shared/components/categories/categories.component";
import {NgForOf} from "@angular/common";
import {ProductsComponent} from '../../shared/components/products/products.component';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CategoriesComponent,
    NgForOf,
    ProductsComponent,
    RouterOutlet
  ],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent { }
