import {Component, OnInit} from '@angular/core';
import {CategoryService} from '../../../services/category.service'
import {Category} from '../../../models/category.model';
import {NgForOf, NgIf} from '@angular/common';
import {FormsModule} from '@angular/forms';


@Component({
  selector: 'app-categories-admin',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    FormsModule
  ],
  templateUrl: './categories-admin.component.html',
  styleUrl: './categories-admin.component.css'
})
export class CategoriesAdminComponent implements OnInit {
  categories: Array<Category> = [];
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

  isVisible: boolean = false;
  isVisible1: boolean = false;

  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.GetCategories();

    this.openModal();
    this.closeModal();

    this.openConfirmModal();
    this.closeConfirmModal();

    this.onSubmit();
  }

  onSubmit(): void {
    this.categoryService.addNewCategory(this.categoryy).subscribe(
      response => {
        console.log('Danh mục sản phẩm mới được thêm thành công ', response);
      },
      error => {
        console.log('Có lỗi xảy ra khi thêm danh mục sản phẩm ', error);
      }
    )
  }

  confirmDeleteCategory(id: number)
  {
    console.log(id);
    this.categoryService.deleteCategory(id).subscribe(
      response => {
        console.log(response);
      },
      error => {
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
  // Mở modal
  openModal() {
    this.isVisible = true;
  }

  // Đóng modal
  closeModal() {
    this.isVisible = false;
  }

  openConfirmModal()
  {
    this.isVisible1 = true;
  }

  closeConfirmModal() {
    this.isVisible1 = false;
  }

  protected readonly close = close;
}
