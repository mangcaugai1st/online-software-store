import {Component, Input} from '@angular/core';
import {Category} from '../../../models/category.model';
import {CategoryService} from '../../../services/category.service';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-delete-existed-category',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './delete-existed-category.component.html',
  styleUrl: './delete-existed-category.component.css'
})
export class DeleteExistedCategoryComponent {
  @Input() categoryId: number;
  isVisible: boolean = false;
  category: Category;

  constructor (private categoryService: CategoryService) { }

  openModal(): void {
    this.isVisible = true;
  }

  closeModal(): void {
    this.isVisible = false;
  }

  confirmDeleteCategory()
  {
    // console.log(id);
    // this.categoryService.deleteCategory(id).subscribe(
    //   response => {
    //     console.log('Đã xóa thành công ', response);
    //   },
    //   error => {
    //     console.log('Xóa không thành công ', error);
    //   }
    // )
    this.categoryService.deleteCategory(this.categoryId).subscribe({
      next: () => {
        console.log('Đã xóa thành công');
        this.closeModal();
        window.location.reload();
      },
      error: (err) => {
        console.log('Lỗi khi xóa danh mục: ', err);
      }
    })
  }
}
