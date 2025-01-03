import { Component } from '@angular/core';
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";
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

  // categoryy: Category = new class implements Category {
  //   description: string | null;
  //   id: number;
  //   name: string | null;
  //   slug: string | null;
  // }

  constructor(private categoryService: CategoryService) { }

  openModal() {
    this.isVisible = true;
  }

  closeModal() {
    this.isVisible = false;
  }

  addNewCategory(): void {
    this.categoryService.addNewCategory(this.category).subscribe(
      response => {
        console.log('Danh mục sản phẩm mới được thêm thành công ', response);
        window.location.reload();
      },
      error => {
        console.log('Có lỗi xảy ra khi thêm danh mục sản phẩm ', error);
      }
    )
  }



}
