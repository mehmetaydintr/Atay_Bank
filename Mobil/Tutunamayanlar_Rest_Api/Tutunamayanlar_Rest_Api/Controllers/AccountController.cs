using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tutunamayanlar_Rest_Api.Models;

namespace Tutunamayanlar_Rest_Api.Controllers
{
    [RoutePrefix("api/tutunamayanlar/account")]
    public class AccountController : ApiController
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();

        [Route("hesapAc")]
        [HttpPost]
        public HttpResponseMessage HesapAc(hesap hesap)
        {
            if (hesap.musteriNo != null)
            {
                kullanici user = db.kullanici.FirstOrDefault(x => x.musteriNo == hesap.musteriNo);
                if (user != null)
                {
                    try
                    {
                        int hesapNo = 1000;
                        List<hesap> hesaps = db.hesap.Where(x => x.musteriNo == hesap.musteriNo).ToList();
                        hesapNo = hesapNo + hesaps.Count + 1;
                        hesap.hesapNo = hesap.musteriNo + hesapNo.ToString();
                        hesap.aktiflikDurumu = true;
                        hesap.bakiye = 0;
                        db.hesap.Add(hesap);
                        db.SaveChanges();
                        var hesaplar = db.hesap.Where(x => x.musteriNo == hesap.musteriNo && x.aktiflikDurumu == true).ToList();
                        return Request.CreateResponse(HttpStatusCode.OK, hesaplar);
                    }
                    catch (Exception e)
                    {

                    }
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                }
            }

            return Request.CreateResponse(HttpStatusCode.Accepted, "Eksik Bilgi Girdiniz");

        }

        [Route("hesaplar")]
        [HttpPost]
        public HttpResponseMessage Hesaplar(HesaplarDTO hesap)
        {
            if (hesap.musteriNo != null)
            {
                kullanici user = db.kullanici.FirstOrDefault(x => x.musteriNo == hesap.musteriNo);
                if (user != null)
                {
                    try
                    {
                        var hesaplar = db.hesap.Where(x => x.musteriNo == hesap.musteriNo && x.aktiflikDurumu == true).ToList();
                        if (hesaplar.Count() > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, hesaplar);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.Accepted, "Hesabınız bulunmamaktadır");
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                }
            }

            return Request.CreateResponse(HttpStatusCode.Accepted, "Eksik Bilgi Girdiniz");

        }

        [Route("hesapSil")]
        [HttpPut]
        public HttpResponseMessage HesapSil(hesap hesap)
        {
            if (hesap.musteriNo != null)
            {
                kullanici user = db.kullanici.FirstOrDefault(x => x.musteriNo == hesap.musteriNo);
                if (user != null)
                {
                    try
                    {
                        hesap silinecekHesap = db.hesap.FirstOrDefault(x => x.hesapNo == hesap.hesapNo && x.musteriNo == user.musteriNo && x.aktiflikDurumu == true);
                        if (silinecekHesap != null)
                        {
                            if (silinecekHesap.bakiye == 0)
                            {
                                silinecekHesap.aktiflikDurumu = false;
                                db.SaveChanges();
                                var hesaplar = db.hesap.Where(x => x.musteriNo == hesap.musteriNo && x.aktiflikDurumu == true).ToList();
                                return Request.CreateResponse(HttpStatusCode.OK, hesaplar);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.Accepted, "Hesabınızı silmek için bakiyenizin 0₺ olması gerekmektedir.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Geçerli Bir Hesap Seçiniz!");
                        }

                    }
                    catch (Exception e)
                    {

                    }
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                }
            }

            return Request.CreateResponse(HttpStatusCode.Accepted, "Eksik Bilgi Girdiniz");

        }

        [Route("paraYukle")]
        [HttpPut]
        public HttpResponseMessage ParaYukle(hesap hesap)
        {
            if (hesap.musteriNo != null)
            {
                kullanici user = db.kullanici.FirstOrDefault(x => x.musteriNo == hesap.musteriNo);
                if (user != null)
                {
                    try
                    {
                        hesap guncellecekHesap = db.hesap.FirstOrDefault(x => x.hesapNo == hesap.hesapNo && x.musteriNo == user.musteriNo && x.aktiflikDurumu == true);
                        if (guncellecekHesap != null)
                        {
                            if (hesap.bakiye != null && hesap.bakiye > 0)
                            {
                                guncellecekHesap.bakiye += hesap.bakiye;
                                db.SaveChanges();
                                var hesaplar = db.hesap.Where(x => x.musteriNo == hesap.musteriNo && x.aktiflikDurumu == true).ToList();
                                return Request.CreateResponse(HttpStatusCode.OK, guncellecekHesap.bakiye);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Geçerli Bir Tutar Giriniz!");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Geçerli Bir Hesap Seçiniz!");
                        }

                    }
                    catch (Exception e)
                    {

                    }
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                }
            }

            return Request.CreateResponse(HttpStatusCode.Accepted, "Eksik Bilgi Girdiniz");

        }

        [Route("paraCek")]
        [HttpPut]
        public HttpResponseMessage ParaCek(hesap hesap)
        {
            if (hesap.musteriNo != null)
            {
                kullanici user = db.kullanici.FirstOrDefault(x => x.musteriNo == hesap.musteriNo);
                if (user != null)
                {
                    try
                    {
                        hesap guncellecekHesap = db.hesap.FirstOrDefault(x => x.hesapNo == hesap.hesapNo && x.musteriNo == user.musteriNo && x.aktiflikDurumu == true);
                        if (guncellecekHesap != null)
                        {
                            if (hesap.bakiye != null && hesap.bakiye > 0)
                            {
                                if (hesap.bakiye <= guncellecekHesap.bakiye)
                                {
                                    guncellecekHesap.bakiye -= hesap.bakiye;
                                    db.SaveChanges();
                                    var hesaplar = db.hesap.Where(x => x.musteriNo == hesap.musteriNo && x.aktiflikDurumu == true).ToList();
                                    return Request.CreateResponse(HttpStatusCode.OK, guncellecekHesap.bakiye);
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.Accepted, "Yetersiz Bakiye");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Geçerli Bir Tutar Giriniz!");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.Accepted, "Lütfen Geçerli Bir Hesap Seçiniz!");
                        }

                    }
                    catch (Exception e)
                    {

                    }
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                }
            }

            return Request.CreateResponse(HttpStatusCode.Accepted, "Eksik Bilgi Girdiniz");
        }
    }
}
