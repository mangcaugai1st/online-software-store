import { Order } from "./order.model";
import { Product } from "./product.model";

export interface OrderDetail {
  id: number;
  orderId: number;
  order: Order | null;
  productId: number;
  product: Product | null;
  quantity: number;
  unitPrice: number;
  subtotal: number;
}
