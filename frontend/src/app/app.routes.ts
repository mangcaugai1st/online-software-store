import { Routes } from '@angular/router';
import {HomeComponent} from './user/modules/home/home.component';
import {LoginComponent} from './user/modules/login/login.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
];
