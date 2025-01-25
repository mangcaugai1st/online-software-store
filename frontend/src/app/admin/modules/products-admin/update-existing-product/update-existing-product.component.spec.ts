import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateExistingProductComponent } from './update-existing-product.component';

describe('UpdateExistingProductComponent', () => {
  let component: UpdateExistingProductComponent;
  let fixture: ComponentFixture<UpdateExistingProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateExistingProductComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateExistingProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
