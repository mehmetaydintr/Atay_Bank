using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutunamayanlar.Models.EntityFramework;

namespace Tutunamayanlar.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Guncelle()
        {
            var kullaniciadi = User.Identity.Name.Split(' ');
            long musteriNo = Int32.Parse(kullaniciadi[3]);
            var kullaniciDB = db.kullanici.FirstOrDefault(x => x.musteriNo == musteriNo);
            return View(kullaniciDB);
        }

        [HttpPost]
        public ActionResult Guncelle(kullanici user)
        {
            bool kontrol = false;
            if (user.ad == null)
            {
                kontrol = true;
                ViewBag.GuncelleMesajAd = "Lütfen Ad Giriniz";
            }
            if (user.soyad == null)
            {
                kontrol = true;
                ViewBag.GuncelleMesajSoyad = "Lütfen Soyad Giriniz";
            }
            if (user.adres == null)
            {
                kontrol = true;
                ViewBag.GuncelleMesajAdres = "Lütfen Adres Giriniz";
            }
            if (user.telefon == null)
            {
                kontrol = true;
                ViewBag.GuncelleMesajTelefon = "Lütfen Telefon Numarası Giriniz";
            }
            if (user.sifre == null)
            {
                kontrol = true;
                ViewBag.GuncelleMesajSifre = "Lütfen Şifre Giriniz";
            }
            else if (user.sifre.Length != 6)
            {
                kontrol = true;
                ViewBag.GuncelleMesajSifre = "Şifreniz 6 Haneli Olmalıdır";
            }
            if (user.email == null)
            {
                kontrol = true;
                ViewBag.GuncelleMesajMail = "Lütfen E-Mail Giriniz";
            }
            else if (!(user.email.EndsWith("@gmail.com") || user.email.EndsWith("@hotmail.com") || user.email.EndsWith("@outlook.com") || user.email.EndsWith("@outlook.com.tr") || user.email.EndsWith("@yandex.com")))
            {
                kontrol = true;
                ViewBag.GuncelleMesajMail = "Lütfen Geçerli Bir E-Mail Giriniz";
            }

            if (kontrol == false)
            {
                try
                {
                    var kullaniciadi = User.Identity.Name.Split(' ');
                    long musteriNo = Int32.Parse(kullaniciadi[3]);
                    var guncellenecekUser = db.kullanici.FirstOrDefault(x => x.musteriNo == musteriNo);

                    guncellenecekUser.ad = user.ad;
                    guncellenecekUser.soyad = user.soyad;
                    guncellenecekUser.adres = user.adres;
                    guncellenecekUser.email = user.email;
                    guncellenecekUser.sifre = user.sifre;
                    guncellenecekUser.telefon = user.telefon;

                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    ViewBag.GuncelleMesaj = "Lütfen Bilgilerinizi Kontrol Edip Tekrar Deneyiniz";
                }
            }

            return View();
        }
    }
}