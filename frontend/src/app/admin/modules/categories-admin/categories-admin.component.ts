import {Component, OnInit} from '@angular/core';
import { CategoryService } from '../../../services/category.service'
import { Category } from '../../../models/category.model';
import { NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {  } from '../../shared/create-new-category-form'

@Component({
  selector: 'app-categories-admin',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    FormsModule,
  ],
  templateUrl: './categories-admin.component.html',
  styleUrl: './categories-admin.component.css'
})
export class CategoriesAdminComponent implements OnInit {
  categories: Array<Category> = [];
  selectedCategory: Category | null = null; // Id danh mục chọn để xóa
  isVisible: boolean = false;
  isVisible1: boolean = false;
  isUpdateCategoryModalVisible: boolean = false;

  // category: Category = {
  //   id : 0,
  //   name: "",
  //   slug: "",
  //   description: ""
  // };

  categoryy: Category = new class implements Category {
    description: string | null;
    id: number;
    name: string | null;
    slug: string | null;
  }

  constructor (private categoryService: CategoryService) { }

  ngOnInit() {
    this.GetCategories();
  }

  confirmDeleteCategory(id: number)
  {
    console.log(id);
    // this.categoryService.deleteCategory(id).subscribe(
    //   response => {
    //     console.log('Đã xóa thành công ', response);
    //   },
    //   error => {
    //     console.log('Xóa không thành công ', error);
    //   }
    // )
    this.categoryService.deleteCategory(id).subscribe(
      () => {
        this.categories = this.categories.filter(category => category.id !== id);
        this.closeConfirmModal();
      },
      error => {
        console.log('Xóa không thành công ', error);
      }
    )
  }

  saveChanges() {
    this.categoryService.updateExistedCategory(this.categoryy).subscribe(
      (saveChanges) => {
        alert('Cập nhật danh mục thành công!');
        // Cập nhật lại danh mục nếu thành công
        this.categoryy = saveChanges;
      },
      (error) => {
        console.log(error);
      }
    )
  }

  GetCategories () {
    this.categoryService.getCategories().subscribe({
      next : (categories: Category[]) => { this.categories = categories },
      error : error => console.log(error)
    })
  }

  openConfirmModal(category: Category)
  {
    this.selectedCategory = category;
    this.isVisible1 = true;
  }

  closeConfirmModal() {
    this.isVisible1 = false;
    this.selectedCategory = null;
  }

  openUpdateCategoryModal() {
    this.isUpdateCategoryModalVisible = true;
  }

  closeUpdateCategoryModal() {
    this.isUpdateCategoryModalVisible = false;
  }

  protected readonly close = close;
}
