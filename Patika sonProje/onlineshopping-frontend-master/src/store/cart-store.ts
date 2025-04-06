import { create } from "zustand";
import { Product } from "@/types/product";

interface CartItem extends Product {
  quantity: number;
}

interface CartStore {
  items: CartItem[];
  addToCart: (product: Product) => void;
  removeFromCart: (productId: number) => void;
  clearCart: () => void;
  removeOneFromCart: (productId: number) => void;
}

export const useCartStore = create<CartStore>((set) => ({
  items: [],
  removeOneFromCart: (productId: number) =>
    set((state) => {
      const product = state.items.find((item) => item.id === productId);
      
      if (product) {
        if (product.quantity > 1) {
          // Miktarı 1 azalt
          return {
            items: state.items.map((item) =>
              item.id === productId
                ? { ...item, quantity: item.quantity - 1 }
                : item
            ),
          };
        } else {
          // Eğer miktar 1 ise, o ürünü tamamen sepetten çıkar
          return {
            items: state.items.filter((item) => item.id !== productId),
          };
        }
      }
      return state;
    }),
  addToCart: (product) =>
    set((state) => {
      const exists = state.items.find((item) => item.id === product.id);
      if (exists) {
        return {
          items: state.items.map((item) =>
            item.id === product.id ? { ...item, quantity: item.quantity + 1 } : item
          ),
        };
      } else {
        return { items: [...state.items, { ...product, quantity: 1 }] };
      }
    }),  
    removeFromCart: (productId: number) =>
      set((state) => ({
        items: state.items.filter((item) => item.id !== productId),  // sadece seçili ürünü siliyoruz
      })),
  clearCart: () => set({ items: [] }),
}));
