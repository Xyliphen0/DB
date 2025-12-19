Microsoft.EntityFrameworkCore Microsoft.EntityFrameworkCore.Design Microsoft.EntityFrameworkCore.SqlServer Microsoft.EntityFrameworkCore.Tools Scaffold-DbContext
using Microsoft.EntityFrameworkCore;

Scaffold - DbContext "Server=SUNUCU_ADRESI_BURAYA;Database=VERITABANI_ADI_BURAYA;User Id=KULLANICI_ADI_BURAYA;Password=SIFRE_BURAYA;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - Force

Scaffold - DbContext "Server=(localdb)\MSSQLLocalDB;Database=SinavDeneme;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - Force

==========================================================================================================================================================================================================================================
// ---------------------------------------------------------
            // 2. ADIM: VERİ EKLEME (INSERT) - 10 PUAN
            // ---------------------------------------------------------
            try
            {
                Console.WriteLine("\n--- Veri Ekleniyor ---");

                var yeniUrun = new Urunler()
                {
                    // ID (Primary Key) genelde otomatik olduğu için yazılmaz.
                    Ad = "Sınav Kalemi",
                    StokAdeti = 100,
                    SonKullanmaTarihi = DateTime.Now.AddYears(1), // 1 yıl sonra
                    GeriDonusturebilir = true,
                    Fiyat = 50
                };

                // DİKKAT: "db.Urunler" mi yoksa "db.Urunlers" mi?
                // Noktayı koyunca çıkan listeden doğrusunu seç.
                db.Urunlers.Add(yeniUrun);
                db.SaveChanges();

                Console.WriteLine("BAŞARILI: Veri veritabanına eklendi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("HATA (Ekleme): " + ex.Message);
            }

==========================================================================================================================================================================================================================================

using SinavUygulamasi.Models; // Kendi proje adını buraya yaz
using System;
using System.Linq;

==========================================================================================================================================================================================================================================

BELEDİYE DB

// 1. ADIM: Veritabanı bağlantısını çağır (Scaffold ile gelen Context ismini yaz)
using SinavUygulamasi.Models;

using var context = new SinavDenemeContext();

// =============================================================
// SORU 1: 01.01.2015'ten yeni ve büyüklüğü 6 veya daha küçük araçlar
// =============================================================

// Önce tarih sınırını belirle
DateTime sinirTarih = new DateTime(2015, 1, 1);

// LINQ Sorgusu
var uygunAraclar = context.Araclars
    .Where(x => x.ModelTarihi > sinirTarih && x.Buyukluk <= 6)
    .ToList();

// Sonucu Hocaya Göstermek İçin Ekrana Yazdır
Console.WriteLine("--- UYGUN ARAÇLAR LİSTESİ ---");
foreach (var arac in uygunAraclar)
{
    // Burada aracın adını veya plakasını yazdırabilirsin
    Console.WriteLine($"Araç ID: {arac.Id} - Model Tarihi: {arac.ModelTarihi}");
}

// =============================================================
// SORU 2: Yetki seviyesi 3 olan yetki adı nedir?
// =============================================================

// LINQ Sorgusu (Tek bir veri aradığımız için FirstOrDefault kullanıyoruz)
var yetki = context.Yetkilers
    .FirstOrDefault(x => x.YetkiSeviyesi == 3);

Console.WriteLine("\n--- YETKİ SORGUSU ---");
if (yetki != null)
{
    Console.WriteLine($"Seviyesi 3 Olan Yetkinin Adı: {yetki.YetkiAdi}");
}
else
{
    Console.WriteLine("Seviyesi 3 olan bir yetki bulunamadı.");
}


// Eğer sütun sadece yıl tutuyorsa (int ise)
DateTime sinirYil = new DateTime(2015, 1, 1);
var uygunAraclars = context.Araclars
    .Where(x => x.ModelTarihi > sinirYil && x.Buyukluk <= 6)
    .ToList();

==========================================================================================================================================================================================================================================
==========================================================================================================================================================================================================================================

BİSİKLETİÇİ DB

// 1. ADIM: Context'i oluştur (İsmi BisikletciContext veya benzeri olacaktır)
using SinavUygulamasi.Models;
using var context = new SinavDenemeContext();

// =================================================================================
// SORU 1: 15.12.2024 itibariyle tarihi geçmiş ve geri dönüştürülebilen ürünler
// =================================================================================

// Kıyaslanacak tarihi belirle (Yıl, Ay, Gün formatında)
DateTime sinirTarih = new DateTime(2024, 12, 15);

// LINQ Sorgusu
// Mantık: SonKullanmaTarihi < sinirTarih (Tarihi geçmiş) VE GeriDonusturulebilir == true
var copUrunler = context.Urunlers
    .Where(x => x.SonKullanmaTarihi < sinirTarih && x.GeriDonusturulebilir == true)
    .ToList();

// Sonuçları Ekrana Yazdır
Console.WriteLine("--- TARİHİ GEÇMİŞ VE GERİ DÖNÜŞEBİLEN ÜRÜNLER ---");
foreach (var urun in copUrunler)
{
    Console.WriteLine($"Ürün Adı: {urun.UrunAdi} - SKT: {urun.SonKullanmaTarihi}");
}

// =================================================================================
// SORU 2: Yetki seviyesi 3'ten büyük olan yetki adları
// =================================================================================

// LINQ Sorgusu
// Mantık: YetkiSeviyesi > 3 olanları bul, sadece isimlerini (YetkiAdi) seç.
var yuksekYetkiler = context.Yetkilers
    .Where(x => x.YetkiSeviyesi > 3)
    .Select(x => x.YetkiAdi) // Sadece adlarını istediği için Select kullandık
    .ToList();

// Sonuçları Ekrana Yazdır
Console.WriteLine("\n--- SEVİYESİ 3'TEN BÜYÜK YETKİLER ---");
foreach (var yetkiAdi in yuksekYetkiler)
{
    Console.WriteLine($"Yetki Adı: {yetkiAdi}");
}

==========================================================================================================================================================================================================================================
==========================================================================================================================================================================================================================================

DERSHANE DB

// 1. ADIM: Context'i oluştur (Scaffold sonrası oluşan isimle, örn: DershaneContext)
using SinavUygulamasi.Models;
using var context = new SinavDenemeContext();


// =================================================================================
// SORU 1: 15.12.2024 tarihinde başvurusu AKTİF olan sınavlar
// =================================================================================

// Bize verilen kontrol tarihi
DateTime kontrolTarihi = new DateTime(2024, 12, 15);

// LINQ Sorgusu
// Mantık: 
// 1. Başvuru Başlangıç Tarihi, bizim tarihimizden küçük veya eşit olmalı (Başlamış olmalı)
// 2. Başvuru Bitiş Tarihi, bizim tarihimizden büyük veya eşit olmalı (Henüz bitmemiş olmalı)
var aktifSinavlar = context.Sinavlars
    .Where(x => x.BasvuruBaslangicTarihi <= kontrolTarihi && x.BasvuruBitisTarihi >= kontrolTarihi)
    .ToList();

// Sonuçları Ekrana Yazdır
Console.WriteLine("--- 15.12.2024 İTİBARİYLE BAŞVURUSU AÇIK OLAN SINAVLAR ---");
foreach (var sinav in aktifSinavlar)
{
    Console.WriteLine($"Sınav Adı: {sinav.SinavAdi} - Bitiş: {sinav.BasvuruBitisTarihi}");
}

// =================================================================================
// SORU 2: 2025 yılı içerisinde kaç tane sınav yapılacaktır?
// =================================================================================

// LINQ Sorgusu
// Mantık: Sınav tarihinin YILI (.Year) 2025'e eşit olanları SAY (.Count)
DateTime baslangic = new DateTime(2025, 1, 1);
DateTime bitis = new DateTime(2025, 12, 31);

int sinavSayisi = context.Sinavlars
    .Count(x => x.SinavTarihi >= baslangic && x.SinavTarihi <= bitis);
// Sonucu Ekrana Yazdır
Console.WriteLine($"\n2025 Yılında Yapılacak Toplam Sınav Sayısı: {sinavSayisi}");

==========================================================================================================================================================================================================================================
==========================================================================================================================================================================================================================================

KIRTASİYE DB

// 1. ADIM: Context'i çağır (İsim: KirtasiyeContext vb. olabilir)
using SinavUygulamasi.Models;
using var context = new SinavDenemeContext();

// =================================================================================
// SORU 1: 15.12.2024 tarihi geçmiş ve geri dönüştürülebilen ürünlerin ADETLERİ
// =================================================================================

DateTime sinirTarih = new DateTime(2024, 12, 15);

// 1. LINQ Sorgusu - 'Adet' yerine 'Stok' kullanıldı
var urunListesi = context.Urunlers
    .Where(x => x.SonKullanmaTarihi < sinirTarih && x.GeriDonusturulebilir == true)
    .Select(x => new { x.UrunAdi, x.Stok }) // 'Adet' olan yer 'Stok' yapıldı
    .ToList();

// 2. Sonuçları Ekrana Yazdır
Console.WriteLine("--- TARİHİ GEÇMİŞ & GERİ DÖNÜŞEN ÜRÜN ADETLERİ ---");
foreach (var satir in urunListesi)
{
    // Anonim tip içinde 'Stok' ismini kullandığımız için burada da satir.Stok diyoruz
    Console.WriteLine($"Ürün: {satir.UrunAdi} - Stok Adeti: {satir.Stok}");
}
// Sonuçları Ekrana Yazdır
Console.WriteLine("--- TARİHİ GEÇMİŞ & GERİ DÖNÜŞEN ÜRÜN ADETLERİ ---");
foreach (var satir in urunListesi)
{
    // Burada satir.UrunAdi ve satir.Adet diyerek verilere ulaşabilirsin
    Console.WriteLine($"Ürün: {satir.UrunAdi} - Stok Adeti: {satir.Stok}");
}

// =================================================================================
// SORU 2: Yetki seviyesi 2'den küçük olan yetki adları
// =================================================================================

// LINQ Sorgusu
// Mantık: Seviyesi 2'den KÜÇÜK olanları bul -> Sadece ADINI (YetkiAdi) seç.
var dusukYetkiler = context.Yetkilers
    .Where(x => x.YetkiSeviyesi < 2)
    .Select(x => x.YetkiAdi) // Sadece string listesi döner
    .ToList();

// Sonuçları Ekrana Yazdır
Console.WriteLine("\n--- SEVİYESİ 2'DEN KÜÇÜK YETKİLER ---");
foreach (var ad in dusukYetkiler)
{
    Console.WriteLine($"Yetki Adı: {ad}");
}

==========================================================================================================================================================================================================================================
==========================================================================================================================================================================================================================================

KÜTÜPHANE DB

// 1. ADIM: Context'i oluştur (KutuphaneContext vb.)
using var context = new KutuphaneContext();

// =================================================================================
// SORU 1: İki ALES sınavı arasındaki gün farkı
// =================================================================================

// Strateji: Önce isminde "ALES" geçen sınavları veritabanından çekelim.
// Sonra C# tarafında tarihleri birbirinden çıkaralım.

var alesSinavlari = context.Sinavlar
    .Where(x => x.SinavAdi.Contains("ALES")) // Adında ALES geçenleri bul
    .OrderBy(x => x.SinavTarihi)             // Tarihe göre eskiden yeniye sırala
    .ToList();

Console.WriteLine("--- ALES SINAVLARI ARASINDAKİ GÜN FARKI ---");

// Hata almamak için listede en az 2 sınav var mı kontrol edelim
if (alesSinavlari.Count >= 2)
{
    // Listeyi tarihe göre sıraladığımız için:
    // [0] -> İlk ALES
    // [1] -> İkinci ALES (veya sonuncuyu almak için alesSinavlari.Last() kullanılabilir)
    
    DateTime tarih1 = alesSinavlari[0].SinavTarihi;
    DateTime tarih2 = alesSinavlari[1].SinavTarihi;

    // Tarihleri birbirinden çıkarınca sonuç "TimeSpan" (Zaman Aralığı) türünde olur
    TimeSpan fark = tarih2 - tarih1;

    Console.WriteLine($"İlk Sınav: {tarih1.ToShortDateString()}");
    Console.WriteLine($"İkinci Sınav: {tarih2.ToShortDateString()}");
    Console.WriteLine($"Aradaki Gün Sayısı: {fark.TotalDays}"); // Sonuç gün olarak döner
}
else
{
    Console.WriteLine("Yeterli sayıda ALES sınavı kaydı bulunamadı.");
}

// =================================================================================
// SORU 2: Başvurusu geçen sınavların listesi
// =================================================================================

// Şu anki tarihi alalım
DateTime bugun = DateTime.Now;

// LINQ Sorgusu
// Mantık: Başvuru Bitiş Tarihi, bugünden KÜÇÜK olanlar (Tarih geçmiş demek)
var suresiGecenler = context.Sinavlar
    .Where(x => x.BasvuruBitisTarihi < bugun)
    .ToList();

Console.WriteLine("\n--- BAŞVURU SÜRESİ GEÇMİŞ SINAVLAR ---");
foreach (var sinav in suresiGecenler)
{
    Console.WriteLine($"Sınav: {sinav.SinavAdi} - Son Başvuru: {sinav.BasvuruBitisTarihi}");
}

ÇALIŞAN VERSİYON

using SinavUygulamasi.Models;
using System;
using System.Linq;

// 1. ADIM: Bağlantıyı kur (Görselindeki isme göre)
using var context = new SinavDenemeContext();

// =================================================================================
// SORU 1: İki ALES sınavı arasındaki gün farkı
// =================================================================================

// 1. Veritabanından adında "ALES" geçen ve tarihi boş olmayan sınavları çek
var alesSinavlari = context.Sinavlars
    .Where(x => x.SinavAdi.Contains("ALES") && x.SinavTarihi != null)
    .OrderBy(x => x.SinavTarihi)
    .ToList();

Console.WriteLine("--- ALES SINAVLARI ARASINDAKİ GÜN FARKI ---");

if (alesSinavlari.Count >= 2)
{
    // DateTime? olan veriyi .Value ile DateTime'a çeviriyoruz
    DateTime tarih1 = alesSinavlari[0].SinavTarihi.Value;
    DateTime tarih2 = alesSinavlari[1].SinavTarihi.Value;

    // İki tarih arasındaki farkı hesaplıyoruz [cite: 41]
    TimeSpan fark = tarih2 - tarih1;

    Console.WriteLine($"İlk Sınav: {tarih1.ToShortDateString()}");
    Console.WriteLine($"İkinci Sınav: {tarih2.ToShortDateString()}");
    Console.WriteLine($"Aradaki Gün Sayısı: {fark.TotalDays}");
}
else
{
    Console.WriteLine("Hesaplama için yeterli sayıda (en az 2) ALES sınavı bulunamadı.");
}

// =================================================================================
// SORU 2: Başvurusu geçen sınavların listesi
// =================================================================================

DateTime bugun = DateTime.Now;

// Başvuru bitiş tarihi bugünden küçük olanları filtrele [cite: 45]
var suresiGecenler = context.Sinavlars
    .Where(x => x.BasvuruBitisTarihi < bugun)
    .ToList();

Console.WriteLine("\n--- BAŞVURU SÜRESİ GEÇMİŞ SINAVLAR ---");
if (suresiGecenler.Any())
{
    foreach (var sinav in suresiGecenler)
    {
        Console.WriteLine($"Sınav: {sinav.SinavAdi} - Son Başvuru: {sinav.BasvuruBitisTarihi}");
    }
}
else
{
    Console.WriteLine("Başvuru süresi geçmiş sınav bulunamadı.");
}

==========================================================================================================================================================================================================================================
==========================================================================================================================================================================================================================================

MARKET DB

// 1. ADIM: Context'i çağır (MarketContext vb.)
using SinavUygulamasi.Models;

using var context = new SinavDenemeContext();

// =================================================================================
// SORU 1: Adeti 1000'den az olan VE geri dönüştürülemeyen ürünler
// =================================================================================

// LINQ Sorgusu
// Mantık: Adet < 1000 VE (&&) GeriDonusturulebilir == false
var copUrunler = context.Urunlers
    .Where(x => x.Stok < 1000 && x.GeriDonusturulebilir == false)
    .ToList();

// Sonuçları Ekrana Yazdır
Console.WriteLine("--- STOK AZ VE GERİ DÖNÜŞMEYENLER ---");
foreach (var urun in copUrunler)
{
    Console.WriteLine($"Ürün: {urun.UrunAdi} - Adet: {urun.Stok}");
}

// =================================================================================
// SORU 2: En az stok olan ürün geri dönüştürülebiliyor mu?
// =================================================================================

// Strateji: Önce stoğa göre küçükten büyüğe sırala (OrderBy), sonra ilk sıradakini al (FirstOrDefault).

var enAzStokluUrun = context.Urunlers
    .OrderBy(x => x.Stok) // Küçükten büyüğe sıralar
    .FirstOrDefault();    // İlkini (en küçüğünü) alır

Console.WriteLine("\n--- EN AZ STOKLU ÜRÜN DURUMU ---");

if (enAzStokluUrun != null)
{
    Console.WriteLine($"En Az Stoklu Ürün: {enAzStokluUrun.UrunAdi} (Stok: {enAzStokluUrun.Stok})");

    // Geri dönüştürülebilir mi kontrolü
    if (enAzStokluUrun.GeriDonusturulebilir == true)
    {
        Console.WriteLine("CEVAP: EVET, bu ürün geri dönüştürülebilir.");
    }
    else
    {
        Console.WriteLine("CEVAP: HAYIR, bu ürün geri dönüştürülemez.");
    }
}
else
{
    Console.WriteLine("Veritabanında hiç ürün yok.");
}

==========================================================================================================================================================================================================================================
==========================================================================================================================================================================================================================================

OKUL DB

// 1. ADIM: Context'i çağır (OkulContext vb.)
using SinavUygulamasi.Models;

using var context = new SinavDenemeContext();

// =================================================================================
// SORU 1: Başvuru süresi EN KISA olan sınav hangisi?
// =================================================================================

var enKisaSureliSinav = context.Sinavlars
    .Where(x => x.BasvuruBitisTarihi != null && x.BasvuruBaslangicTarihi != null)
    .AsEnumerable() // KRİTİK ADIM: Buradan sonrasını C# (RAM) üzerinde yap diyoruz.
    .OrderBy(x => (x.BasvuruBitisTarihi.Value - x.BasvuruBaslangicTarihi.Value).TotalDays)
    .FirstOrDefault();

Console.WriteLine("--- BAŞVURU SÜRESİ EN KISA OLAN SINAV ---");

if (enKisaSureliSinav != null)
{
    // Artık RAM'de olduğumuz için çıkarma işlemi hata vermez. [cite: 41, 63]
    TimeSpan sure = enKisaSureliSinav.BasvuruBitisTarihi.Value - enKisaSureliSinav.BasvuruBaslangicTarihi.Value;

    Console.WriteLine($"Sınav Adı: {enKisaSureliSinav.SinavAdi}");
    Console.WriteLine($"Başvuru Süresi: {Math.Round(sure.TotalDays)} Gün");
}
else
{
    Console.WriteLine("Veritabanında uygun sınav kaydı bulunamadı.");
}
// =================================================================================
// SORU 2: Yapılması planlanan EN SON sınav hangisi?
// =================================================================================

// Strateji: Sınav tarihine göre "Büyükten Küçüğe" (Yeniden Eskiye) sırala ve ilkini al.
// OrderByDescending kullanacağız.

var enSonSinav = context.Sinavlars
    .OrderByDescending(x => x.SinavTarihi) // En ileri tarihi en başa al
    .FirstOrDefault();

Console.WriteLine("\n--- PLANLANAN EN SON SINAV ---");

if (enSonSinav != null)
{
    Console.WriteLine($"Sınav Adı: {enSonSinav.SinavAdi}");
    Console.WriteLine($"Tarihi: {enSonSinav.SinavTarihi}");
}
else
{
    Console.WriteLine("Veritabanında sınav bulunamadı.");
}

==========================================================================================================================================================================================================================================
==========================================================================================================================================================================================================================================

OTO YEDEK PARÇA DB

// 1. ADIM: Context'i çağır (OtoParcaContext vb.)
using SinavUygulamasi.Models;

using var context = new SinavDenemeContext();

// =================================================================================
// SORU 1: Geri dönüştürülebilir olup Son Kullanma Tarihi OLMAYAN (Null) ürün var mı?
// =================================================================================

// LINQ Sorgusu
// Mantık: GeriDonusturulebilir == true VE (&&) SonKullanmaTarihi == null
bool urunVarMi = context.Urunlers
    .Any(x => x.GeriDonusturulebilir == true && x.SonKullanmaTarihi == null);

Console.WriteLine("--- TANIMSIZ TARİHLİ GERİ DÖNÜŞÜM ÜRÜNÜ KONTROLÜ ---");

if (urunVarMi)
{
    Console.WriteLine("CEVAP: EVET, böyle ürünler veritabanında mevcut.");

    // (Opsiyonel) Hocaya kanıtlamak istersen o ürünleri listeleyebilirsin:
    /*
    var hataliUrunler = context.Urunler
        .Where(x => x.GeriDonusturulebilir == true && x.SonKullanmaTarihi == null)
        .ToList();
        
    foreach(var u in hataliUrunler) 
    {
        Console.WriteLine($"Bulunan Ürün: {u.UrunAdi}");
    }
    */
}
else
{
    Console.WriteLine("CEVAP: HAYIR, böyle bir ürün bulunamadı.");
}

// =================================================================================
// SORU 2: Toplam ürün sayısı nedir?
// =================================================================================

// LINQ Sorgusu
// En basit komut: .Count()
int toplamSayi = context.Urunlers.Count();

Console.WriteLine($"\n--- TOPLAM ÜRÜN İSTATİSTİĞİ ---");
Console.WriteLine($"Veritabanındaki Toplam Ürün Sayısı: {toplamSayi}");

==========================================================================================================================================================================================================================================
==========================================================================================================================================================================================================================================

RESTORAN DB

// 1. ADIM: Context'i çağır (RestoranContext vb.)
using SinavUygulamasi.Models;

using var context = new SinavDenemeContext();

// =================================================================================
// SORU 1: Geri dönüştürülebilen ürünlerin yüzdesi ne kadardır?
// =================================================================================

// Adım 1: Toplam ürün sayısını bul
int toplamUrunSayisi = context.Urunlers.Count();

Console.WriteLine("--- İSTATİSTİKLER ---");

if (toplamUrunSayisi > 0)
{
    // Adım 2: Geri dönüştürülebilen (true olan) ürün sayısını bul
    int geriDonusenSayisi = context.Urunlers
        .Count(x => x.GeriDonusturulebilir == true);

    // Adım 3: Yüzdeyi hesapla
    // Dikkat: Bölme işleminin ondalıklı çıkması için sayılardan birini (double) yapmalıyız.
    // Yoksa C# tamsayı bölmesi yapar ve sonuç 0 çıkar!
    double yuzde = ((double)geriDonusenSayisi / toplamUrunSayisi) * 100;

    Console.WriteLine($"Toplam Ürün: {toplamUrunSayisi}");
    Console.WriteLine($"Geri Dönüşen Ürün: {geriDonusenSayisi}");

    // "F2" kodu, virgülden sonra sadece 2 basamak göster demektir (Örn: %45.23)
    Console.WriteLine($"Geri Dönüşüm Oranı: %{yuzde.ToString("F2")}");
}
else
{
    Console.WriteLine("Hesaplanacak ürün yok (Veritabanı boş).");
}

// =================================================================================
// SORU 2: Son kullanma tarihi tanımlı olmayan (NULL) kaç adet ürün vardır?
// =================================================================================

// LINQ Sorgusu
// Mantık: SonKullanmaTarihi sütunu NULL olanları SAY (.Count)
int tarihizUrunSayisi = context.Urunlers
    .Count(x => x.SonKullanmaTarihi == null);

Console.WriteLine($"\nSon Kullanma Tarihi Olmayan Ürün Sayısı: {tarihizUrunSayisi}");

==========================================================================================================================================================================================================================================
==========================================================================================================================================================================================================================================

KLİMA SİSTEMLERİ DB

using SinavUygulamasi.Models; // Kendi proje adını buraya yaz
using System;
using System.Linq;

// 1. ADIM: Context'i oluştur (Scaffold sonrası oluşan isimle, örn: KlimaContext)
using var context = new SinavDenemeContext();

Console.WriteLine("--- KLİMA SİSTEMLERİ DB SORGULARI ---");

// =================================================================================
// SORU 1: Ürün adeti 500 ve altı, tarihi geçmiş ve geri dönüştürülebilir olanların ADLARI
// =================================================================================

DateTime bugun = DateTime.Now;

// LINQ Sorgusu
var uygunUrunAdlari = context.Urunlers
    .Where(x => x.Stok <= 500 && x.SonKullanmaTarihi < bugun && x.GeriDonusturulebilir == true)
    .Select(x => x.UrunAdi) // Sadece ürün adlarını seçiyoruz
    .ToList();

Console.WriteLine("\n--- ŞARTLARA UYGAN ÜRÜN İSİMLERİ ---");
foreach (var ad in uygunUrunAdlari)
{
    Console.WriteLine($"Ürün Adı: {ad}");
}

// =================================================================================
// SORU 2: Yetkileri yetki seviyesine göre büyükten küçüğe sırala
// =================================================================================

// LINQ Sorgusu
// Mantık: OrderByDescending kullanarak seviyeyi 5, 4, 3... şeklinde sıralarız.
var siraliYetkiler = context.Yetkilers
    .OrderByDescending(x => x.YetkiSeviyesi)
    .ToList();

Console.WriteLine("\n--- YETKİ SEVİYESİNE GÖRE SIRALI LİSTE (BÜYÜKTEN KÜÇÜĞE) ---");
foreach (var yetki in siraliYetkiler)
{
    Console.WriteLine($"Yetki: {yetki.YetkiAdi} - Seviye: {yetki.YetkiSeviyesi}");
}

Console.ReadLine(); // Ekranın kapanmaması için

using Microsoft.EntityFrameworkCore

using System;
using System.Linq;
using İleriSinavFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace IleriSinav
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sınav Uygulaması Başlatılıyor...");

            // ---------------------------------------------------------
            // 1. ADIM: BAĞLANTIYI KUR
            // ---------------------------------------------------------
            // DİKKAT: "SinavHazirlikDBContext" ismi Models klasöründeki dosya adıyla aynı olmalı.
            // Eğer altı kırmızı yanarsa, Models klasörüne bak ismini düzelt.
            SinavHazirlikDbContext db = new SinavHazirlikDbContext();

            // ---------------------------------------------------------
            // 2. ADIM: VERİ EKLEME (INSERT) - 10 PUAN
            // ---------------------------------------------------------
            try
            {
                Console.WriteLine("\n--- Veri Ekleniyor ---");

                var yeniUrun = new Urunler()
                {
                    // ID (Primary Key) genelde otomatik olduğu için yazılmaz.
                    Ad = "Sınav Kalemi",
                    StokAdeti = 100,
                    SonKullanmaTarihi = DateTime.Now.AddYears(1), // 1 yıl sonra
                    GeriDonusturebilir = true,
                    Fiyat = 50
                };

                // DİKKAT: "db.Urunler" mi yoksa "db.Urunlers" mi?
                // Noktayı koyunca çıkan listeden doğrusunu seç.
                db.Urunlers.Add(yeniUrun);
                db.SaveChanges();

                Console.WriteLine("BAŞARILI: Veri veritabanına eklendi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("HATA (Ekleme): " + ex.Message);
            }

            // ---------------------------------------------------------
            // 3. ADIM: SORGULAMA (LINQ) - 25 PUAN
            // ---------------------------------------------------------
            var copUrunler = db.Urunlers
            .Where(x => x.StokAdeti < 500 && x.SonKullanmaTarihi < DateTime.Now && x.GeriDonusturebilir == true)
            .ToList();

            foreach (var x in copUrunler)
            {
                // 2. x.Id noktasından sonraki hatayı düzelttik.
                Console.WriteLine($"ID: {x.Id} | Ad: {x.Ad} | Stok: {x.StokAdeti} | Geri Dönüşüm: {x.GeriDonusturebilir} | Fiyat: {x.Fiyat}");
            }

            Console.ReadLine();
        }
    }
}