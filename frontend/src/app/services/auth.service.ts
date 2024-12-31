import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment'
import {HttpClient} from '@angular/common/http';
import {User} from '../models/user.model'
import {BehaviorSubject, map, Observable, tap} from 'rxjs';
import {jwtDecode, JwtPayload} from 'jwt-decode'

interface LoginResponse {
  token: string;
  //username: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl;
  currentUser?: string;

  constructor(private http: HttpClient) {}

  registerHandler() {
    // return this.http.post(this.apiUrl + 'auth' + '/register', JSON.stringify({}))
    return this.http.post<User>(`${this.apiUrl}auth/register`, JSON.stringify({}))
  }

  // Xử lý đăng nhập và lưu trữ thông tin sau khi đăng nhập thành công.
  loginHandler(credentials: {username: string, password: string}): Observable<LoginResponse>
  {
    return this.http.post<LoginResponse>(`${this.apiUrl}auth/login`,credentials).pipe(
      // tap operator để lưu token
      tap(response => {
        // Lưu token
        localStorage.setItem('token', response.token);
        if (response.token)
        {
          const decodedToken: any = jwtDecode<JwtPayload>(response.token); // returns with the JwtPayload type
          const username = decodedToken.name; // Lấy tên người dùng từ token
          const userRole: string = decodedToken.role; // Lấy vai trò user
          // this.currentUser = username;
          console.log(`Mã token: ${response.token}, tên người dùng là: ${username}, vai trò là: ${userRole}`);

          window.location.reload();
        }
      })
    );
  }

  // Kiểm tra xem người dùng đã đăng nhập hay chưa
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token'); // Nếu có token trong LocalStorage thì tức là đã đăng nhập
  }

  isAdmin(): boolean {
    const token: any  = localStorage.getItem('token');
    const decodedToken: any = jwtDecode<JwtPayload>(token);
    const userRole: string = decodedToken.role;

    if (userRole == "True") {return true}
    return false;
  }

  isUser(): boolean {
    const token: any  = localStorage.getItem('token');
    const decodedToken: any = jwtDecode<JwtPayload>(token);
    const userRole: string = decodedToken.role;

    if (userRole == "False") {return true}
    return false;
  }
  // Xoá toàn bộ thông tin đã đăng nhập
  logoutHandler() {
    localStorage.removeItem('token'); // Xoá token
    window.location.reload();
  }

  // getAccessToken: Lấy token

  // Lấy tên đăng nhập ở thời điểm hiện tại
  getCurrentUser(): string
  {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken: any = jwtDecode(token);
      this.currentUser = decodedToken.name;
    }
    return <string>this.currentUser;
  }

  // refreshToken: làm mới token
}
