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
    public class YazarController : Controller
    {
        // GET: Yazar
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        public ActionResult Index(int page=1)
        {
            var degerler = db.TBLYAZAR.ToList().ToPagedList(page, 10);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YazarEkle(TBLYAZAR y)
        {
            if (!ModelState.IsValid)  //eğerki model üzerinde yapmış olduğum geçerli değilse
            {
                return View("YazarEkle");  //yazarekleme sayfasına geri döndür
            }
            db.TBLYAZAR.Add(y);       //geçerli ise ekleme işlemini yapar bu sayede boş değer girilmemiş olur
            db.SaveChanges();
            return RedirectToAction("YazarEkle");
        }   
        public ActionResult YazarSil(int id)
        {
            var yzr = db.TBLYAZAR.Find(id);
            db.TBLYAZAR.Remove(yzr);
            db.SaveChanges();
            return RedirectToAction("Index"); //kaydettikten sonra index sayfasına yönlendir.
        }
        public ActionResult YazarGetir(int id)
        {
            var yzr = db.TBLYAZAR.Find(id);
            return View("YazarGetir", yzr);  //yazargetir actionresultım içerisindeki yzr adlı değere göre bana o sayfadaki değeri getirir.
        }
        public ActionResult YazarGuncelle(TBLYAZAR p)
        {
            var yzr = db.TBLYAZAR.Find(p.ID); //tblyazar içerisinde id'ye göre deger bulacak
            yzr.AD = p.AD; //yzr'den gelen ad değeri p'den gelen yeni ad değeri olacak
            yzr.SOYAD = p.SOYAD;
            yzr.DETAY = p.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }
        public ActionResult YazarKitaplar(int id)
        {
            var yazar = db.TBLKITAP.Where(x => x.YAZAR == id).ToList();  //id'ye göre kitap tablosundaki o yazara ait kitapları yazar adlı değişkenine gönderdim
            var yazarad = db.TBLYAZAR.Where(x => x.ID == id).Select(y => y.AD + " " + y.SOYAD).FirstOrDefault();  // idlerin eşit olan yer içerisinde seç,bana bu yazarın adı soyadının ilk bulduğun değerlerini getir yazarad isimli değişkene ata
            ViewBag.y1 = yazarad;  //viewbagle taşıyacağım
            return View(yazar);
        }
    }
}