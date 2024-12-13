// import { User } from "./user";
// import { Address } from "./address";
// import { PaymentDetail } from "./paymentDetail";
import { OrderDetail } from "./orderDetail.model";

export interface Order {
  id: number;
  userId: number;
//  user: User | null;
  addressId: number;
//  address: Address | null;
  orderDate: string;
  totalAmount: number;
  status: string | null;
  paymentStatus: string | null;
  createdAt: string;
  updatedAt: string;
//  paymentDetail: PaymentDetail | null;
  orderDetails: OrderDetail[] | null;
}
