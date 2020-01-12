using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutunamayanlar.Models.EntityFramework;

namespace Tutunamayanlar.Controllers
{
    [Authorize]
    public class HesapController : Controller
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();

        // GET: Hesap
        public ActionResult Hesaplarim()
        {
            var kullaniciadi = User.Identity.Name.Split(' ');
            long musteriNo = Int32.Parse(kullaniciadi[3]);
            var hesaplar = db.hesap.Where(x => x.musteriNo == musteriNo).ToList();
            ViewBag.hesap = hesaplar.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult YeniHesap()
        {
            int hesapNo = 1000;
            try
            {
                var kullaniciadi = User.Identity.Name.Split(' ');
                long musteriNo = Int32.Parse(kullaniciadi[3]);
                List<hesap> hesaps = db.hesap.Where(x => x.musteriNo == musteriNo).ToList();
                hesap eklenecekHesap = new hesap();
                eklenecekHesap.aktiflikDurumu = true;
                eklenecekHesap.bakiye = 0;
                eklenecekHesap.musteriNo = musteriNo;
                hesapNo = hesapNo + hesaps.Count + 1;
                eklenecekHesap.hesapNo = musteriNo + hesapNo.ToString();
                db.hesap.Add(eklenecekHesap);
                db.SaveChanges();
            }
            catch(Exception e)
            {

            }

            return RedirectToAction("Hesaplarim", "Hesap");
        }

        [HttpGet]
        public ActionResult ParaYatir(string id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var kullaniciadi = User.Identity.Name.Split(' ');
                long musteriNo = Int32.Parse(kullaniciadi[3]);
                var hesap = db.hesap.FirstOrDefault(x => x.musteriNo == musteriNo && x.hesapNo == id && x.aktiflikDurumu == true);
                if (hesap == null)
                {
                    return RedirectToAction("Hesaplarim", "Hesap");
                }
                ViewBag.ParaYatirBakiye = hesap.bakiye;
                hesap.bakiye = null;
                return View(hesap);
            }
        }

        [HttpPost]
        public ActionResult ParaYatir(string id, hesap hesap)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var kullaniciadi = User.Identity.Name.Split(' ');
                long musteriNo = Int32.Parse(kullaniciadi[3]);
                var guncellenecekHesap = db.hesap.FirstOrDefault(x => x.musteriNo == musteriNo && x.hesapNo == id && x.aktiflikDurumu == true);
                if (guncellenecekHesap == null)
                {
                    ViewBag.ParaYatirMesaj = "Böyle Bir Hesap Bulunamamıştır Lütfen Kontrol Edip Tekrar Deneyiniz";
                    return View();
                }
                if (hesap.bakiye <= 0)
                {
                    ViewBag.ParaYatirMesaj = "Lütfen Geçerli Tutar Giriniz";
                    return View();
                }
                if (hesap.bakiye == null)
                {
                    ViewBag.ParaYatirMesaj = "Lütfen Geçerli Tutar Giriniz";
                    return View();
                }
                try
                {
                    guncellenecekHesap.bakiye = guncellenecekHesap.bakiye + hesap.bakiye;
                    db.SaveChanges();
                    return RedirectToAction("Hesaplarim", "Hesap");
                }
                catch(Exception e){
                    ViewBag.ParaYatirMesaj = "Hata";
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult ParaCek(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var kullaniciadi = User.Identity.Name.Split(' ');
                long musteriNo = Int32.Parse(kullaniciadi[3]);
                var hesap = db.hesap.FirstOrDefault(x => x.musteriNo == musteriNo && x.hesapNo == id && x.aktiflikDurumu == true);
                if (hesap == null)
                {
                    return RedirectToAction("Hesaplarim", "Hesap");
                }
                ViewBag.ParaCekBakiye = hesap.bakiye;
                hesap.bakiye = null;
                return View(hesap);
            }
        }

        [HttpPost]
        public ActionResult ParaCek(string id, hesap hesap)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var kullaniciadi = User.Identity.Name.Split(' ');
                long musteriNo = Int32.Parse(kullaniciadi[3]);
                var guncellenecekHesap = db.hesap.FirstOrDefault(x => x.musteriNo == musteriNo && x.hesapNo == id);
                if (guncellenecekHesap == null)
                {
                    ViewBag.ParaYatirMesaj = "Böyle Bir Hesap Bulunamamıştır Lütfen Kontrol Edip Tekrar Deneyiniz";
                    return View();
                }
                if (guncellenecekHesap.bakiye < hesap.bakiye)
                {
                    ViewBag.ParaYatirMesaj = "Yetersiz Bakiye";
                    return View();
                }
                if (hesap.bakiye <= 0)
                {
                    ViewBag.ParaYatirMesaj = "Lütfen Geçerli Bakiye Giriniz";
                    return View();
                }
                if (hesap.bakiye == null)
                {
                    ViewBag.ParaYatirMesaj = "Lütfen Geçerli Tutar Giriniz";
                    return View();
                }
                try
                {
                    guncellenecekHesap.bakiye = guncellenecekHesap.bakiye - hesap.bakiye;
                    db.SaveChanges();
                    return RedirectToAction("Hesaplarim", "Hesap");
                }
                catch (Exception e)
                {
                    ViewBag.ParaYatirMesaj = "Hata";
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult Sil(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var kullaniciadi = User.Identity.Name.Split(' ');
                long musteriNo = Int32.Parse(kullaniciadi[3]);
                var hesap = db.hesap.FirstOrDefault(x => x.musteriNo == musteriNo && x.hesapNo == id && x.aktiflikDurumu == true);
                if (hesap == null)
                {
                    return RedirectToAction("Hesaplarim", "Hesap");
                }
                if(hesap.bakiye>0)
                {
                    return  RedirectToAction("Hesaplarim", "Hesap");
                }
                try
                {
                    hesap.aktiflikDurumu = false;
                    db.SaveChanges();
                }catch(Exception e)
                {

                }
                return RedirectToAction("Hesaplarim", "Hesap");
            }
        }


    }
}