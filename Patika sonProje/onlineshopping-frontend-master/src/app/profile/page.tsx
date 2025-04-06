"use client";
import { useState, useEffect } from "react";
import { getToken } from "@/lib/auth";
import api from "@/lib/api";
import { useRouter } from "next/navigation";

export default function ProfilePage() {
  const [userProfile, setUserProfile] = useState<{
    firstName: string;
    lastName: string;
    phoneNumber: string;
  } | null>(null);
  const [firstName, setFirstName] = useState<string>("");
  const [lastName, setLastName] = useState<string>("");
  const [phoneNumber, setPhoneNumber] = useState<string>("");
  const [currentPassword, setCurrentPassword] = useState<string>("");
  const [newPassword, setNewPassword] = useState<string>("");
  const [reNewPassword, setReNewPassword] = useState<string>("");
  const [isLoading, setIsLoading] = useState(false);
  const router = useRouter();

  useEffect(() => {
    const fetchUserProfile = async () => {
      const token = getToken();
      if (token) {
        try {
          const response = await api.get("/User/profile", {
            headers: { Authorization: `Bearer ${token}` },
          });
          setUserProfile(response.data);
          setFirstName(response.data.firstName);
          setLastName(response.data.lastName);
          setPhoneNumber(response.data.phoneNumber);
        } catch (error) {
          console.error("Error fetching user profile:", error);
        }
      }
    };

    fetchUserProfile();
  }, []);

  /* const handleProfileUpdate = async () => {
    setIsLoading(true);
    try {
      await api.put(
        "/User/profile",
        {
          firstName,
          lastName,
          phoneNumber,
        },
        {
          headers: { Authorization: `Bearer ${getToken()}` },
        }
      );
      alert("Profil güncellendi.");
    } catch (error) {
      alert("Profil güncellenemedi.");
      console.error(error);
    } finally {
      setIsLoading(false);
    }
  }; */

  const handlePasswordUpdate = async () => {
    setIsLoading(true);
    try {
      await api.put(
        "/User/update-password",
        {
          currentPassword,
          newPassword,
          reNewPassword,
        },
        {
          headers: { Authorization: `Bearer ${getToken()}` },
        }
      );
      alert("Şifre güncellendi.");
    } catch (error) {
      alert("Şifre güncellenemedi.");
      console.error(error);
    } finally {
      setIsLoading(false);
    }
  };

  const goToOrders = () => {
    router.push("/orders");  // Siparişler sayfasına yönlendir
  };

  return (
    <div className="max-w-screen-sm mx-auto p-8">
      <h2 className="text-2xl font-semibold mb-6">Profilim</h2>
      {userProfile ? (
        <div className="space-y-6">
          <div>
            <h3 className="text-xl font-medium mb-2">Kişisel Bilgiler</h3>
            <div className="space-y-4">
              <div>
                <label className="block">Ad</label>
                <input
                  type="text"
                  value={firstName}
                  onChange={(e) => setFirstName(e.target.value)}
                  className="w-full p-2 border border-gray-300 rounded-md"
                />
              </div>
              <div>
                <label className="block">Soyad</label>
                <input
                  type="text"
                  value={lastName}
                  onChange={(e) => setLastName(e.target.value)}
                  className="w-full p-2 border border-gray-300 rounded-md"
                />
              </div>
              <div>
                <label className="block">Telefon Numarası</label>
                <input
                  type="text"
                  value={phoneNumber}
                  onChange={(e) => setPhoneNumber(e.target.value)}
                  className="w-full p-2 border border-gray-300 rounded-md"
                />
              </div>
            </div>
           {/*  <button
              onClick={handleProfileUpdate}
              className={`mt-4 bg-blue-500 text-white py-2 px-4 rounded-md ${isLoading ? "opacity-50" : ""}`}
              disabled={isLoading}
            >
              {isLoading ? "Güncelleniyor..." : "Profil Güncelle"}
            </button> */}
          </div>

          <div>
            <h3 className="text-xl font-medium mb-2">Şifre Güncelleme</h3>
            <div className="space-y-4">
              <div>
                <label className="block">Mevcut Şifre</label>
                <input
                  type="password"
                  value={currentPassword}
                  onChange={(e) => setCurrentPassword(e.target.value)}
                  className="w-full p-2 border border-gray-300 rounded-md"
                />
              </div>
              <div>
                <label className="block">Yeni Şifre</label>
                <input
                  type="password"
                  value={newPassword}
                  onChange={(e) => setNewPassword(e.target.value)}
                  className="w-full p-2 border border-gray-300 rounded-md"
                />
              </div>
              <div>
                <label className="block">Yeni Şifre (Tekrar)</label>
                <input
                  type="password"
                  value={reNewPassword}
                  onChange={(e) => setReNewPassword(e.target.value)}
                  className="w-full p-2 border border-gray-300 rounded-md"
                />
              </div>
            </div>
            <button
              onClick={handlePasswordUpdate}
              className={`mt-4 bg-red-500 text-white py-2 px-4 rounded-md ${isLoading ? "opacity-50" : ""}`}
              disabled={isLoading}
            >
              {isLoading ? "Güncelleniyor..." : "Şifre Güncelle"}
            </button>
          </div>

          <div className="mt-6">
            <button
              onClick={goToOrders}
              className="bg-green-600 text-white py-2 px-4 rounded-md"
            >
              Siparişlerim
            </button>
          </div>
        </div>
      ) : (
        <p>Yükleniyor...</p>
      )}
    </div>
  );
}
