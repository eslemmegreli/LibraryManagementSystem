using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystemProject.Models.Entity;
using LibraryManagementSystemProject.Models.Sınıflarım;

namespace LibraryManagementSystemProject.Controllers
{
    [AllowAnonymous]
    public class VitrinController : Controller
    {
        // GET: Vitrin
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();                         // Class1'den cs isimli bir nesne türettim.
            cs.Deger1 = db.TBLKITAP.ToList();                 //tblkitapların listesini deger1 propumun üstüne atadım.
            cs.Deger2 = db.tblHAKKIMIZDA.ToList();            //class1 classı içinde oluşturduğumuz IEnumarable listesine değerleri atadım.
            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(tblILETISIM t)              //vitrin içerisindeki iletişim kısmı için
        {
            db.tblILETISIM.Add(t);                            //iletişim tablosuna ekle
            db.SaveChanges();                                 //değişiklikleri kaydet
            return RedirectToAction("Index");                 //indexe dön
        }
    }
}