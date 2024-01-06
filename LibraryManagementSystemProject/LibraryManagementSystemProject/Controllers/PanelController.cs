using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LibraryManagementSystemProject.Models.Entity;

namespace LibraryManagementSystemProject.Controllers
{
    [Authorize]     //oturumu açılmış kullanıcılar için komut
    public class PanelController : Controller
    {
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        // GET: Panel
   
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"]; //uyemail değişkeni, kullanıcının oturum bilgilerinde "Mail" adlı anahtar ile saklanan e-posta adresini içerir
            //var degerler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyemail); // üyeler tablosu içerisinde mail adresi üyemaile eşit olan ilk değeri bana getir bunu değerler değişkenine ata
           var degerler=db.TBLDUYURULAR.ToList();
            
            var d1=db.TBLUYELER.Where(x=>x.MAIL==uyemail).Select(y=>y.AD).FirstOrDefault(); // d1 isimli değişken oluşturup, bu d1 uyelertablosu içerisinde mail adresi üyemaile eşit olanın adını bana getirsin
            ViewBag.d1 = d1;
            var d2 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault(); // d1 isimli değişken oluşturup, bu d1 uyelertablosu içerisinde mail adresi üyemaile eşit olanın soyadını bana getir
            ViewBag.d2 = d2;
            var d3 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.FOTOGRAF).FirstOrDefault(); 
            ViewBag.d3 = d3;
            var d4 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.KULLANICIADI).FirstOrDefault();
            ViewBag.d4 = d4;
            var d5 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.OKUL).FirstOrDefault(); 
            ViewBag.d5 = d5;
            var d6 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.TELEFON).FirstOrDefault();
            ViewBag.d6 = d6;
            var d7 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.MAIL).FirstOrDefault();
            ViewBag.d7 = d7;

            var uyeid = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.ID).FirstOrDefault();
           
            var d8 = db.TBLHAREKET.Where(x => x.UYE == uyeid).Count();  //hareket tablomda üyeid eşit uye ise bana bunun say
            ViewBag.d8 = d8;

            var d9 = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail).Count();  //mesajlar tablomda üyemail alıcı olarak kaç defa geçiyorsa bunu bana döndür
            ViewBag.d9 = d9;

            var d10 = db.TBLDUYURULAR.Count();  //tblduyuruların sayısını getir
            ViewBag.d10 = d10;
            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            var kullanici = (string)Session["Mail"]; //session içerisindeki mail adlı kullanıcı bilgisini (stringe çevirip) kullanici isimli değişkene atadım
            var uye = db.TBLUYELER.FirstOrDefault(x=>x.MAIL==kullanici);
            uye.SİFRE = p.SİFRE;
            uye.AD = p.AD;
            uye.SOYAD = p.SOYAD;
            uye.FOTOGRAF = p.FOTOGRAF;
            uye.TELEFON = p.TELEFON;
            uye.OKUL = p.OKUL;
            uye.KULLANICIADI = p.KULLANICIADI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Kitaplarim()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(x => x.MAIL == kullanici.ToString()).Select(x => x.ID).FirstOrDefault();
            var degerler = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            return View(degerler);
        }

        public ActionResult Duyurular()
        {
            var duyurulistesi = db.TBLDUYURULAR.ToList();
           return View(duyurulistesi);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();  //çıkış yapmak için
            return RedirectToAction("GirisYap", "Login"); //login controller içerindeki GirisYap actionresultına yönlendir
        }
        public ActionResult Yardim()
        {
            return View();
        }
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        public PartialViewResult Partial2()
        {
            var mail = (string)Session["Mail"];  //mail adlı değişken oluşturup aslında başta index içerisinde yapmış olduğum işlemleri buraya taşıdım
            var id = db.TBLUYELER.Where(x => x.MAIL == mail).Select(y => y.ID).FirstOrDefault();
            var uyegetir = db.TBLUYELER.Find(id);
            return PartialView("Partial2",uyegetir);  //uyegetirden gelen değerlerle bana partial2'yi döndür
        }
    }
}