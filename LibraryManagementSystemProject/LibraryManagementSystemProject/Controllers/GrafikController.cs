using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystemProject.Models;

namespace LibraryManagementSystemProject.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VisualizeKitapResult()
        {
            return Json(liste());   //liste isimli bir metodu bana geri döndürür
        }
        public List<Class1> liste()   //class1'i liste formatında burada liste isimli metodla çağırdım
        {
            List<Class1> cs = new List<Class1>();  //class1 sınıfından cs isimli bir nesne türettim
            cs.Add(new Class1() 
            { 
                yayinevi="CAN",                 //CAN yayınevinin kitap sayısı 4 
                sayi=4
            });
            cs.Add(new Class1()
            {
                yayinevi = "Türkiye İş Bankası Kültür Yayınları",
                sayi = 13
            });
            cs.Add(new Class1()
            {
                yayinevi = "Altın Kitaplar",
                sayi = 4
            });
            return cs;
        }
    }
}