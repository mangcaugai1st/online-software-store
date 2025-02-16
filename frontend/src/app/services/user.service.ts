import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getUsers()
  {
    return this.http.get<User[]>(`${this.apiUrl}/users`);
  }

  getUserById(id: string)
  {
    return this.http.get<User[]>(`${this.apiUrl}/users/${id}`);
  }

}
