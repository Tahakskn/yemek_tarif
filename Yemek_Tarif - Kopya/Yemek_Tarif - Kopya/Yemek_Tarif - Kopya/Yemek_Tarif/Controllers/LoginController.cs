using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Yemek_Tarif.Models;

namespace Yemek_Tarif.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string Pass)
        {
           
            YemekTarifEntities ent = new YemekTarifEntities();
            //Linq sorgusu
            List<tbl_kullanicilar> kulListesi = ent.tbl_kullanicilar.Where(i => i.KullaniciAdi == username && i.Sifre == Pass).ToList();
            //Lambda Notation
            int donenKaySay = kulListesi.Count();
            if (donenKaySay == 1)
            {
                return RedirectToAction("Admin", "Sabitler");
            }
            else if (donenKaySay == 0)
            {
                ViewBag.hata = "Lütfen Bilgilerinizi Kontrol Ediniz...";
                return View();
            }

            else return View();

        }


    }
}