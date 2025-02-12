import { User } from "./user.model";
import { Product } from "./product.model";

export interface Cart {
  id: number;
  userId: number;
  user: User | null;
  productId: number;
  product: Product | null;
  quantity: number;
  createdAt: string;
  updatedAt: string;
}
