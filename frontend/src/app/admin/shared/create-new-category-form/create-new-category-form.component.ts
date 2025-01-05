import { Component } from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {NgIf} from "@angular/common";
import {Category} from '../../../models/category.model';
import { CategoryService } from '../../../services/category.service'

@Component({
  selector: 'app-create-new-category-form',
  standalone: true,
    imports: [
        FormsModule,
        NgIf,
        ReactiveFormsModule
    ],
  templateUrl: './create-new-category-form.component.html',
  styleUrl: './create-new-category-form.component.css'
})
export class CreateNewCategoryFormComponent {
  category: Category;
  isVisible: boolean = false;
  createForm: FormGroup;

  // categoryy: Category = new class implements Category {
  //   description: string | null;
  //   id: number;
  //   name: string | null;
  //   slug: string | null;
  // }

  constructor(
    private categoryService: CategoryService,
    private formBuilder: FormBuilder
  ) {
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      slug: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  openModal() {
    this.isVisible = true;
  }

  closeModal() {
    this.isVisible = false;
  }

  addNewCategory(): void {
    this.categoryService.addNewCategory(this.createForm.value).subscribe({
      next: () => {
        console.log("Thêm danh mục mới thành công: ");
        window.location.reload();
      },
      error: (error) => {
        console.log('Thêm mới thất bại: ' + error);
      }
    })
  }
}
