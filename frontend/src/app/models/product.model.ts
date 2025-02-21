import {Category} from "./category.model"
import {OrderDetail} from "./orderDetail.model"
import {SubscriptionType} from "../enums/subscription-type.enum"

export interface Product {
  id: number;
  categoryId: number;
  category: Category | null;
  name: string | null;
  price: number;
  subscriptionType: SubscriptionType;
  monthlyRentalPrice: number | null;
  yearlyRentalPrice: number | null;
  discount: number | null;
  imagePath: string | null;
  description: string | null;
  stockQuantity: number;
  slug: string | null;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
  orderDetails: OrderDetail[] | null;
}
