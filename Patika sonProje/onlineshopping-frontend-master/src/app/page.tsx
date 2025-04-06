"use client";
import Link from "next/link";


export default function Home() {
  return (
    <div className="flex flex-col items-center justify-center min-h-screen p-8">
      <h1 className="text-3xl font-bold mb-8">Hoş Geldiniz</h1>
      <div>
        <Link href="/login">
          <button className="bg-blue-500 text-white py-2 px-4 rounded mr-4">Giriş Yap</button>
        </Link>
        <Link href="/signup">
          <button className="bg-green-500 text-white py-2 px-4 rounded">Kayıt Ol</button>
        </Link>
      </div>
    </div>
  );
}
