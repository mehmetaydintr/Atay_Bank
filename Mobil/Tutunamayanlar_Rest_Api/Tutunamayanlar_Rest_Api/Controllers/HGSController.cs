using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Tutunamayanlar_Rest_Api.Controllers
{
    [RoutePrefix("api/tutunamayanlar/hgs")]
    public class HGSController : ApiController
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();

        [Route("yenihgs")]
        [HttpPost]
        public HttpResponseMessage satinAl(HGS_Banka hgsBanka)
        {
            if(hgsBanka.hgsBakiyesi > 25)
            {
                if (hgsBanka.HGSMusteriNumarasi != null)
                {
                    decimal ücret = 25;
                    HGS_Banka hgs = new HGS_Banka();
                    try
                    {
                        hgsBanka.hgsBakiyesi -= ücret;
                        hgs.hgsBakiyesi = hgsBanka.hgsBakiyesi;
                        hgs.HGSMusteriNumarasi = hgsBanka.HGSMusteriNumarasi;
                        String hgsNo = hgsBanka.HGSMusteriNumarasi.ToString();
                        hgsNo += (hgslerim(hgsBanka.HGSMusteriNumarasi) + 1).ToString();
                        hgs.HGSNumarası = hgsNo;
                        db.HGS_Banka.Add(hgs);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "HGS Başvurunuz Başarılı Bir Şekilde Gerçekleşmiştir.\nHGS Bakiyeniz : " + hgs.hgsBakiyesi.ToString() + "₺");
                    }
                    catch (Exception e)
                    {
                        return Request.CreateResponse(HttpStatusCode.Accepted, "HGS Kurumundaki Bir Hatadan Dolayı Şu Anda İşleminizi Gerçekleştiremiyoruz");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Kullanıcı bulunamadı!");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Geçerli Bir Tutar Giriniz");
            }
        }

        [Route("paraYukle")]
        [HttpPut]
        public HttpResponseMessage bakiyeYukle(HGS_Banka hgsBanka)
        {
            if (hgsBanka.hgsBakiyesi > 0)
            {
                if (hgsBanka.HGSNumarası != null)
                {
                    try
                    {
                        var hgs = db.HGS_Banka.FirstOrDefault(x => x.HGSNumarası == hgsBanka.HGSNumarası);

                        if (hgs == null)
                        {
                            return Request.CreateResponse(HttpStatusCode.Accepted, "Hesap Bulunamadı");
                        }
                        hgs.hgsBakiyesi += hgsBanka.hgsBakiyesi;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "İşleminiz Başarıyla Gerçekleşmiştir.Yeni Bakiyeniz : " + hgs.hgsBakiyesi.ToString() + "₺");
                    }
                    catch (Exception e)
                    {
                        return Request.CreateResponse(HttpStatusCode.Accepted, "Kurum tarafında beklenmedik bir hata oluştu!");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Geçersiz HGS Numarası");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Geçerli Bir Tutar Giriniz!");
            }

        }

        [Route("hgsler")]
        [HttpPost]
        public HttpResponseMessage hgsler(HGS_Banka hgsBanka)
        {
            if(hgsBanka.HGSMusteriNumarasi != null)
            {
                var hgsler = db.HGS_Banka.Where(x => x.HGSMusteriNumarasi == hgsBanka.HGSMusteriNumarasi).ToList();
                if (hgsler.Count() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, hgsler);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "HGS'niz bulunmamaktadır");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Kullanıcı bulunumadı!");
            }
        }

        public int hgslerim(long? musteriNo)
        {
            try
            {
                var hgs = db.HGS_Kurum.Where(x => x.musteriNumarasi == musteriNo).ToList();
                return hgs.Count;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
    }
}
