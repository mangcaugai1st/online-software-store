import { Component } from '@angular/core';
import {HeaderComponent} from '../../shared/components/header/header.component';
import {NgForOf} from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NgForOf
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  slide = [
    "https://animebird.net/wp-content/uploads/2018/07/9-3.jpg?w=800&h=449&crop=1"
  ]
}
