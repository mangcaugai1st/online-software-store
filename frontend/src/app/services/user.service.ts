import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../models/user.model';
import { UpdateUserProfileDto } from '../models/updateUserProfileDto';
import {jwtDecode, JwtPayload} from 'jwt-decode';
import {throwError} from 'rxjs';

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

  // Hiển thị thông tin người dùng trong trang hồ sơ
  getUserById()
  {
    const token: string = localStorage.getItem('token');

    if (!token) {
      throw new Error("Trong trang profile không tìm thấy mã JWT Token");
    }

    const decodedToken = jwtDecode<JwtPayload>(token);
    const userId = decodedToken['nameid'];

    if (!userId) {
      return throwError(() => new Error('User id không tồn tại trong jwt token'));
    }

    const header = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    return this.http.get<User>(`${this.apiUrl}/users/${userId}`, { headers: header });
  }

  updateUserProfile(updateUserProfileDto: UpdateUserProfileDto) {
    const token: string = localStorage.getItem('token');

    if (!token) {
      throw new Error();
    }

    const decodedToken = jwtDecode<JwtPayload>(token);
    const userId = decodedToken['nameid'];

    if (!userId) {
      return throwError(() => new Error('User id không tồn tại trong jwt token'));
    }

    const header = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    return this.http.put<UpdateUserProfileDto>(`${this.apiUrl}/users/update`, updateUserProfileDto, { headers: header })
  }
}
