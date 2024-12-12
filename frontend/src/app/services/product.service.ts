import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Product} from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

    getProducts()
    {
      return this.http.get<Product[]>(`${this.apiUrl}products`);
    }

    getProductsByCategory(categorySlug: string)
    {
      return this.http.get<Product[]>(`${this.apiUrl}categories/category/${categorySlug}`);
    }

    getProductBySlugName(productSlug: string)
    {
      return this.http.get<Product[]>(`${this.apiUrl}products/product/${productSlug}`);
    }
}
