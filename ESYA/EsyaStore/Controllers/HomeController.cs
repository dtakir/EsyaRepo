using EsyaStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EsyaStore.Controllers
{
    public class HomeController : Controller
    {
               
        // GET: Home
        private ProductEntities db = new ProductEntities();
        public ActionResult Index()
        {
            if (User.IsInRole(role:"Admin"))
            {
                return RedirectToAction("AdminIndex");
            }
            var products = db.Products.Include(x=>x.ApplicationUser);
            return View(products.ToList());
        }
        public ActionResult Contact()
        {
         return View();

        }
        public ActionResult AdminIndex()
        {
         
            ViewBag.User= db.Users.Count();
            ViewBag.Product = db.Products.Count();
          


            return View();


        }
        [Authorize]
        public ActionResult Rising_price()
        {
            var sıra = db.Products.OrderBy(x => x.Price);
            return View(sıra);
        }
        [Authorize]
        public ActionResult Decreasing_price()
        {
            var sıra = db.Products.OrderByDescending(x => x.Price);
            return View(sıra);
        }
        [Authorize]
       
        public ActionResult UserTable()
        {
            return View(db.Users.ToList());
        }
        public ActionResult ProductTable()
        {
            var Product = db.Products.Include(p=>p.Country).Include(p => p.ApplicationUser);
          
            return View(Product.ToList());
           
        }
       

    }
}