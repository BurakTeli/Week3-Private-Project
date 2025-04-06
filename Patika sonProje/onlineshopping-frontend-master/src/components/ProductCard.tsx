"use client";
import { Product } from "@/types/product";
import { useCartStore } from "@/store/cart-store";
import { useState } from "react";

interface Props {
  product: Product;
}

export default function ProductCard({ product }: Props) {
  const addToCart = useCartStore((s) => s.addToCart);
  const [isAdding, setIsAdding] = useState(false);  // Sepete eklerken state oluşturduk

  const handleAddToCart = () => {
    setIsAdding(true);
    addToCart(product);  // Sepete ekle
    setTimeout(() => setIsAdding(false), 1000);  // 1 saniye sonra "Bekliyor..." durumu kaldırılır
  };

  return (
    <div className="border rounded p-4 flex flex-col gap-2 shadow hover:shadow-md transition">
      <h3 className="text-lg font-semibold">{product.productName}</h3>
      <p>Fiyat: {product.price} ₺</p>
      <p>Stok: {product.stockQuantity}</p>
      <button
        className={`bg-blue-500 text-white py-1 px-3 rounded mt-auto ${isAdding ? "bg-gray-400 cursor-not-allowed" : ""}`}
        onClick={handleAddToCart}
        disabled={isAdding}  // Beklerken buton devre dışı bırakılır
      >
        {isAdding ? "Sepete Ekleme..." : "Sepete Ekle"}
      </button>
    </div>
  );
}
