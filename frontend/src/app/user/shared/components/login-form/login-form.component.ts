import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {AuthService} from '../../../../services/auth.service';
import {Router} from '@angular/router';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})
export class LoginFormComponent {
  loginForm: FormGroup;
  isLoading: boolean = false;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
      this.loginForm = this.formBuilder.group({
        username: ['', Validators.required],
        password: ['', Validators.required]
      })
  }

  // get f() { return this.loginForm.controls; }

  onSubmit() {
    if (this.loginForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';

      this.authService.loginHandler(this.loginForm.value).subscribe({
        next: (response) => {
          localStorage.setItem('token', response.token);
          // localStorage.setItem('username', response.username);
          this.router.navigate(['']);
          this.successMessage = 'Đăng nhập thành công';
        },
        error: (error) => {
          this.errorMessage = "Đăng nhập thất bại. Vui lòng kiểm tra lại thông tin.";

          console.error('Lỗi đăng nhập:',error);
        }
      })
    }
  }
}
