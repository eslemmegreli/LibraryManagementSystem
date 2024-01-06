using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystemProject.Models.Entity;

namespace LibraryManagementSystemProject.Controllers
{
    public class AyarlarController : Controller
    {
        // GET: Ayarlar
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        //public ActionResult Index()
        //{
        //    var kullanicilar = db.TBLADMIN.ToList(); //admindeki verileri listele
        //    return View(kullanicilar);
        //}
        public ActionResult Index2()
        {
            var kullanicilar = db.TBLADMIN.ToList(); //admindeki verileri listele
            return View(kullanicilar);
        }
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(TBLADMIN p)
        {
            db.TBLADMIN.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
        public ActionResult AdminSil(int id)
        {
            var adm = db.TBLADMIN.Find(id);
            db.TBLADMIN.Remove(adm);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
        [HttpGet]
        public ActionResult AdminGuncelle(int id)
        {
            var adm = db.TBLADMIN.Find(id);
            return View("AdminGuncelle",adm);     //güncelleme sayfasında verileri getirir
        }
        [HttpPost]
        public ActionResult AdminGuncelle(TBLADMIN p)
        {
            var adm = db.TBLADMIN.Find(p.ID);
            adm.Kullanici = p.Kullanici;
            adm.Sifre = p.Sifre;
            adm.Yetki = p.Yetki;
            db.SaveChanges();
            return RedirectToAction("Index2");
        }

    }
}