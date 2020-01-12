using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tutunamayanlar.Models.EntityFramework;

namespace Tutunamayanlar.Controllers
{
    public class LoginController : Controller
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(kullanici user)
        {
            var kullaniciDB = db.kullanici.FirstOrDefault(x => x.TCKN == user.TCKN && x.sifre == user.sifre);
            if (kullaniciDB != null)
            {
                FormsAuthentication.SetAuthCookie($"{kullaniciDB.ad} {kullaniciDB.soyad} {kullaniciDB.email} {kullaniciDB.musteriNo}", true);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.GirisMesaj = "Geçersiz TC Kimlik No veya Şifre";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(kullanici user)
        {
            bool kontrol = false;
            if (user.TCKN == null)
            {
                kontrol = true;
                ViewBag.KayitMesajTCKN = "Lütfen TC Kimlik No Giriniz";
            }
            else if (user.TCKN.Length != 11)
            {
                kontrol = true;
                ViewBag.KayitMesajTCKN = "TC Kimlik No 11 Haneli Olmalıdır";
            }
            if (user.ad == null)
            {
                kontrol = true;
                ViewBag.KayitMesajAd = "Lütfen Ad Giriniz";
            }
            if (user.soyad == null)
            {
                kontrol = true;
                ViewBag.KayitMesajSoyad = "Lütfen Soyad Giriniz";
            }
            if (user.adres == null)
            {
                kontrol = true;
                ViewBag.KayitMesajAdres = "Lütfen Adres Giriniz";
            }
            if (user.telefon == null)
            {
                kontrol = true;
                ViewBag.KayitMesajTelefon = "Lütfen Telefon Numarası Giriniz";
            }
            if (user.sifre == null)
            {
                kontrol = true;
                ViewBag.KayitMesajSifre = "Lütfen Şifre Giriniz";
            }
            else if (user.sifre.Length != 6)
            {
                kontrol = true;
                ViewBag.KayitMesajSifre = "Şifreniz 6 Haneli Olmalıdır";
            }
            if (user.email == null)
            {
                kontrol = true;
                ViewBag.KayitMesajMail= "Lütfen E-Mail Giriniz";
            }
            else if ( !(user.email.EndsWith("@gmail.com") || user.email.EndsWith("@hotmail.com") || user.email.EndsWith("@outlook.com") || user.email.EndsWith("@outlook.com.tr") || user.email.EndsWith("@yandex.com")))
            {
                kontrol = true;
                ViewBag.KayitMesajMail = "Lütfen Geçerli Bir E-Mail Giriniz";
            }

            if (kontrol == false)
            {
                try
                {
                    db.kullanici.Add(user);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie($"{user.ad} {user.soyad} {user.email} {user.musteriNo}", true);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}