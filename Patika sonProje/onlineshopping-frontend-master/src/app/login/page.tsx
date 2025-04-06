"use client";

import { useState } from "react";
import api from "@/lib/api";
import { useRouter } from "next/navigation";
import { setToken } from "@/lib/auth";

export default function LoginPage() {
  const router = useRouter();
  const [form, setForm] = useState({ email: "", password: "" });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await api.post("/Auth/login", form);
      setToken(res.data.token);
      router.push("/products");
    } catch (err) {
      console.error("Error during login:", err); // Debug log
      alert("Giriş başarısız.");
    }
  };

  return (
    <main className="max-w-md mx-auto mt-20 p-4 border rounded-lg shadow">
      <h2 className="text-2xl mb-4">Giriş Yap</h2>
      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <input name="email" onChange={handleChange} type="email" placeholder="Email" required />
        <input name="password" onChange={handleChange} type="password" placeholder="Şifre" required />
        <button type="submit" className="bg-green-600 text-white py-2 rounded">Giriş Yap</button>
      </form>
    </main>
  );
}
