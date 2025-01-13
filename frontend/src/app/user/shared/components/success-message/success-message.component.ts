import { Component } from '@angular/core';

@Component({
  selector: 'app-success-message',
  standalone: true,
  imports: [],
  templateUrl: './success-message.component.html',
  styleUrl: './success-message.component.css'
})
export class SuccessMessageComponent {
  message: string;
}
