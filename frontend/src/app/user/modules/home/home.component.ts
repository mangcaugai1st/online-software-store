import { Component } from '@angular/core';
import {NgForOf} from '@angular/common';
import {CategoriesComponent} from '../../shared/components/categories/categories.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NgForOf,
    CategoriesComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  slide = [
    "https://animebird.net/wp-content/uploads/2018/07/9-3.jpg?w=800&h=449&crop=1"
  ]
}
