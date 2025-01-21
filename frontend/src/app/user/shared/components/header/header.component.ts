import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {RouterLink} from '@angular/router';
import {CategoriesComponent} from '../categories/categories.component';
import { SearchProductPipe } from '../../../../pipes/search-product.pipe'
import { ProductService } from '../../../../services/product.service'
import {Product} from '../../../../models/product.model';
import {FormsModule} from '@angular/forms';
import {AsyncPipe, CurrencyPipe, NgForOf, NgIf} from '@angular/common';
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
    AsyncPipe,
    CurrencyPipe
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  searchQuery: string = '';
  products = [];
  username?: string;
  isLoggedIn: boolean = false;
  noResults = false;
  emptyList = []

  @ViewChild('toggleOpen', { static: true }) toggleOpen!: ElementRef;
  @ViewChild('toggleClose', { static: true }) toggleClose!: ElementRef;
  @ViewChild('collapseMenu', { static: true }) headerContent!: ElementRef;

  constructor(
    private productService : ProductService,
    private authService: AuthService,
  ) {}

  // currentUser$!: Observable<any>;

  ngOnInit() {
    this.loadProducts()
    // this.currentUser$ = this.authService.currentUser$;
    this.getUsernameAfterLogin();
    this.isLoggedIn = this.authService.isLoggedIn();
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
    if (this.searchQuery.trim()) {
      this.products = this.products.filter(product =>
        product.name.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    } else {
      this.emptyList;
    }
  }

  // Đăng xuất
  onLogout() {
    this.authService.logoutHandler(); // Xóa token
  }

  // isLogged() {
  //   this.authService.currentIsLogged.subscribe(isLogged => {this.isLogged = isLogged})
  // }
}
