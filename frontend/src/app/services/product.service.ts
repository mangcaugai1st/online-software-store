import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Product} from '../models/product.model';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

    getProducts(sortOrder: string = 'asc')
    {
      const params = new HttpParams().set('sortOrder', sortOrder);
      return this.http.get<Product[]>(`${this.apiUrl}/products`);
    }

    getProductsByCategory(categorySlug: string)
    {
      return this.http.get<Product[]>(`${this.apiUrl}/categories/category/${categorySlug}`);
    }

    getProductBySlugName(productSlug: string)
    {
      return this.http.get<Product[]>(`${this.apiUrl}/products/detail/${productSlug}`);
    }

    getProductDetailsById(productId: number)
    {
      return this.http.get<Product>(`${this.apiUrl}/products/product/${productId}`);
    }

    createNewProduct(productData: FormData)
    {
      return this.http.post<Product>(`${this.apiUrl}/products`, productData);
    }

    updateExistedProduct(productId: number, productData: FormData)
    {
      return this.http.put<Product>(`${this.apiUrl}/products/product/${productId}`, productData);
    }

    deleteProduct(productId: number)
    {
      return this.http.delete(`${this.apiUrl}/products/delete_product/${productId}`);
    }

    searchProducts(query: string): Observable<Product[]>
    {
      const searchUrl = `${this.apiUrl}/products/search`;
      const params = new HttpParams().set('query', query);

      return this.http.get<Product[]>(searchUrl, { params: params });
    }
}
