Çok Fonksiyonlu Program

Bu proje, kullanıcıya çeşitli işlevler sunan bir C# konsol uygulamasıdır. Program, aşağıdaki üç ana işlevi yerine getirebilir:

Hesap Makinesi

Rastgele Sayı Tahmin Oyunu

Not Ortalaması Hesaplama

Kullanım

Program çalıştırıldığında, kullanıcıya üç seçenek sunulur. Kullanıcı, istediği işlevi seçmek için bir sayı girer ve ardından ilgili işlev devreye girer.

1. Hesap Makinesi

Kullanıcı, iki sayı girerek aşağıdaki işlemlerden birini gerçekleştirebilir:

Toplama

Çıkarma

Çarpma

Bölme

İlk sayının karesini hesaplama

Örnek Kullanım:

İlk sayıyı girin: 5

İkinci sayıyı girin: 3

İşlem seçin: 1 (Toplama)Sonuç: 5 + 3 = 8

Not: Bölme işlemi sırasında ikinci sayı sıfır olamaz.

2. Rastgele Sayı Tahmin Oyunu

Bilgisayar, 1 ile 100 arasında rastgele bir sayı seçer ve kullanıcıdan bu sayıyı tahmin etmesini ister. Kullanıcıya 5 tahmin hakkı verilir. Program, tahminlerin doğru olup olmadığını ve ipuçlarını kullanıcıya bildirir:

"Daha büyük bir sayı tahmin edin!"

"Daha küçük bir sayı tahmin edin!"

Örnek Kullanım:

Bilgisayarın seçtiği sayı: 42

Kullanıcı tahmini: 50 → Daha küçük bir sayı tahmin edin!

Kullanıcı tahmini: 40 → Daha büyük bir sayı tahmin edin!

Kullanıcı tahmini: 42 → Tebrikler! Doğru tahmin.

Not: Kullanıcı tahmin hakkını doldurduğunda doğru sayı açıklanır.

3. Not Ortalaması Hesaplama

Kullanıcıdan üç farklı dersin notu istenir ve bu notların ortalaması hesaplanır. Hesaplanan ortalamaya göre harf notu ve Ajda Pekkan’dan özel bir mesaj kullanıcıya gösterilir.

Örnek Kullanım:

İlk notu girin: 90

İkinci notu girin: 85

Üçüncü notu girin: 80

Sonuç:

Ortalama: 85.00

Harf Notu: BA

Mesaj: "Ajda Pekkan Seninle Gurur Duyuyor"

Not: Notlar 0 ile 100 arasında olmalıdır. Geçersiz bir not girildiğinde hata mesajı gösterilir.

Hata Yönetimi

Programda kullanıcı hatalarına karşı kapsamılı bir hata yönetimi sistemi mevcuttur:

Geçersiz sayı girişi yapıldığında, kullanıcı bilgilendirilir ve yeniden giriş yapması istenir.

Beklenmeyen bir hata meydana gelirse, kullanıcıya hatanın sebebi açıklanır.