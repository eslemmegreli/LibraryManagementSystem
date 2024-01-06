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
    public class KitapController : Controller
    {
        // GET: Kitap
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        public ActionResult Index(string p)
        {
            var kitaplar = from k in db.TBLKITAP select k;
            if (!string.IsNullOrEmpty(p))         //eğerki input(textbox kısmı) null değil veya boş değilse
            {
                kitaplar = kitaplar.Where(x => x.AD.Contains(p));  //Kitaplar içerisinde, p'nin içerdiği değere göre kitabı bul     
            }

            return View(kitaplar.ToList());    //içermiyorsada kitapların listesini döndür
        }




        [HttpGet]                                                                        //hiçbir işlem gerçekleşmezse
        public ActionResult KitapEkle() 
        {

            List<SelectListItem> deger1 = (from i in db.TBLKATEGORİ.ToList()            /* Listeden değer 1 isimli bir öğe seçer ve tbl kategoriden i değeri gelir*/
                                           select new SelectListItem                    /* şu öğeyi seç*/
                                           {                     
                                               Text = i.AD,                             /* AD'ı texte, ID ise değere ata YANİ; arka planda id değerini çalıştır önde ise ad değerini çalıştır*/
                                               Value = i.ID.ToString()                 
                                           }).ToList();                                 /*listele*/
            ViewBag.dgr1 = deger1;                                                      /*dgr1 i taşı deger1'den al*/

            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + ' ' + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View();
        }
        [HttpPost]                                                                           //herhangi bir işlem gerçekleşirse
        public ActionResult KitapEkle(TBLKITAP p)
        {
            var ktg = db.TBLKATEGORİ.Where(k => k.ID == p.TBLKATEGORİ.ID).FirstOrDefault();  //ilk eşleşen kaydı alır veya eşleşmezse null alır,değeri ktg'ye ata
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();        //ilk veya varsayılan seçilmiş değeri yzr değişkenine ata
            p.TBLKATEGORİ = ktg;                                                             //kitabın kategorisini veritabanından alınan kategoriye(ktg) ayarla.
            p.TBLYAZAR = yzr;
            db.TBLKITAP.Add(p);                                                              //tblkitap tablosuna ekle
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public ActionResult KitapSil(int id)
        {
            var ktp = db.TBLKITAP.Find(id);                //parametreden girilen id'ye göre tblkitap içerisinde bul ve onu ktp değişkenine ata
            db.TBLKITAP.Remove(ktp);                       //ktp'yi kitap tablosundan sil
            db.SaveChanges();
            return RedirectToAction("Index");              //indexe dön

        }
        public ActionResult KitapGetir(int id)
        {
            var ktp = db.TBLKITAP.Find(id);


            List<SelectListItem> deger1 = (from i in db.TBLKATEGORİ.ToList()      
                                           select new SelectListItem              
                                           {
                                               Text = i.AD,                           
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + ' ' + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;


            return View("KitapGetir", ktp);  //kitapgetir actionresultım içerisindeki ktp adlı değere göre bana o sayfadaki değeri getirir.
        }
        public ActionResult KitapGuncelle(TBLKITAP p)
        {
            var ktp = db.TBLKITAP.Find(p.ID);             //girilen değere göre tblkitaptan bul ve ktp içine ata
            ktp.AD = p.AD;                                //yeni girilen ad değerini eskisi yerine al
            ktp.KATEGORİ = p.KATEGORİ;                    //yeni girilen kategori değerini eskisi yerine al
            ktp.YAZAR = p.YAZAR;
            ktp.BASIMYIL = p.BASIMYIL;
            ktp.SAYFA = p.SAYFA;
            ktp.YAYINEVI = p.YAYINEVI;
            ktp.DURUM = true;                            //durumu true yap
            var ktg = db.TBLKATEGORİ.Where(k => k.ID == p.TBLKATEGORİ.ID).FirstOrDefault();     //tblkategori id ile aynı id olanların ilk değerini al ve ktg değişkenine ata
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();           //tblyazar id ile aynı id olanların ilk değerini al ve yzr değişkenine ata
            ktp.KATEGORİ = ktg.ID;                                                              //ktg id'yi kategoriye ata
            ktp.YAZAR = yzr.ID;                                                                 //yzr id'yi yazara ata
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}