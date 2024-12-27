import {Component, OnInit} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {NgxSpinnerComponent, NgxSpinnerService} from 'ngx-spinner';
import {HeaderComponent} from './user/shared/components/header/header.component';
import {FooterComponent} from './user/shared/components/footer/footer.component';
import {AdminHeaderComponent} from './admin/shared/admin-header/admin-header.component';
// import {BrowserAnimationsModule, NoopAnimationsModule} from '@angular/platform-browser/animations';
import {AuthService} from './services/auth.service';
import {NgIf} from '@angular/common';
import {UserComponent} from './user/user.component'
import {AdminComponent} from './admin/admin.component'
import {AdminSidebarComponent} from './admin/shared/admin-sidebar/admin-sidebar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    FooterComponent,
    NgxSpinnerComponent,
    AdminHeaderComponent,
    UserComponent,
    AdminComponent,
    NgIf,
    RouterLink,
    AdminSidebarComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  isAdmin: boolean = false;

  constructor (
    private spinner: NgxSpinnerService,
    private authService: AuthService,
  ) {}
  title = 'Nameless';
  role: string = 'admin';

  ngOnInit() {
    this.spinner.show();

    setTimeout(() => {
      this.spinner.hide();
    }, 1500);

    this.isAdmin = this.authService.isAdmin();
  }
}
