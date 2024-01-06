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
    public class KategoriController : Controller
    {
        // GET: Kategori
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();  //veritabanımı tanıttım
        public ActionResult Index(int page=1)  //sayfa açıldığında 1.sayfadan başlasın diye 1 değerini verdim
        {
            var degerler = db.TBLKATEGORİ.Where(x=>x.DURUM==true).ToList().ToPagedList(page, 10);  //Onar onar sayfalasın
            return View(degerler); //geriye degerleri döndür
        }
        [HttpGet] //herhangi bir işlem yapılmazsa
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost] //işlem yapıldığında
        public ActionResult KategoriEkle(TBLKATEGORİ p)
        {
            if (!ModelState.IsValid)             //Eğer ModelState geçerli değilse, yani formun doğrulama kurallarına uymayan bir durum varsa
            {
                return View("KategoriEkle");     //KategoriEkle'ye dön
            }
            db.TBLKATEGORİ.Add(p);               //değilse p parametresini tabloya ekle
            db.SaveChanges();                    //değişiklikleri kaydet
            return RedirectToAction("KategoriEkle"); //KategoriEkleye dön
        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBLKATEGORİ.Find(id); //kategori tablosu içerisinde id bul onu kategori değişkenine ata
            //db.TBLKATEGORİ.Remove(kategori);        //kategori değerini tablodan sil
            kategori.DURUM = false;                   //durum değerini false yapar böylece tabloda gözükmez
            db.SaveChanges();                       // değişiklikleri kaydet
            return RedirectToAction("Index");       //index sayfasına geri dön
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBLKATEGORİ.Find(id); //id ye göre değeri bul
            return View("KategoriGetir", ktg); //kategorigetir sayfasını döndür fakat ktgden gelen değerle dön
        }
        public ActionResult KategoriGuncelle(TBLKATEGORİ p1)
        {
            var ktg = db.TBLKATEGORİ.Find(p1.ID);  //kategori tablosundaki p1 ID'sini bul onu ktg değişkenine ata
            ktg.AD = p1.AD;                        //p1'deki ad'ı ktg'nin Ad içerisine ata
            db.SaveChanges();
            return RedirectToAction("Index");      //index sayfasına geri dön
        }
    }
}