import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {catchError, Observable, throwError} from 'rxjs';
import {JwtHeader, jwtDecode, JwtPayload} from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = environment.apiUrl + "cart";

  constructor(
    private http: HttpClient,
  ) { }

  addToCart(productId: number, quantity: number): Observable<any> {
    // Lấy JWT token từ LocalStorage
    const token = localStorage.getItem("token");

    if (!token) {
      throw new Error("JWT token is missing");
    }
    // Tạo header chứa JWT token
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
      'Content-Type': 'application/json'
    })

    // Dữ liệu yêu cầu
    const body = {
      productId: productId,
      quantity: quantity
    }

    // Gửi yêu cầu POST tới API
    return this.http.post(`${this.apiUrl}/add`, body, { headers: headers });
  }

  // Hiển thị toàn bộ danh sách sản phẩm trong giỏ hàng theo id người dùng
  getProductsInCart(): Observable<any> {
    const token = localStorage.getItem("token");

    if (!token) {
      throw new Error("JWT token is missing");
    }

    try {
      const decodedToken = jwtDecode<JwtPayload>(token);
      const userId = decodedToken['nameid'];

      if (!userId) {
        return throwError(() => new Error('User id không tồn tại trong token'));
      }

      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      });

      return this.http.get(`${this.apiUrl}/user${userId}`, { headers: headers })
        .pipe(
          catchError(error => {
            if (error.status == 401) {
              return throwError(() => new Error('Unauthorized'));
            }
            if (error.status == 403) {
              return throwError(() => new Error('Forbidden'));
            }
            return throwError(() => new Error('Failed to fetch cart items'));
          })
        ) ;
    }
    catch (error) {
      return throwError(error);
    }
  }
}
