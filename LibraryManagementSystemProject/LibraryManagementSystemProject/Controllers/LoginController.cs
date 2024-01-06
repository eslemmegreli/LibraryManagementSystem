using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystemProject.Models.Entity;
using System.Web.Security;

namespace LibraryManagementSystemProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        [HttpGet]
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(TBLUYELER p)
        {
            var bilgiler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == p.MAIL && x.SİFRE == p.SİFRE);
            if(bilgiler !=null)   //eğer kullanıcının girdiği bilgiler ile veritabanımdaki veriler eşleşiyorsa(null değilse)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);  //kullanıcıyı kimliklendirir
                Session["Mail"] = bilgiler.MAIL.ToString();
                //TempData["ID"] = bilgiler.ID.ToString();
                //TempData["Ad"] = bilgiler.AD.ToString();
                //TempData["Soyad"] = bilgiler.SOYAD.ToString();
                //TempData["KullanıcıAdı"] = bilgiler.KULLANICIADI.ToString();
                //TempData["Sifre"] = bilgiler.SİFRE.ToString();
                //TempData["Okul"] = bilgiler.OKUL.ToString();
                Session["Ad"] = bilgiler.AD.ToString();
                Session["Soyad"] = bilgiler.SOYAD.ToString();
                return RedirectToAction("Index", "Panel");  //kimliklendirme başarılı ise kullanıcıyı panel içerisindeki index'e yönlendir.
            }
            else                         
            {
                return View();
            }
          
        }
        public ActionResult kosul()
        {
            return View();
        }
       
    }
}