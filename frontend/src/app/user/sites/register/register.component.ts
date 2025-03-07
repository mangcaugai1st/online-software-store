import { Component } from '@angular/core';
import {LoginFormComponent} from "../../shared/components/login-form/login-form.component";
import {RegisterFormComponent} from '../../shared/components/register-form/register-form.component';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    LoginFormComponent,
    RegisterFormComponent,
    RouterLink
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

}
