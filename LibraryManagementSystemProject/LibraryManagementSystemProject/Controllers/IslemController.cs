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
    public class IslemController : Controller
    {
       
        dbKUTUPHANEEntities db = new dbKUTUPHANEEntities();
        // GET: Islem
        public ActionResult Index(int page=1)
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == true).ToList().ToPagedList(page,7); //hareket tablosunda durum true ise sayfalama yaparak listele
            return View(degerler);
        }
    }
}