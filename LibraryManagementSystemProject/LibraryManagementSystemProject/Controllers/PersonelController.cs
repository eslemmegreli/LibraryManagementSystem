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
    public class PersonelController : Controller
    {
        // GET: Personel
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        public ActionResult Index(int page=1)
        {
            var degerler = db.TBLPERSONEL.ToList().ToPagedList(page, 5); //beşer beşer sayfala
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(TBLPERSONEL p)
        {
            if (!ModelState.IsValid)   //data annotations'a bağlı olan geçerliliği sağlamazsa personelekle'ye dön
            {
                return View("PersonelEkle");
            }
            db.TBLPERSONEL.Add(p);     //p ekle personel tablosuna
            db.SaveChanges();
            return RedirectToAction("PersonelEkle");
        }
        public ActionResult PersonelSil(int id)
        {
            var prsn = db.TBLPERSONEL.Find(id); //id'e göre tblpersonelden bul onu da prsn ata
            db.TBLPERSONEL.Remove(prsn);        //atadığın prsn değerini tablodan sil
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var prsn = db.TBLPERSONEL.Find(id);  //id'e göre tblpersonelden bul onu da prsn ata
            return View("PersonelGetir", prsn);
        }
        public ActionResult PersonelGuncelle(TBLPERSONEL p)
        {
            var prsn = db.TBLPERSONEL.Find(p.ID);  //p parametresininin id değerini tblpersonelden çek onu da prsn ata
            prsn.PERSONEL = p.PERSONEL;            // p deki personel adını prsn ata
            db.SaveChanges();                      //değişiklikleri kaydet
            return RedirectToAction("Index");      //indexe dön
        }
    }
}
