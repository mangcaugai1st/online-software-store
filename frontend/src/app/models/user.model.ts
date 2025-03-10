import { Order } from "./order.model";

export interface User {
  id: number;
  username: string | null;
  fullName: string | null;
  password: string | null;
  email: string | null;
  phone: string | null;
  address: string | null;
  isAdmin: boolean;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
  orders: Order[] | null;
}
