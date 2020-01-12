using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for HGS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class HGS : System.Web.Services.WebService
{

    public HGS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string satinAl(long musteriNo, decimal bakiye)
    {
        decimal ücret = 25;
        YazilimBakimiEntities db = new YazilimBakimiEntities();
        HGS_Kurum hgs = new HGS_Kurum();
        try
        {
            bakiye -= ücret;
            hgs.hgsBakiye = bakiye;
            hgs.musteriNumarasi = musteriNo;
            String hgsNo = musteriNo.ToString();
            hgsNo += (hgslerim(musteriNo)+1).ToString();
            hgs.HGSNO = hgsNo;
            db.HGS_Kurum.Add(hgs);
            db.SaveChanges();
            return "HGS Başvurunuz Başarılı Bir Şekilde Gerçekleşmiştir.\nHGS Bakiyeniz : "+hgs.hgsBakiye.ToString()+"₺";
        }
        catch (Exception e)
        {
            return "HGS Kurumundaki Bir Hatadan Dolayı Şu Anda İşleminizi Gerçekleştiremiyoruz";
        }
        
    }

    [WebMethod]
    public string bakiyeSorgula(string hgsNo)
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();
        try
        {
            var hgs = db.HGS_Kurum.FirstOrDefault(x => x.HGSNO == hgsNo);

            if (hgs == null)
            {
                return "Hesap Yok";
            }

            return hgs.hgsBakiye.ToString();
        }
        catch (Exception e)
        {
            return "Hata!";
        }

    }

    [WebMethod]
    public string bakiyeYukle(string hgsNo, decimal tutar)
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();
        try
        {
            var hgs = db.HGS_Kurum.FirstOrDefault(x => x.HGSNO == hgsNo);

            if (hgs == null)
            {
                return "Hesap Yok";
            }
            hgs.hgsBakiye += tutar;
            db.SaveChanges();
            return "İşleminiz Başarıyla Gerçekleşmiştir.\nYeni Bakiyeniz : " +hgs.hgsBakiye.ToString()+"₺";
        }
        catch (Exception e)
        {
            return null;
        }

    }

    [WebMethod]
    public int hgslerim(long musteriNo)
    {
        YazilimBakimiEntities db = new YazilimBakimiEntities();
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
