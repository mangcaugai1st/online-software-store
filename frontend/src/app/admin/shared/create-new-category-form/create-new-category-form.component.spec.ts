import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateNewCategoryFormComponent } from './create-new-category-form.component';

describe('CreateNewCategoryFormComponent', () => {
  let component: CreateNewCategoryFormComponent;
  let fixture: ComponentFixture<CreateNewCategoryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateNewCategoryFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateNewCategoryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
