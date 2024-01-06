using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystemProject.Models.Entity;

namespace LibraryManagementSystemProject.Controllers
{
    [AllowAnonymous]
    public class KayitOlController : Controller
    {

        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        // GET: KayitOl

        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(TBLUYELER p)
        {
            if (!ModelState.IsValid)
            {
                return View("Kayit");
            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}