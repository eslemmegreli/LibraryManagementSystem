using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LibraryManagementSystemProject.Models.Entity;

namespace LibraryManagementSystemProject.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities(); 
        // GET: AdminLogin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(TBLADMIN p)
        {
            var bilgiler = db.TBLADMIN.FirstOrDefault(x => x.Kullanici == p.Kullanici && x.Sifre == p.Sifre); // tbladmin tablosundan kullanıcı adı vee şifre alanları eşleşen ilk kaydı al,bilgiler değişkenine ata
            if(bilgiler != null)   //bilgiler null değilse
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Kullanici, false);  //kullanıcının bilgilerini kimliklendiren çerez oluşturdum, false ile ise çerezin kalıcı olmadığını belirttim
                Session["Kullanici"] = bilgiler.Kullanici.ToString(); //Kullanıcının oturum bilgilerini, oturum süresince kullanılabilecek bir Session değişkenine ata
                return RedirectToAction("Index", "Kategori"); //kullanıcı bilgileri doğrulandığında kategori controller içerisindeki index sayfasına git
            }
            else
            {
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();  //çıkış yapmak için
            return RedirectToAction("Login", "AdminLogin"); //login controller içerindeki GirisYap actionresultına yönlendir
        }
    }
}