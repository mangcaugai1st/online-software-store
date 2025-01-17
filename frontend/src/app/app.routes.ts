import { Routes } from '@angular/router';
import { HomeComponent } from './user/modules/home/home.component';
import { LoginComponent } from './user/modules/login/login.component';
import { RegisterComponent } from './user/modules/register/register.component';
import { ProductListComponent } from './user/modules/product-list/product-list.component';
import { ProductsComponent } from './user/shared/components/products/products.component';
import { DetailProductComponent } from './user/shared/components/detail-product/detail-product.component';
import { DashboardComponent } from './admin/modules/dashboard/dashboard.component'
import { adminGuard } from './guards/admin.guard'
import {userGuard} from './guards/user.guard'
import { ProductsAdminComponent } from './admin/modules/products-admin/products-admin.component'
import { CreateNewProductsComponent } from './admin/modules/products-admin/create-new-products/create-new-products.component'

import { CategoriesAdminComponent } from './admin/modules/categories-admin/categories-admin.component'
import { AdminComponent } from './admin/admin.component';
import { UserComponent } from './user/user.component';

export const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'login', component: LoginComponent},
  { path: 'dang_ky', component: RegisterComponent},
  { path: ':categorySlug', component: ProductListComponent},
  { path: 'product/:productSlug', component: DetailProductComponent},

  // {
  //   path: 'user',
  //   component: UserComponent,
  //   children: [
  //     {
  //       path: 'home',
  //       component: HomeComponent
  //     },
  //     {
  //       path: 'login',
  //       component: LoginComponent
  //     },
  //     {
  //       path: ':categorySlug',
  //       component: ProductListComponent
  //     },
  //     {
  //       path: 'product/:productSlug',
  //       component: DetailProductComponent
  //     },
  //   ]
  // },

  { path: 'admin', component: AdminComponent, canActivate:[adminGuard],
    // canActivate: [adminGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent, canActivate:[adminGuard] },
      { path: 'products_management', component: ProductsAdminComponent, canActivate:[adminGuard] },
      { path: 'create_new_product', component: CreateNewProductsComponent, canActivate:[adminGuard] },
      { path: 'categories', component: CategoriesAdminComponent, canActivate:[adminGuard]},
      // { path: 'users', component: AdminUsersComponent },
    ]
  },
  // {path: '', redirectTo: '', pathMatch: 'full'}
];
