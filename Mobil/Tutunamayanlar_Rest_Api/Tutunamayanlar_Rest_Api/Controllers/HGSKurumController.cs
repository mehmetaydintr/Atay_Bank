using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Tutunamayanlar_Rest_Api.Controllers
{
    [RoutePrefix("api/tutunamayanlar/hgskurum")]
    public class HGSKurumController : ApiController
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();

        [Route("yenihgs")]
        [HttpPost]
        public HttpResponseMessage satinAl(HGS_Kurum hgsKurum)
        {
            if (hgsKurum.hgsBakiye > 25)
            {
                if (hgsKurum.musteriNumarasi != null)
                {
                    decimal ücret = 25;
                    HGS_Kurum hgs = new HGS_Kurum();
                    try
                    {
                        hgsKurum.hgsBakiye -= ücret;
                        hgs.hgsBakiye = hgsKurum.hgsBakiye;
                        hgs.musteriNumarasi = hgsKurum.musteriNumarasi;
                        String hgsNo = hgsKurum.musteriNumarasi.ToString();
                        hgsNo += (hgslerim(hgsKurum.musteriNumarasi) + 1).ToString();
                        hgs.HGSNO = hgsNo;
                        db.HGS_Kurum.Add(hgs);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, hgs);
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
                return Request.CreateResponse(HttpStatusCode.Accepted, "Geçerli Bir Tutar Giriniz!");
            }
        }

        [Route("paraYukle")]
        [HttpPut]
        public HttpResponseMessage bakiyeYukle(HGS_Kurum hgsKurum)
        {
            if (hgsKurum.hgsBakiye > 0)
            {
                if (hgsKurum.HGSNO != null)
                {
                    try
                    {
                        var hgs = db.HGS_Kurum.FirstOrDefault(x => x.HGSNO == hgsKurum.HGSNO);

                        if (hgs == null)
                        {
                            return Request.CreateResponse(HttpStatusCode.Accepted, "Hesap Bulunamadı");
                        }
                        hgs.hgsBakiye += hgsKurum.hgsBakiye;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, hgs);
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
