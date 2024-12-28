import {CanActivateFn} from '@angular/router';
import {AuthService} from '../services/auth.service';
import {inject} from '@angular/core';

export const adminGuard: CanActivateFn = () => {
  const authService = inject(AuthService); // inject AuthService để kiểm tra quyền
  const isAdmin = authService.isAdmin();

  if (isAdmin)
  {
    return true; // Người dùng là admin, cho phép truy cập
  }
  else {
     return false; // Người dùng không phải là admin, từ chối truy cập
  }
};


// export class AdminGuard implements CanActivateFn {
//   constructor(private authService: AuthService, private router: Router) {}
//
//   canActive(
//     route: ActivatedRouteSnapshot,
//     state: RouterStateSnapshot
//   ): Observable<boolean> | Promise<boolean> | boolean {
//     if (this.authService.isAdmin()) {
//       return true;
//     }
//     else {
//       this.router.navigate(['']);
//       return false;
//     }
//   }
// }
// CanActivate
// Là một interface được sử dụng để tạo guard dựa trên class
// Yêu cầu tạo một class và implement interface
// Thường được sử dụng trong angular < 14

// CanActivateFn
// Là một function type, không cần tạo class
// Được giới thiệu từ Angular 14 như một cách đơn giản hóa
// Giúp code ngắn gọn và dễ test hơn.
