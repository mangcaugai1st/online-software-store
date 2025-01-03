import { Component } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-update-existed-category-modal',
  standalone: true,
    imports: [
        FormsModule,
        NgIf,
        ReactiveFormsModule
    ],
  templateUrl: './update-existed-category-modal.component.html',
  styleUrl: './update-existed-category-modal.component.css'
})
export class UpdateExistedCategoryModalComponent {

}
