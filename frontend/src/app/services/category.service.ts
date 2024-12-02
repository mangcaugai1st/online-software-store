import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment'
import {Category} from '../models/category.model'

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  getCategories()
  {
    return this.http.get<Category[]>(environment.apiUrl + 'categories');
  }
}
