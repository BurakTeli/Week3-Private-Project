"use client";

import { useState, useEffect } from "react";
import { Product } from "@/types/product";
import api from "@/lib/api";
import { useRouter } from "next/navigation";
import { getToken } from "@/lib/auth";
import { jwtDecode } from "jwt-decode";

export default function AdminPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [form, setForm] = useState({ productName: "", price: 0, stockQuantity: 0 });
  const [isAdmin, setIsAdmin] = useState(false);  // Admin kontrolü için state

  const router = useRouter();
  const token = getToken();  // Token'ı alıyoruz

  // Admin kontrolü için token'ı decode etme işlemi
  useEffect(() => {
    if (token) {
      try {
        const decodedToken = jwtDecode<{ role: string }>(token);  // JWT token'ı decode ediyoruz
        if (decodedToken.role === "Admin") {
          setIsAdmin(true);  // Kullanıcı adminse admin yetkisini sağlıyoruz
        } else {
          alert("Bu sayfayı sadece admin kullanıcıları görebilir!");
          router.push("/products");  // Admin olmayan kullanıcıları ana sayfaya yönlendiriyoruz
        }
      } catch (error) {
        console.error("Token decode hatası:", error);
        alert("Geçersiz token.");
        router.push("/login");  // Token geçersizse login sayfasına yönlendiriyoruz
      }
    } else {
      alert("Giriş yapmadınız!");
      router.push("/login");  // Token yoksa login sayfasına yönlendiriyoruz
    }
  }, [token, router]);

  // Ürünleri almak için API çağrısı
  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await api.get("/Product");
        setProducts(response.data);
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };

    fetchProducts();
  }, []);

  // Form inputları için değişiklik işlemi
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  // Ürün ekleme işlemi
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await api.post("/product", form);
      setForm({ productName: "", price: 0, stockQuantity: 0 });
      // Ürünler listesini güncelle
      const response = await api.get("/product");
      setProducts(response.data);
    } catch (err) {
      console.error("Error adding product:", err);
      alert("Ürün eklenemedi.");
    }
  };

  // Ürün silme işlemi
  const handleDelete = async (productId: number) => {
    try {
      await api.delete(`/product/${productId}`);
      setProducts(products.filter((product) => product.id !== productId));
    } catch (err) {
      console.error("Error deleting product:", err);
      alert("Ürün silinemedi.");
    }
  };

  return (
    <main className="max-w-screen-lg mx-auto mt-20 p-8 bg-white">
    <h2 className="text-3xl font-semibold text-center text-blue-800 mb-8">Admin Paneli</h2>
    {isAdmin ? (
      <>
        <div className="bg-white p-6 rounded-lg shadow-md mb-8">
          <h3 className="text-xl font-semibold text-gray-900 mb-4">Yeni Ürün Ekle</h3>
          <form onSubmit={handleSubmit} className="space-y-4">
            <div>
              <label htmlFor="productName" className="block text-sm font-medium text-gray-800">Ürün Adı</label>
              <input
                id="productName"
                name="productName"
                value={form.productName}
                onChange={handleChange}
                type="text"
                placeholder="Ürün Adı"
                required
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>

            <div>
              <label htmlFor="price" className="block text-sm font-medium text-gray-800">Fiyat</label>
              <input
                id="price"
                name="price"
                value={form.price}
                onChange={handleChange}
                type="number"
                placeholder="Fiyat"
                required
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>

            <div>
              <label htmlFor="stockQuantity" className="block text-sm font-medium text-gray-800">Stok Miktarı</label>
              <input
                id="stockQuantity"
                name="stockQuantity"
                value={form.stockQuantity}
                onChange={handleChange}
                type="number"
                placeholder="Stok Miktarı"
                required
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>

            <button
              type="submit"
              className="w-full bg-blue-600 text-white py-2 rounded-md shadow-sm hover:bg-blue-700 transition duration-300"
            >
              Ürün Ekle
            </button>
          </form>
        </div>

        <div className="bg-white p-6 rounded-lg shadow-md">
          <h3 className="text-xl font-semibold text-gray-900 mb-4">Ürünler</h3>
          {products.length === 0 ? (
            <p className="text-gray-500">Ürün bulunmamaktadır.</p>
          ) : (
            <ul className="space-y-4">
              {products.map((product) => (
                <li key={product.id} className="flex justify-between items-center p-4 border rounded-lg shadow-sm bg-gray-50">
                  <div>
                    <p className="font-semibold text-lg text-gray-800">{product.productName}</p>
                    <p className="text-gray-600">{product.price} ₺</p>
                  </div>
                  <button
                    className="bg-red-500 text-white py-2 px-4 rounded-md shadow-sm hover:bg-red-600 transition duration-300"
                    onClick={() => handleDelete(product.id)}
                  >
                    Sil
                  </button>
                </li>
              ))}
            </ul>
          )}
        </div>
      </>
    ) : (
      <p className="text-center text-lg text-red-500">Admin yetkisi gereklidir.</p>
    )}
  </main>
  );
}