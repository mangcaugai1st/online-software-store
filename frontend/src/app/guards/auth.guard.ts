import {
  CanActivateFn,
  CanActivate,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  MaybeAsync, GuardResult
} from '@angular/router';
import {Injectable} from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {

  return true;
};

