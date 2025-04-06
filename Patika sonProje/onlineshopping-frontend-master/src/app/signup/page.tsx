"use client";

import { useState } from "react";
import api from "@/lib/api";
import { useRouter } from "next/navigation";

export default function SignupPage() {
  const router = useRouter();
  const [form, setForm] = useState({ email: "", password: "", rePassword: "" });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await api.post("/Auth/register", form);
      router.push("/login");
    } catch (err) {
        console.log(err);
      alert("Kayıt başarısız.");
    }
  };

  return (
    <main className="max-w-md mx-auto mt-20 p-4 border rounded-lg shadow">
      <h2 className="text-2xl mb-4">Kayıt Ol</h2>
      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <input name="email" onChange={handleChange} type="email" placeholder="Email" required />
        <input name="password" onChange={handleChange} type="password" placeholder="Şifre" required />
        <input name="rePassword" onChange={handleChange} type="password" placeholder="Şifre Tekrar" required />
        <button type="submit" className="bg-blue-500 text-white py-2 rounded">Kayıt Ol</button>
      </form>
    </main>
  );
}
