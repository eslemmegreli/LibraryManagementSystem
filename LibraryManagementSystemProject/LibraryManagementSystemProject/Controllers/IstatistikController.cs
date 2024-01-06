using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.IO;
using LibraryManagementSystemProject.Models.Entity;

namespace LibraryManagementSystemProject.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        public ActionResult Index()
        {
            var deger1 = db.TBLUYELER.Count(); //tblüyeleri say
            var deger2 = db.TBLKITAP.Count();  //kitap sayısını say
            var deger3 = db.TBLKITAP.Where(x => x.DURUM == false).Count();  //tblkitaptaki durumu false olanları yani aslında ödünç verilen kitapları say.
            var deger4 = db.TBLCEZA.Sum(x => x.PARA);  //tblcezadaki para sütunumun değerlerini topla.
            ViewBag.dgr1 = deger1;  //üye sayısını viewbagın dgr1'ine at
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            return View();
        }
        public ActionResult Hava()
        {
            CultureInfo trCulture = new CultureInfo("tr-TR");  // Tr kültürünü berlirledim
            DateTime suAnkiTarih = DateTime.Now;  //şuanki tarihi al
            DateTime yarin = suAnkiTarih.AddDays(1); //bir sonraki günü al
            DateTime sonrakiGun = suAnkiTarih.AddDays(2); //iki gün sonrayı al

            string gunAdi = suAnkiTarih.ToString("dddd", trCulture); //O anki günü, Türkçe gün adıyla al
            string yarinGunAdi = yarin.ToString("dddd", trCulture); //Bir sonraki günü, Türkçe gün adını al
            string sonrakiGunAdi = sonrakiGun.ToString("dddd", trCulture); // Yarından sonraki günü, Türkçe gün adıyla al

            ViewBag.GunAdi = gunAdi;  //ViewBag üzerinden View'e veriyi ilet
            ViewBag.YarinGunAdi = yarinGunAdi;
            ViewBag.sonrakiGunAdi = sonrakiGunAdi;
            return View();
        }
        public ActionResult HavaKart()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult resimyukle(HttpPostedFileBase dosya)  //dosya parametre isimli dosya göndermek için
        {
            if (dosya.ContentLength > 0)   //eğer dosyanın boyutu 0'dan büyükse yani bir dosya seçildiğinde
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/galeri/"), Path.GetFileName(dosya.FileName)); //sunucu içerisinde verdiğim adrese kaydet, bu yoldan gelen dosya isimlerini al.
                dosya.SaveAs(dosyayolu); //dosyayolu'ndan gelen dosyayı farklı kaydet
            }
            return RedirectToAction("Galeri");
        }
        public ActionResult LinqKart()
        {
            var deger1 = db.TBLKITAP.Count();  //tblkitap içindeki değerleri say
            var deger2 = db.TBLUYELER.Count(); //tblüye tablosu içindeki değerleri say
            var deger3 = db.TBLCEZA.Sum(x => x.PARA); //tblceza tablosu içindeki para değerlerini topla
            var deger4 = db.TBLKITAP.Where(x => x.DURUM == false).Count(); //tblkitap içinde durumu false olanları say
            var deger5 = db.TBLKATEGORİ.Count();

            var deger8 = db.FazlaKitapYazar().FirstOrDefault();
            var deger9 = db.TBLKITAP.GroupBy(x => x.YAYINEVI).OrderByDescending(z => z.Count()).Select(y => new { y.Key }).FirstOrDefault();  // tblkitap içerisinde, yayınevini al bunu azalan sıraya göre grupla, bunun içerisinden keyin (sütunun) ilk değerini getir.
            var deger11 = db.tblILETISIM.Count();

            ViewBag.dgr1 = deger1; //deger1'den gelen degeri al
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            ViewBag.dgr5 = deger5;
            ViewBag.dgr8 = deger8;
            ViewBag.dgr9 = deger9;
            ViewBag.dgr11 = deger11;
            return View();
        }
    }
}