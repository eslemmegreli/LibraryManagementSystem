using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystemProject.Models.Entity;



namespace LibraryManagementSystemProject.Controllers
{
    public class OduncController : Controller
    {
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        // GET: Odunc
        [Authorize(Roles ="A")]                                                //sadece rolü A olan adminler bu sayfaya erişebilir
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == false).ToList(); //işlemdurumu false olanları listele
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from i in db.TBLUYELER.ToList()             /* Listeden değer 1 isimli bir öğe seçer ve tbl üyelerden i değeri gelir*/
                                           select new SelectListItem                   /* şu öğeyi seç*/
                                           {
                                               Text = i.AD +" "+ i.SOYAD,                            /* AD'ı texte, ID ise değere ata YANİ; arka planda id değerini çalıştır önde ise ad değerini göster*/
                                               Value = i.ID.ToString()
                                           }).ToList();
        

            List<SelectListItem> deger2 = (from i in db.TBLKITAP.Where(x=>x.DURUM==true).ToList()   /* Listeden değer 2 isimli bir öğe seçer ve tbl kitaptan durumu true olanları listeler */
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()                 /* AD'ı texte, ID ise değere ata YANİ; arka planda id değerini çalıştır önde ise ad değerini göster*/
                                           }).ToList();


            List<SelectListItem> deger3 = (from i in db.TBLPERSONEL.ToList()              /* Listeden değer 2 isimli bir öğe seçer ve tbl kitaptan i değeri gelir*/
                                           select new SelectListItem
                                           {
                                               Text = i.PERSONEL,
                                               Value = i.ID.ToString()                 /* AD'ı texte, ID ise değere ata YANİ; arka planda id değerini çalıştır önde ise ad değerini göster*/
                                           }).ToList();

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET p)
        {
            var d1 = db.TBLUYELER.Where(x => x.ID == p.TBLUYELER.ID).FirstOrDefault();
            var d2 = db.TBLKITAP.Where(x => x.ID == p.TBLKITAP.ID).FirstOrDefault();
            var d3 = db.TBLPERSONEL.Where(x => x.ID == p.TBLPERSONEL.ID).FirstOrDefault();
           
            p.TBLUYELER = d1;
            p.TBLKITAP = d2;
            p.TBLPERSONEL = d3;
            db.TBLHAREKET.Add(p);
            //var ktp = db.TBLKITAP.Where(x => x.ID == p.TBLKITAP.ID).FirstOrDefault();
            //if (ktp != null)
            //{
            //    ktp.DURUM= false;
            //}

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OduncIade(TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;
            return View("OduncIade", odn);
            }
        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);    //p parametresininin id değerini tblhareketten çek onu da hrk ata
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;   // p deki uyegetirtarih hrk ata
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");      //indexe dön
        }
    }
}