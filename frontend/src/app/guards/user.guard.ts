import { CanActivateFn } from '@angular/router';
import {AuthService} from '../services/auth.service';
import {inject} from '@angular/core';

export const userGuard: CanActivateFn = () => {
  const authService = inject(AuthService); // inject AuthService để kiểm tra quyền
  const isUser = authService.isUser();

  if (isUser) {
    return true;
  }
  else {
    return false;
  }
};
