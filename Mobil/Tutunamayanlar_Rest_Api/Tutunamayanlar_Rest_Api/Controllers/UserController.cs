using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tutunamayanlar_Rest_Api.Models;

namespace Tutunamayanlar_Rest_Api.Controllers
{
    [RoutePrefix("api/tutunamayanlar/user")]
    public class UserController : ApiController
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();

        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginDTO kullanici2)
        {
            if(kullanici2.TCKN != null && kullanici2.sifre != null)
            {
                try
                {
                  
                    //UInt32.Parse(TCKN);
                    kullanici kullanici = db.kullanici.FirstOrDefault(x => x.TCKN == kullanici2.TCKN && x.sifre == kullanici2.sifre);

                    if (kullanici != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, kullanici);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Accepted, "TCKN veya Şifre Hatalı");
                    }

                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "TCKN sadece rakamlardan oluşmaldır.");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Eksik Bilgi Girdiniz");
            }
        }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(kullanici kullanici)
        {
            if (kullanici.ad != null && kullanici.adres != null && kullanici.email != null && kullanici.sifre != null && kullanici.soyad != null && kullanici.TCKN != null && kullanici.telefon != null)
            {
                try
                {
                    kullanici tempkullanici = db.kullanici.FirstOrDefault(x => x.TCKN == kullanici.TCKN);

                    if (tempkullanici != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.Accepted, "Bu TCKN kayıtlıdır");
                    }
                    else
                    {
                        kullanici temp2 = db.kullanici.FirstOrDefault(x => x.email == kullanici.email);
                        if (temp2 == null)
                        {
                            var temp = db.kullanici.FirstOrDefault(x => x.TCKN == kullanici.TCKN || x.email == kullanici.email);
                            if (temp != null)
                            {
                                return Request.CreateResponse(HttpStatusCode.Accepted, "Bu bilgilerde başka bir kullanıcı kayıtlıdır!");
                            }
                            else
                            {
                                db.kullanici.Add(kullanici);

                                try
                                {
                                    db.SaveChanges();
                                    var user = db.kullanici.FirstOrDefault(x => x.TCKN == kullanici.TCKN && x.sifre == kullanici.sifre);
                                    return Request.CreateResponse(HttpStatusCode.OK, user);
                                }
                                catch (Exception e)
                                {

                                }
                                return Request.CreateResponse(HttpStatusCode.Accepted, "Bilgileriniz Hatalıdır Lütfen Kontrol Edip Tekrar Deneyiniz!");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.Accepted, "Bu E-Mail kayıtlıdır.");
                        }
                    }

                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "TCKN sadece rakamlardan oluşmaldır.");
                }
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "Eksik Bilgi Girdiniz");

        }

        [Route("bilgiGuncelle")]
        [HttpPut]
        public HttpResponseMessage BilgiGuncelle(kullanici kullanici)
        {
            if(kullanici.ad != null && kullanici.adres != null && kullanici.email != null && kullanici.sifre != null && kullanici.soyad != null && kullanici.telefon!= null)
            {
                kullanici temp2 = db.kullanici.FirstOrDefault(x => x.email == kullanici.email && x.musteriNo != kullanici.musteriNo);
                if (temp2 == null)
                {
                    kullanici temp = db.kullanici.FirstOrDefault(x => x.musteriNo == kullanici.musteriNo);
                    if (temp != null)
                    {
                        temp.ad = kullanici.ad;
                        temp.adres = kullanici.adres;
                        temp.email = kullanici.email;
                        temp.sifre = kullanici.sifre;
                        temp.soyad = kullanici.soyad;
                        temp.telefon = kullanici.telefon;
                        try
                        {
                            db.SaveChanges();
                            var user = db.kullanici.FirstOrDefault(x => x.musteriNo == kullanici.musteriNo);
                            return Request.CreateResponse(HttpStatusCode.OK, user);
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
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Bu E-Mail kayıtlıdır.");
            }

            return Request.CreateResponse(HttpStatusCode.Accepted, "Eksik Bilgi Girdiniz");

        }
    }
}
