"use client";

import { useState, useEffect } from "react";
import api from "@/lib/api";


export default function OrdersPage() {
  interface Order {
    id: string;
    date: string;
    items: {
      productId: string;
      productName: string;
      quantity: number;
      price: number;
    }[];
  }

  const [orders, setOrders] = useState<Order[]>([]);
  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const response = await api.get("/order/myorders");
        setOrders(response.data);
      } catch (error) {
        console.error("Error fetching orders:", error);
      }
    };

    fetchOrders();
  }, []);
  // Her siparişin toplam fiyatını hesaplamak
  const calculateTotalPrice = (order: Order) => {
    return order.items.reduce((total: number, item: Order['items'][0]) => total + item.price * item.quantity, 0);
  };

  // Her siparişi ve toplam fiyatı göstermek
  return (
    <main className="max-w-screen-lg mx-auto mt-20 p-4">
      <h2 className="text-2xl font-semibold mb-6">Siparişlerim</h2>
      {orders.length === 0 ? (
        <p>Henüz siparişiniz yok.</p>
      ) : (
        <div className="space-y-6">
          {orders.map((order) => (
            <div key={order.id} className="border p-6 rounded-lg shadow-lg">
              <p className="text-lg font-bold">Sipariş ID: {order.id}</p>
              <p className="text-sm text-gray-500">Tarih: {order.date}</p>

              {/* Sipariş içeriği */}
              <div className="mt-4">
                <h3 className="text-md font-semibold mb-2">Sipariş İçeriği:</h3>
                <ul className="space-y-2">
                  {order.items.map((item: Order['items'][0]) => (
                    <li key={item.productId} className="flex justify-between">
                      <span>{item.productName}</span>
                      <span>{item.quantity} x {item.price} ₺</span>
                    </li>
                  ))}
                </ul>
              </div>

              {/* Siparişin toplam fiyatı */}
              <div className="mt-4 font-semibold text-lg">
                <span>Toplam Fiyat: </span>
                <span>{calculateTotalPrice(order).toFixed(2)} ₺</span>
              </div>
            </div>
          ))}
        </div>
      )}
    </main>
  );
}