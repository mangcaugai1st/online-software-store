import { Pipe, PipeTransform } from '@angular/core';
import { Product } from '../models/product.model'
@Pipe({
  name: 'searchProduct',
  standalone: true
})
export class SearchProductPipe implements PipeTransform {

  transform(products: Product[], searchText: string) {
    if (!searchText || !products) {
      return products;
    }

    return products.filter(product =>
      product.name?.toLowerCase().includes(searchText.toLowerCase())
    );
  }
}
