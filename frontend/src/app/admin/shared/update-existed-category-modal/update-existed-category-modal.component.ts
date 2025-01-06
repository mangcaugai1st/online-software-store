import {Component, Input} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
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
  category: Category;
  @Input() categoryId: number;
  @Input() categoryName: string = '';
  @Input() categorySlug: string = '';
  @Input() categoryDescription: string = '';
  isVisible: boolean = false;
  updateForm: FormGroup

  constructor(
    private categoryService: CategoryService,
    private formBuilder: FormBuilder
  ) {
    this.updateForm = this.formBuilder.group({
      categoryName: ['', [Validators.required]],
      categorySlug: ['', [Validators.required]],
      categoryDescription: [''],
    })
  }

  openModal(): void {
    this.isVisible = true;
  }

  closeModal(): void {
    this.isVisible = false;
  }

  saveChanges() {
    const updatedCategory: Category = {
      id: this.categoryId,
      name: this.updateForm.get('categoryName')?.value,
      slug: this.updateForm.get('categorySlug')?.value,
      description: this.updateForm.get('categoryDescription')?.value || null,
    };
    this.categoryService.updateExistedCategory(this.categoryId, updatedCategory).subscribe({
      next: () => {
        console.log('Cập nhật thành công');
        this.closeModal();
        window.location.reload();
      },
      error: (error) => {
        console.log('Lỗi khi cập nhật', error);
      },
    });
  }
}
