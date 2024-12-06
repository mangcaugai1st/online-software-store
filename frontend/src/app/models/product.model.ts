import {Category} from "./category.model"

export interface Product {
  id: number;
  categoryId: number;
  category: Category | null;
  name: string | null;
  price: number;
  imagePath: string | null;
  description: string | null;
  stockQuantity: number;
  slug: string | null;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
  // orderDetails: OrderDetail[] | null;
}
