import { Routes } from '@angular/router';
import {HomeComponent} from './user/modules/home/home.component';
import {LoginComponent} from './user/modules/login/login.component';
import {ProductsComponent} from './user/shared/components/products/products.component';
import {DetailProductComponent} from './user/shared/components/detail-product/detail-product.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: ':categorySlug', component: ProductsComponent },
  { path: 'product/:productSlug', component: DetailProductComponent },
];
