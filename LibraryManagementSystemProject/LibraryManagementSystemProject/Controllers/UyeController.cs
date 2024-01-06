using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystemProject.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace LibraryManagementSystemProject.Controllers
{
    public class UyeController : Controller
    {
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        // GET: Uye
        public ActionResult Index(int page = 1)
        { 
            var degerler = db.TBLUYELER.ToList().ToPagedList(page, 4);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(TBLUYELER p)
        {
            if (!ModelState.IsValid)   //data annotations'a bağlı olan geçerliliği sağlamazsa üyeekle'ye dön
            {
                return View("UyeEkle");
            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return RedirectToAction("UyeEkle");
        }

        public ActionResult UyeSil(int id)
        {
            var uye = db.TBLUYELER.Find(id);
            db.TBLUYELER.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeGetir(int id)
        {
            var uye = db.TBLUYELER.Find(id); 
            return View("UyeGetir", uye); 
        }
        public ActionResult UyeGuncelle(TBLUYELER p1)
        {
            var uye = db.TBLUYELER.Find(p1.ID);
            uye.AD = p1.AD;
            uye.SOYAD = p1.SOYAD;
            uye.MAIL = p1.MAIL;
            uye.KULLANICIADI = p1.KULLANICIADI;
            uye.SİFRE = p1.SİFRE;
            uye.OKUL = p1.OKUL;
            uye.FOTOGRAF = p1.FOTOGRAF;
            uye.TELEFON = p1.TELEFON;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeKitapGecmis(int id)
        {
            var kitapgcms = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            var uyektp = db.TBLUYELER.Where(x => x.ID == id).Select(y => y.AD + " " + y.SOYAD).FirstOrDefault();  //tbluyeler içerisinde benim gönderdiğim id'ye eşit olanlar arasından üyenin adını ve soyadınını ilk değerini seç, uyektp isimli değişkene ata
            ViewBag.u1 = uyektp;
            return View(kitapgcms);
        }
    }
}