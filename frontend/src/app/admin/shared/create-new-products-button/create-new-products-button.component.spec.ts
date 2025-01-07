import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateNewProductsButtonComponent } from './create-new-products-button.component';

describe('CreateNewProductsButtonComponent', () => {
  let component: CreateNewProductsButtonComponent;
  let fixture: ComponentFixture<CreateNewProductsButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateNewProductsButtonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateNewProductsButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
