import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HeaderComponent} from './user/shared/components/header/header.component';
import {FooterComponent} from './user/shared/components/footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'frontend';
}
