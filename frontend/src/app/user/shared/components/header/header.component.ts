import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {RouterLink} from '@angular/router';
import {CategoriesComponent} from '../categories/categories.component';
import { SearchProductPipe } from '../../../../pipes/search-product.pipe'
import { ProductService } from '../../../../services/product.service'
import {Product} from '../../../../models/product.model';
import {FormsModule} from '@angular/forms';
import {AsyncPipe, NgForOf, NgIf} from '@angular/common';
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
    AsyncPipe
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {

  searchText: string = '';
  products: Product[] = [];
  username?: string;
  isLoggedIn: boolean = false;

  @ViewChild('toggleOpen', { static: true }) toggleOpen!: ElementRef;
  @ViewChild('toggleClose', { static: true }) toggleClose!: ElementRef;
  @ViewChild('collapseMenu', { static: true }) headerContent!: ElementRef;

  constructor(
    private productService : ProductService,
    private authService: AuthService,
  ) {}

  // currentUser$!: Observable<any>;

  ngOnInit() {
    this.GetProducts()
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

  GetProducts() {
     this.productService.getProducts().subscribe({
       next: (products: Product[]) => { this.products = products },
     })
  }

  onLogout() {
    this.authService.logoutHandler(); // Xóa token
  }

  // isLogged() {
  //   this.authService.currentIsLogged.subscribe(isLogged => {this.isLogged = isLogged})
  // }
}
