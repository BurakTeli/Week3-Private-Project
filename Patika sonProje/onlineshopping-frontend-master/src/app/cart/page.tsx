"use client";
import api from "@/lib/api";
import { useCartStore } from "@/store/cart-store";
import { useRouter } from "next/navigation";

export default function CartPage() {
  const { items, removeOneFromCart, clearCart, addToCart } = useCartStore();
  const router = useRouter();

  // Sepetteki toplam fiyatı hesaplamak
  const totalPrice = items.reduce((total, item) => total + item.price * item.quantity, 0);

  const handleCheckout = async () => {
    const orderData = items.map(item => ({
      productId: item.id,
      quantity: item.quantity,
    }));

    try {
      await api.post("/order", { items: orderData });
      clearCart(); // Sepeti temizle
      router.push("/orders"); // Siparişler sayfasına yönlendir
    } catch (err) {
      console.error("Error during checkout:", err);
      alert("Sipariş oluşturulamadı.");
    }
  };

  return (
    <main className="max-w-screen-lg mx-auto mt-20 p-4">
      <h2 className="text-2xl mb-4">Sepet</h2>
      {items.length === 0 ? (
        <p>Sepetiniz boş.</p>
      ) : (
        <div>
          {items.map((item) => (
            <div key={item.id} className="flex justify-between items-center border p-4 rounded shadow mb-4">
              <div>
                <p className="font-semibold">{item.productName}</p>
                <p>{item.quantity} x {item.price} ₺</p>
              </div>
              <div className="flex items-center">
                <button
                  className="bg-gray-300 text-black py-1 px-3 rounded"
                  onClick={() => addToCart(item)}  // Birim artırma
                >
                  +
                </button>
                <span className="mx-2">{item.quantity}</span>
                <button
                  className="bg-gray-300 text-black py-1 px-3 rounded"
                  onClick={() => removeOneFromCart(item.id)}  // Birim azaltma
                >
                  -
                </button>
              </div>
            </div>
          ))}
          <div className="mt-4 flex justify-between">
            <button onClick={clearCart} className="bg-gray-300 text-black py-2 px-4 rounded">
              Sepeti Temizle
            </button>
            <div className="font-semibold text-xl">
              Toplam Fiyat: {totalPrice.toFixed(2)} ₺
            </div>
            <button onClick={handleCheckout} className="bg-green-600 text-white py-2 px-4 rounded">
              Siparişi Ver
            </button>
          </div>
        </div>
      )}
    </main>
  );
}
