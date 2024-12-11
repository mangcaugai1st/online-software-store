import {Inject, Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {Category} from '../models/category.model'
import {HttpClient} from '@angular/common/http';
import {Product} from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

    getProductsByCategory(categorySlug: string)
    {
      return this.http.get<Product[]>(`${this.apiUrl}categories/category/${categorySlug}`);
    }
}
