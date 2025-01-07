import { Component } from '@angular/core';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-create-new-products-button',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './create-new-products-button.component.html',
  styleUrl: './create-new-products-button.component.css'
})
export class CreateNewProductsButtonComponent {

}
