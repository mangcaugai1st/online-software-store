import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../environments/environment'
import {Category} from '../models/category.model'

@Injectable({
  providedIn: 'root'
})

export class CategoryService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // Danh sách danh mục
  getCategories()
  {
    return this.http.get<Category[]>(`${this.apiUrl}categories`);
  }

  // Thêm mới danh mục sản phẩm
  addNewCategory(category: Category)
  {
    return this.http.post<Category>(`${this.apiUrl}categories`, category);
  }

  // Cập nhật danh mục
  updateExistedCategory(category: Category)
  {
    return this.http.put<Category>(`${this.apiUrl}categories/` + category.id, category);
  }

  // Xóa danh mục
  deleteCategory(categoryId: number) {
    return this.http.delete(`${this.apiUrl}categories/`+ categoryId);
  }
}
