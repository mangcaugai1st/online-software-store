import { Component } from '@angular/core';
import {NgIf} from "@angular/common";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from '../../../../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register-form',
  standalone: true,
    imports: [
        NgIf,
        ReactiveFormsModule
    ],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})
export class RegisterFormComponent {
  registrationForm: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';
  submitted: boolean = false;

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) {
    this.registrationForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    }, {
      validators: this.passwordMatchValidator,
    })
  }

  passwordMatchValidator(group: FormGroup) {
    const password = group.get('password');
    const confirmPassword = group.get('confirmPassword');

    /*
    * Kiểm tra 3 điều kiện:
    * 1. password control có tồn tại
    * 2. confirmPassword control có tồn tại
    * 3. Giá trị của 2 control không bằng nhau
    * */
    if (password && confirmPassword && password.value !==  confirmPassword.value) {
      // Nếu không khớp thì sẽ set lỗi cho confirmPassword control.
      confirmPassword.setErrors({ passwordMismatch: true });
    }
    else {
      // Nếu khớp thì sẽ xóa lỗi khỏi confirmPassword control
      confirmPassword.setErrors(null);
    }
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      this.errorMessage = '';
      this.authService.registerHandler().subscribe({
        next: (response) => {
          this.submitted = true;
          console.log(this.registrationForm.value);
          this.successMessage = 'Đăng ký thành công';
          this.router.navigate(['']);
        },
        error: (error) => {
          this.errorMessage = "Đăng ký thất bại. Vui lòng kiểm tra lại thông tin.";
          console.error(error);
        }
      })
    }
  }
}
