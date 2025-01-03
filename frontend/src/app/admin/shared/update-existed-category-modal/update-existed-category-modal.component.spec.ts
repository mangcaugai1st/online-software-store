import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateExistedCategoryModalComponent } from './update-existed-category-modal.component';

describe('UpdateExistedCategoryModalComponent', () => {
  let component: UpdateExistedCategoryModalComponent;
  let fixture: ComponentFixture<UpdateExistedCategoryModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateExistedCategoryModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateExistedCategoryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
