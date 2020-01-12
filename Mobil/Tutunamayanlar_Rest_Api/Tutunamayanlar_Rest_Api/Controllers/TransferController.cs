using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Tutunamayanlar_Rest_Api.Controllers
{
    [RoutePrefix("api/tutunamayanlar/transfer")]
    public class TransferController : ApiController
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();

        [Route("havale")]
        [HttpPost]
        public HttpResponseMessage Havale(havale_virman havale)
        {
            var gondericiHesap = db.hesap.FirstOrDefault(x => x.hesapNo == havale.gondericiHesapNo && x.aktiflikDurumu == true);
            var aliciHesap = db.hesap.FirstOrDefault(x => x.hesapNo == havale.aliciHesapNo && x.aktiflikDurumu == true);
            if (gondericiHesap == null || aliciHesap == null)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Böyle Bir Hesap Bulunamamıştır Lütfen Kontrol Edip Tekrar Deneyiniz");
            }
            if (gondericiHesap.hesapNo == aliciHesap.hesapNo)
            {
               return Request.CreateResponse(HttpStatusCode.Accepted, "Aynı Hesaba Para Gönderemezsiniz");
            }
            if (havale.tutar <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Geçerli Tutar Giriniz"); ;
            }
            if (gondericiHesap.bakiye < havale.tutar)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Yetersiz Bakiye");
            }
            try
            {
                if (aliciHesap.musteriNo == gondericiHesap.musteriNo)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Kendi Hesabınıza Para Göndermek İçin Virman Seçeneğini Kullanınız!");
                }
                else
                {
                    havale.turu = "Havale";
                }
                gondericiHesap.bakiye = gondericiHesap.bakiye - havale.tutar;
                aliciHesap.bakiye = aliciHesap.bakiye + havale.tutar;
                havale.islemTarihi = DateTime.Now;
                havale.gondericiHesapNo = gondericiHesap.hesapNo;
                havale.platform = "Mobil";

                db.havale_virman.Add(havale);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, gondericiHesap.bakiye);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Hata");
            }
        }

        [Route("virman/hesaplar")]
        [HttpPost]
        public HttpResponseMessage VirmanHesaplar(hesap account)
        {
            var hesap = db.hesap.FirstOrDefault(x => x.musteriNo == account.musteriNo && x.hesapNo == account.hesapNo && x.aktiflikDurumu == true);
            if (hesap == null)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Böyle Bir Hesap Bulunamamıştır Lütfen Kontrol Edip Tekrar Deneyiniz");
            }
            var hesaplar = db.hesap.Where(x => x.musteriNo == account.musteriNo && x.aktiflikDurumu == true).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, hesaplar);
        }

        [Route("virman")]
        [HttpPost]
        public HttpResponseMessage Virman(havale_virman virman)
        {
            if (virman.gondericiHesapNo == null)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Böyle Bir Hesap Bulunamamıştır Lütfen Kontrol Edip Tekrar Deneyiniz");
            }
            else
            {
                var gondericiHesap = db.hesap.FirstOrDefault(x => x.hesapNo == virman.gondericiHesapNo && x.aktiflikDurumu == true);
                var aliciHesap = db.hesap.FirstOrDefault(x => x.hesapNo == virman.aliciHesapNo && x.aktiflikDurumu == true);
                if (gondericiHesap == null || aliciHesap == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Böyle Bir Hesap Bulunamamıştır Lütfen Kontrol Edip Tekrar Deneyiniz");
                }
                if (gondericiHesap.hesapNo == aliciHesap.hesapNo)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Aynı Hesaba Para Gönderemezsiniz");
                }
                if (virman.tutar <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Geçerli Tutar Giriniz");
                }
                if (gondericiHesap.bakiye < virman.tutar)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Yetersiz Bakiye");
                }
                try
                {
                    if (aliciHesap.musteriNo == gondericiHesap.musteriNo)
                    {
                        virman.turu = "Virman";
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Başka Birinin Hesabına Para Göndermek İçin Havale Seçeneğini Kullanınız!");
                    }
                    gondericiHesap.bakiye = gondericiHesap.bakiye - virman.tutar;
                    aliciHesap.bakiye = aliciHesap.bakiye + virman.tutar;
                    virman.islemTarihi = DateTime.Now;
                    virman.gondericiHesapNo = gondericiHesap.hesapNo;
                    virman.platform = "Mobil";

                    db.havale_virman.Add(virman);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, gondericiHesap.bakiye);
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Hata");
                }
            }
        }
    }
}
