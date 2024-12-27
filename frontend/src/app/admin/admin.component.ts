import {Component, OnInit} from '@angular/core';
import { AuthService } from '../services/auth.service';
import {NgIf} from '@angular/common';
import {AdminHeaderComponent} from './shared/admin-header/admin-header.component';
import {AdminSidebarComponent} from './shared/admin-sidebar/admin-sidebar.component';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    NgIf,
    AdminHeaderComponent,
    AdminSidebarComponent,
    RouterOutlet,
  ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent implements OnInit {
  isAuthenticated: boolean = false;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    // this.isAuthenticated = this.authService.getAuthStatus();
  }
  //
  // logout(): void {
  //   this.authService.logout();
  //   this.isAuthenticated = false;
  // }
}
