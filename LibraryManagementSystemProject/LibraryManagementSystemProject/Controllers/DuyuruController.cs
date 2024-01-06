using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystemProject.Models.Entity;

namespace LibraryManagementSystemProject.Controllers
{
    public class DuyuruController : Controller
    {
        // GET: Duyuru
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLDUYURULAR.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDuyuru(TBLDUYURULAR t)
        {
            db.TBLDUYURULAR.Add(t);   //t parametresinde gelen yeni duyuruyu tblduyurulara ekle
            db.SaveChanges();
            return RedirectToAction("Index");   //indexe dön
        }
        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TBLDUYURULAR.Find(id);  //id değerine sahip duyuruyu tablodan bul ve duyuru değişkenine ata
            db.TBLDUYURULAR.Remove(duyuru);  //o duyuruyu tablodan sil
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruDetay(TBLDUYURULAR d)
        {
            var duyuru = db.TBLDUYURULAR.Find(d.ID);  //duyurular tablosunda t.id değerine sahip duyuruyuyu bul ve bu duyuruyu 'duyuru' değişkenine ata
            return View("DuyuruDetay", duyuru);
        }
        public ActionResult DuyuruGuncelle(TBLDUYURULAR t)
        {
            var duyuru = db.TBLDUYURULAR.Find(t.ID); 
            duyuru.KATEGORI = t.KATEGORI;  
            duyuru.ICERIK = t.ICERIK;
            duyuru.TARIH = t.TARIH;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}