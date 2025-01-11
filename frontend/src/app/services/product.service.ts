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

    getProductDetailsById(productId: number)
    {
      return this.http.get<Product>(`${this.apiUrl}products/product/${productId}`);
    }

    getProductDetailsBySlugName(productSlug: string)
    {
      return this.http.get<Product>(`${this.apiUrl}products/product/${productSlug}`);
    }

    // createNewProduct(productData: FormData)
    // {
    //   return this.http.post<Product>(`${this.apiUrl}products`, product);
    // }

    createNewProduct(productData: FormData)
   {
      return this.http.post<Product>(`${this.apiUrl}products`, productData);
   }
    updateExistedProduct(productId: number, product: Product)
    {
      return this.http.put<Product>(`${this.apiUrl}/products/product/${productId}`, product);
    }

    deleteProduct(productId: number)
    {
      return this.http.delete(`${this.apiUrl}products/product/${productId}`);
    }

}
