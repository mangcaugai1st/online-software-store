import { Routes } from '@angular/router';
import {HomeComponent} from './user/modules/home/home.component';
import {LoginComponent} from './user/modules/login/login.component';
import {ProductsComponent} from './user/shared/components/products/products.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: ':slug', component: ProductsComponent },
];
