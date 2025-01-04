import { Component } from '@angular/core';
import {FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgIf} from "@angular/common";
import {CategoryService} from '../../../services/category.service';
import {Category} from '../../../models/category.model';

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
  category: Category = new class implements Category {
    id: number;
    name: string | null;
    description: string | null;
    slug: string | null;
  };
  isVisible: boolean  = false;
  updateForm: FormGroup

  constructor(
    private categoryService: CategoryService
  ) { }

  openModal(): void {
    this.isVisible = true;
  }

  closeModal(): void {
    this.isVisible = false;
  }

  saveChanges() {
    this.categoryService.updateExistedCategory(this.category).subscribe(
      (saveChanges) => {
        alert('Cập nhật danh mục thành công!');
        // Cập nhật lại danh mục nếu thành công
        this.category = saveChanges;
      },
      (error) => {
        console.log(error);
      }
    )
  }
}
