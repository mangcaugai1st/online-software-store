import {Category} from "./category.model"
import {OrderDetail} from "./orderDetail.model"

export enum SubscriptionType {
  Perpetual,
  Rental
}

export interface Product {
  id: number;
  categoryId: number;
  category: Category | null;
  name: string | null;
  price: number;
  SubscriptionType : SubscriptionType;
  yearlyRentalPrice: number | null;
  imagePath: string | null;
  description: string | null;
  stockQuantity: number;
  slug: string | null;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
  orderDetails: OrderDetail[] | null;
}
