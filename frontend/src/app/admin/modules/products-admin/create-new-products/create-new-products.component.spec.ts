import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateNewProductsComponent } from './create-new-products.component';

describe('CreateNewProductsComponent', () => {
  let component: CreateNewProductsComponent;
  let fixture: ComponentFixture<CreateNewProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateNewProductsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateNewProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
