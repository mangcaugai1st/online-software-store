import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CofirmDeleteCategoryModalComponent } from './cofirm-delete-category-modal.component';

describe('CofirmDeleteCategoryModalComponent', () => {
  let component: CofirmDeleteCategoryModalComponent;
  let fixture: ComponentFixture<CofirmDeleteCategoryModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CofirmDeleteCategoryModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CofirmDeleteCategoryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
