using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Yemek_Tarif.Models;
namespace Yemek_Tarif.Controllers
{
    public class SabitlerController : Controller
    {
        // GET: Sabitler
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult YemekKategori()
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            var degerler = ent.tbl_Kategoriler.ToList();            
            return View(degerler);

        }
        public ActionResult YemekKategoriDetay(int ?id)
        {
           
            YemekTarifEntities ent = new YemekTarifEntities();
            tbl_Kategoriler kategori = new tbl_Kategoriler();
            var degerler = ent.tbl_Yemekler.Where(x => x.Kategoriid == id).ToList();
           

                return View(degerler);
           

        }
            public ActionResult YemekDetay(int id)
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            List<tbl_Yemekler> yemekler = ent.tbl_Yemekler.Where(x => x.Yemekid == id).ToList();
            return View(yemekler);
        }
        public ActionResult Silme(int id)
        {

            YemekTarifEntities ent = new YemekTarifEntities();
            var yemek = ent.tbl_Yemekler.Find(id);
            ent.tbl_Yemekler.Remove(yemek);
            ent.SaveChanges();
            return RedirectToAction("Admin", "Sabitler");
        }

        public ActionResult Yemekler()
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            List<tbl_Yemekler> yemekListesi = ent.tbl_Yemekler.ToList();
            return View(yemekListesi);
        }

        //Burası Çalışmıyor
        //public ActionResult ResimEkle()
        //{
        //    YemekTarifEntities ent = new YemekTarifEntities();
        //    List<SelectListItem> degerler = (from i in ent.tbl_Kategoriler.ToList()
        //    select new SelectListItem { Text = i.KategoriAd, Value = i.Kategoriid.ToString() }).ToList();
        //    ViewBag.dgr = degerler;
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult ResimEkle(tbl_Yemekler k)
        //{
        //    YemekTarifEntities ent = new YemekTarifEntities();
        //    var ktg = ent.tbl_Kategoriler.Where(m => m.Kategoriid == k.tbl_Kategoriler.Kategoriid).FirstOrDefault();
        //    k.tbl_Kategoriler = ktg;
        //    ent.tbl_Yemekler.Add(k);
        //    ent.SaveChanges();
        //    return View();
        //}
        
        public ActionResult YemekEkle()
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            List<SelectListItem> degerler = (from i in ent.tbl_Kategoriler.ToList()
                                             select new SelectListItem { Text = i.KategoriAd, Value = i.Kategoriid.ToString() }).ToList();
            ViewBag.dgr = degerler;

            return View();
        }
        [HttpPost]
        public ActionResult YemekEkle(tbl_Yemekler y, string YemekAd, string YemekTarif, string YemekResim, string YemekMalzeme)
        {

            YemekTarifEntities ent = new YemekTarifEntities();

            tbl_Tarifler tarifler = new tbl_Tarifler();
            tbl_Kategoriler kategori = new tbl_Kategoriler();

            tbl_Yemekler Yemekler = new tbl_Yemekler();
            var ktg = ent.tbl_Kategoriler.Where(m => m.Kategoriid == y.tbl_Kategoriler.Kategoriid).FirstOrDefault();
            y.tbl_Kategoriler = ktg;
            Yemekler.YemekAd = YemekAd;
            Yemekler.YemekTarif = YemekTarif;
            //yemekler.YemekResim = YemekResim;
            Yemekler.YemekMalzeme = YemekMalzeme;

            ent.tbl_Yemekler.Add(y);

            try
            {
                ent.SaveChanges();
                ViewBag.sonuc = 1;

            }
            catch (Exception ee)
            {

                ViewBag.sonuc = 0;
                ViewBag.mesaj = ee.Message.ToString();
               

            }
            return RedirectToAction("YemekEkle");
            

        }
        public ActionResult Admin()
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            List<tbl_Kategoriler> kat = ent.tbl_Kategoriler.ToList();
            List<tbl_Yemekler> yemekListesi = ent.tbl_Yemekler.ToList();
            return View(yemekListesi);
        }
        public ActionResult YemekGuncelle(string YemekNo)
        {
            YemekTarifEntities ent = new YemekTarifEntities();
            List<SelectListItem> degerler = (from i in ent.tbl_Kategoriler.ToList()
                                             select new SelectListItem { Text = i.KategoriAd, Value = i.Kategoriid.ToString() }).ToList();
            ViewBag.dgr = degerler;
            int yNo = Convert.ToInt32(YemekNo);
            tbl_Yemekler yemekler = ent.tbl_Yemekler.Where(i => i.Yemekid == yNo).FirstOrDefault();
           // return View("YemekGuncelle","Sabitler");
            return View(yemekler);
        }
        [HttpPost]
        public ActionResult YemekGuncelle(tbl_Yemekler y,int ID, string YemekAd, string YemekMalzeme, string YemekTarif)
        {
            YemekTarifEntities ent = new YemekTarifEntities();

           
            tbl_Yemekler yemekler = ent.tbl_Yemekler.Where(i => i.Yemekid == ID).FirstOrDefault();

            tbl_Kategoriler kategori = new tbl_Kategoriler();
            yemekler.YemekAd = YemekAd;
            yemekler.YemekMalzeme = YemekMalzeme;
            yemekler.YemekTarif = YemekTarif;
            var ktg = ent.tbl_Kategoriler.Where(m => m.Kategoriid == y.tbl_Kategoriler.Kategoriid).FirstOrDefault();
            yemekler.Kategoriid = ktg.Kategoriid;
            ent.SaveChanges();
            return RedirectToAction("Admin");
            //return View(yemekler);
        }


    }
}