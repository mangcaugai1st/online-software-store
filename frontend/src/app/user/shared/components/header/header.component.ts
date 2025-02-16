import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {RouterLink} from '@angular/router';
import {CategoriesComponent} from '../categories/categories.component';
import { SearchProductPipe } from '../../../../pipes/search-product.pipe'
import { ProductService } from '../../../../services/product.service'
import { CartService } from '../../../../services/cart.service'
import {Product} from '../../../../models/product.model';
import {FormsModule} from '@angular/forms';
import { CurrencyPipe, NgForOf, NgIf } from '@angular/common';
import {AuthService} from '../../../../services/auth.service';
import {jwtDecode} from 'jwt-decode';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    CategoriesComponent,
    FormsModule,
    NgForOf,
    NgIf,
    CurrencyPipe
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  backendPath: string = 'http://localhost:5252';
  searchQuery: string = '';
  products = [];
  username?: string;
  isLoggedIn: boolean = false;
  errorMessage: string;
  totalQuantity: number | null = null;

  @ViewChild('toggleOpen', { static: true }) toggleOpen!: ElementRef;
  @ViewChild('toggleClose', { static: true }) toggleClose!: ElementRef;
  @ViewChild('collapseMenu', { static: true }) headerContent!: ElementRef;

  constructor(
    private productService : ProductService,
    private authService: AuthService,
    private cartService: CartService,
  ) {}

  ngOnInit() {
    this.loadProducts()
    // this.currentUser$ = this.authService.currentUser$;
    this.getUsernameAfterLogin();
    this.isLoggedIn = this.authService.isLoggedIn();
    this.getTotalQuantity();
  }

  isMenuVisible: boolean = false;

  handleClick() {
    this.isMenuVisible = !this.isMenuVisible;
  }

  protected readonly SearchProductPipe = SearchProductPipe;

  getUsernameAfterLogin() {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken: any = jwtDecode(token);
      this.username = decodedToken.name;
    }
  }

  // Liệt kê danh sách sản phẩm
  loadProducts() {
     this.productService.getProducts().subscribe({
       next: (products: Product[]) => { this.products = products },
     })
  }
  // Tìm kiếm sản phẩm
  onSearch(): void {
    if (this.searchQuery.trim() === '') {
      this.products = [];
      this.errorMessage = 'Search query cannot be empty.';
      return;
    }

    this.productService.searchProducts(this.searchQuery).subscribe(
      (data) => {
        this.products = data;
        this.errorMessage = ''; // Xóa thông báo lỗi nếu tìm kiếm thành công
      },
      (error) => {
        this.products = [];
        this.errorMessage = 'No products found matching the search criteria.';
      }
    );
  }

  // Đăng xuất
  onLogout() {
    this.authService.logoutHandler(); // Xóa token
  }

  getTotalQuantity() {
    this.cartService.getTotalQuantity().subscribe({
      next: (quantity: number) => {
        this.totalQuantity = quantity;
      },
      error: (error) => {
        this.errorMessage = error.Message || 'Something went wrong';
      }
    })
  }
}
