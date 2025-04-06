"use client";
import { useCartStore } from "@/store/cart-store";
import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import { getToken, clearToken } from "@/lib/auth";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { jwtDecode } from "jwt-decode";

const Navbar = () => {
  const { items } = useCartStore();  // Sepet elemanlarını alıyoruz
  const [isLoginPage, setIsLoginPage] = useState(false);
  const router = useRouter();
  const pathname = usePathname();  // usePathname ile geçerli yol

  const user = getToken();
  const [isAdmin, setIsAdmin] = useState(false);

  useEffect(() => {
    setIsLoginPage(pathname === "/login" || pathname === "/signup");

    if (user) {
      try {
        const decodedToken = jwtDecode<{ role: string }>(user);  // JWT token'ı decode ediyoruz
        if (decodedToken.role === "Admin") {
          setIsAdmin(true);  // Kullanıcı admin ise isAdmin true yapıyoruz
        }else{
            setIsAdmin(false);
        }
        
      } catch (error) {
        console.error("Token decode hatası:", error);
        setIsAdmin(false);
      }
    }
  }, [pathname, user]);  // user değiştiğinde çalışacak

  const handleLogout = () => {
    clearToken();
    router.push("/login");
  };

  const goToCart = () => {
    router.push("/cart");
  };

  const goToProfile = () => {
    router.push("/profile");  // Profil sayfasına yönlendir
  };

  const goToProducts = () => {
    router.push("/products");  // Ürünler sayfasına yönlendir
  };

  return (
    <nav className="flex justify-between items-center p-4 bg-gray-800 text-white">
      <div className="text-2xl font-semibold text-blue-500" onClick={goToProducts}>Logo</div>
      <div className="flex items-center space-x-6">
        {isLoginPage ? null : (
          user && (
            <button onClick={goToCart} className="relative">
              <span>Sepet ({items.length})</span>  {/* Sepet eleman sayısı burada */}
            </button>
          )
        )}

        {user ? (
          <div className="flex items-center space-x-2">
            <button onClick={handleLogout} className="bg-red-500 text-white py-1 px-3 rounded">Çıkış</button>
            <button onClick={goToProfile} className="bg-blue-500 text-white py-1 px-3 rounded">Profil</button>
            {isAdmin == true && (
              <Link href="/admin" className="bg-blue-500 text-white py-1 px-3 rounded">
                Admin Paneli
              </Link>
            )}
          </div>
        ) : (
          <div className="flex items-center space-x-2">
            <Link href="/login">Giriş Yap</Link>
            <Link href="/signup">Kayıt Ol</Link>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
