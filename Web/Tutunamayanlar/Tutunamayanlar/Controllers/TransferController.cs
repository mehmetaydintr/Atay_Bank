using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutunamayanlar.Models.EntityFramework;

namespace Tutunamayanlar.Controllers
{
    [Authorize]
    public class TransferController : Controller
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();

        // GET: Transfer
        public ActionResult Hesaplarim()
        {
            var kullaniciadi = User.Identity.Name.Split(' ');
            long musteriNo = Int32.Parse(kullaniciadi[3]);
            var hesaplar = db.hesap.Where(x => x.musteriNo == musteriNo).ToList();
            ViewBag.hesaplar = hesaplar.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Havale(string id)
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
                    return RedirectToAction("Hesaplarim", "Transfer");
                }
                ViewBag.TransferBakiye = hesap.bakiye;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Havale(string id, havale_virman transfer)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var kullaniciadi = User.Identity.Name.Split(' ');
                long musteriNo = Int32.Parse(kullaniciadi[3]);
                var gondericiHesap = db.hesap.FirstOrDefault(x => x.musteriNo == musteriNo && x.hesapNo == id && x.aktiflikDurumu == true);
                var aliciHesap = db.hesap.FirstOrDefault(x => x.hesapNo == transfer.aliciHesapNo && x.aktiflikDurumu == true);
                if (gondericiHesap == null || aliciHesap == null)
                {
                    ViewBag.TransferMesaj = "Böyle Bir Hesap Bulunamamıştır Lütfen Kontrol Edip Tekrar Deneyiniz";
                    return View();
                }
                if (gondericiHesap.hesapNo == aliciHesap.hesapNo)
                {
                    ViewBag.TransferMesaj = "Aynı Hesaba Para Gönderemezsiniz";
                    return View();
                }
                if (transfer.tutar <= 0)
                {
                    ViewBag.TransferMesaj = "Lütfen Geçerli Tutar Giriniz";
                    return View();
                }
                if(gondericiHesap.bakiye < transfer.tutar)
                {
                    ViewBag.TransferMesaj = "Yetersiz Bakiye";
                    return View();
                }
                try
                {
                   if(aliciHesap.musteriNo == gondericiHesap.musteriNo)
                    {
                        ViewBag.TransferMesaj = "Lütfen Kendi Hesabınıza Para Göndermek İçin Virman Seçeneğini Kullanınız!";
                        return View();
                    }
                    else
                    {
                        transfer.turu = "Havale";
                    }
                    gondericiHesap.bakiye = gondericiHesap.bakiye - transfer.tutar;
                    aliciHesap.bakiye = aliciHesap.bakiye + transfer.tutar;
                    transfer.islemTarihi = DateTime.Now;
                    transfer.gondericiHesapNo = gondericiHesap.hesapNo;
                    transfer.platform = "Web";

                    db.havale_virman.Add(transfer);
                    db.SaveChanges();
                    return RedirectToAction("Hesaplarim", "Transfer");
                }
                catch (Exception e)
                {
                    ViewBag.TransferMesaj = "Hata";
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult Virman(string id)
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
                    return RedirectToAction("Hesaplarim", "Transfer");
                }
                ViewBag.VirmanBakiye = hesap.bakiye;
                var hesaplar = db.hesap.Where(x => x.musteriNo == musteriNo && x.aktiflikDurumu == true && x.hesapNo != id).ToList();
                ViewBag.VirmanHesaplar = hesaplar;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Virman(string id, havale_virman transfer)
        {
            var kullaniciadi = User.Identity.Name.Split(' ');
            long musteriNo = Int32.Parse(kullaniciadi[3]);
            var hesaplar = db.hesap.Where(x => x.musteriNo == musteriNo && x.aktiflikDurumu == true && x.hesapNo != id).ToList();
            ViewBag.VirmanHesaplar = hesaplar;
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var gondericiHesap = db.hesap.FirstOrDefault(x => x.musteriNo == musteriNo && x.hesapNo == id && x.aktiflikDurumu == true);
                var aliciHesap = db.hesap.FirstOrDefault(x => x.hesapNo == transfer.aliciHesapNo && x.aktiflikDurumu == true);
                if (gondericiHesap == null || aliciHesap == null)
                {
                    ViewBag.VirmanMesaj = "Böyle Bir Hesap Bulunamamıştır Lütfen Kontrol Edip Tekrar Deneyiniz";
                    return View();
                }
                if (gondericiHesap.hesapNo == aliciHesap.hesapNo)
                {
                    ViewBag.VirmanMesaj = "Aynı Hesaba Para Gönderemezsiniz";
                    return View();
                }
                if (transfer.tutar <= 0)
                {
                    ViewBag.VirmanMesaj = "Lütfen Geçerli Tutar Giriniz";
                    return View();
                }
                if (gondericiHesap.bakiye < transfer.tutar)
                {
                    ViewBag.VirmanMesaj = "Yetersiz Bakiye";
                    return View();
                }
                try
                {
                    if (aliciHesap.musteriNo == gondericiHesap.musteriNo)
                    {
                        transfer.turu = "Virman";
                    }
                    else
                    {
                        ViewBag.VirmanMesaj = "Lütfen Başka Birinin Hesabına Para Göndermek İçin Havale Seçeneğini Kullanınız!";
                        return View();
                    }
                    gondericiHesap.bakiye = gondericiHesap.bakiye - transfer.tutar;
                    aliciHesap.bakiye = aliciHesap.bakiye + transfer.tutar;
                    transfer.islemTarihi = DateTime.Now;
                    transfer.gondericiHesapNo = gondericiHesap.hesapNo;
                    transfer.platform = "Web";

                    db.havale_virman.Add(transfer);
                    db.SaveChanges();
                    return RedirectToAction("Hesaplarim", "Transfer");
                }
                catch (Exception e)
                {
                    ViewBag.VirmanMesaj = "Hata";
                    return View();
                }
            }
        }
    }
}