using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutunamayanlar.Models.EntityFramework;

namespace Tutunamayanlar.Controllers
{
    [Authorize]
    public class HGSController : Controller
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();

        // GET: HGS
        public ActionResult HGSlerim()
        {
            var kullaniciadi = User.Identity.Name.Split(' ');
            long musteriNo = Int32.Parse(kullaniciadi[3]);
            ViewBag.hgs = db.HGS_Banka.Where(x => x.HGSMusteriNumarasi == musteriNo).ToList();
            
            return View();
        }

        [HttpGet]
        public ActionResult YeniHGS()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniHGS(HGS_Kurum hgs)
        {
            if (hgs.hgsBakiye < 25)
            {
                ViewBag.YeniHGSMesaj = "Lütfen Geçerli Bir Tutar Giriniz!";
                return View();
            }

            try
            {
                var kullaniciadi = User.Identity.Name.Split(' ');
                long musteriNo = Int32.Parse(kullaniciadi[3]);
                long bakiye = Int32.Parse(hgs.hgsBakiye.ToString());

                HGS_WebService.HGSSoapClient servis = new HGS_WebService.HGSSoapClient();
                var sonuc = servis.satinAl(musteriNo, bakiye);

                if(sonuc == null)
                {
                    ViewBag.YeniHGSMesaj = "İşleminiz Gerçekleşmemiştir Lütfen Daha Sonra Tekrar Deneyiniz!";
                    return View();
                }
                else
                {
                    ViewBag.YeniHGSMesaj = sonuc;
                    if(!sonuc.Equals("HGS Kurumundaki Bir Hatadan Dolayı Şu Anda İşleminizi Gerçekleştiremiyoruz"))
                    {
                        var hesap = db.HGS_Banka.Where(x => x.HGSMusteriNumarasi == musteriNo).ToList();
                        HGS_Banka hgsBanka = new HGS_Banka();
                        hgsBanka.HGSMusteriNumarasi = musteriNo;
                        hgsBanka.hgsBakiyesi = bakiye - 25;
                        hgsBanka.HGSNumarası = musteriNo+""+(hesap.Count + 1).ToString();

                        db.HGS_Banka.Add(hgsBanka);
                        db.SaveChanges();
                    }
                    return View();
                }
            }
            catch(Exception e)
            {
                ViewBag.YeniHGSMesaj = "Hata";
                return View();
            }
        }

        [HttpGet]
        public ActionResult HGSBakiyeYukle(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var kullaniciadi = User.Identity.Name.Split(' ');
                long musteriNo = Int32.Parse(kullaniciadi[3]);
                var hgs = db.HGS_Banka.FirstOrDefault(x => x.HGSMusteriNumarasi == musteriNo && x.HGSNumarası == id);
                if (hgs == null)
                {
                    return RedirectToAction("HGSlerim", "HGS");
                }
                ViewBag.HGSBakiyeYukleBakiye = hgs.hgsBakiyesi;
                hgs.hgsBakiyesi = null;
                return View();
            }
        }

        [HttpPost]
        public ActionResult HGSBakiyeYukle(string id, HGS_Kurum hgs_kurum)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                if(hgs_kurum.hgsBakiye <= 0)
                {
                    ViewBag.HGSBakiyeYukleMesaj = "Lütfen Geçerli Bir Tutar giriniz";
                    return View();
                }

                try
                {
                    var kullaniciadi = User.Identity.Name.Split(' ');
                    long musteriNo = Int32.Parse(kullaniciadi[3]);
                    long bakiye = Int32.Parse(hgs_kurum.hgsBakiye.ToString());
                    HGS_WebService.HGSSoapClient servis = new HGS_WebService.HGSSoapClient();
                    var sonuc = servis.bakiyeYukle(id, bakiye);

                    if (sonuc == null)
                    {
                        ViewBag.HGSBakiyeYukleMesaj = "İşleminiz Gerçekleşmemiştir Lütfen Daha Sonra Tekrar Deneyiniz!";
                        return View();
                    }
                    else
                    {
                        ViewBag.HGSBakiyeYukleMesaj = sonuc;
                        if (!sonuc.Equals("Hesap Yok"))
                        {
                            var hesap = db.HGS_Banka.FirstOrDefault(x => x.HGSNumarası == id && x.HGSMusteriNumarasi == musteriNo);
                            hesap.hgsBakiyesi += bakiye;
                            db.SaveChanges();
                        }
                        return View();
                    }

                }
                catch(Exception e)
                {
                    ViewBag.HGSBakiyeYukleMesaj = "Hata!";
                    return View();
                }
            }
        }
    }
}