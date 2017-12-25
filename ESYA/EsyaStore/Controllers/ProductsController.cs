using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EsyaStore.Models;
using System.Web.Helpers;
using System.IO;
using EsyaStore.Identity;
using Microsoft.AspNet.Identity;

namespace EsyaStore.Controllers
{
    public class ProductsController : Controller
    {
        private ProductEntities db = new ProductEntities();

        // GET: Products
        [Authorize]
        public ActionResult Index()
        {
           
            string id = User.Identity.GetUserId();
            var products = db.Products.Include(p => p.Country).Where(x=>x.ApplicationUserId==id);
            ViewData["a"] = db.Country.Include(x=>x.District);
            return View(products.ToList());
        }
          public ActionResult District(int? CountryId)
        {
            var District = db.District.Where(d => d.CountryId == CountryId).
                Select(x=> new {DistrictId=x.DistrictId,DistrictName=x.DistrictName}).ToList() ;
            return Json(District);
        }

       
        // GET: Products/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Country, "CountryId", "CountryName");
            //ViewBag.DistrictId = new SelectList(db.District, "DistrictId", "DistrictName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Comment,ApplicationUserId,CountryId,DistrictId,FileName,CreatedDate,IsDeleted")] Product product, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
              
              string id=  User.Identity.GetUserId();
                product.ApplicationUserId = id;
                //Fotoğraf ekleme
                WebImage img = new WebImage(Photo.InputStream).Resize(800, 300, false, true);
                FileInfo fotoinfo = new FileInfo(Photo.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
               
                    img.Save("~/Content/Dosyalar/" + newfoto);
                   product.Photo = "/Content/Dosyalar/" + newfoto;
    
           
                  
             
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            
            }
          
            ViewBag.CountryId = new SelectList(db.Country, "CountryId", "CountryName", product.CountryId);
            //ViewBag.DistrictId = new SelectList(db.District, "DistrictId", "DistrictName", product.DistrictId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            Product product = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            product = db.Products.Where(x => x.ProductId == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
           
            ViewBag.CountryId = new SelectList(db.Country, "CountryId", "CountryName", product.CountryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Comment,ApplicationUserId,CountryId,DistrictId,FileName,CreatedDate,IsDeleted")]Product product, HttpPostedFileBase Photo)
        {

            if (ModelState.IsValid)
            {


                string id = User.Identity.GetUserId();
                product.ApplicationUserId = id;
                if (product != null)
                {
                    if (Photo != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(product.Photo)))
                        {
                            System.IO.File.Delete(Server.MapPath(product.Photo));
                        }
                        WebImage img = new WebImage(Photo.InputStream).Resize(800,300,false,true);
                        FileInfo fotoinfo = new FileInfo(Photo.FileName);
                        string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                     
                        img.Save("~/Content/Dosyalar/" + newfoto);
                        product.Photo = "/Content/Dosyalar/" + newfoto;

                    }
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            ViewBag.CountryId = new SelectList(db.Country, "CountryId", "CountryName", product.CountryId);
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
               db.Products.Remove(product);
          
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Details(int? id)
        {
            var SelectPro = db.Products.Where(x => x.ProductId == id);
            return View(SelectPro.ToList());
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
