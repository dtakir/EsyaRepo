using EsyaStore.Identity;
using EsyaStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace EsyaStore.Controllers
{
    public class UserController : Controller
    {
        ProductEntities db = new ProductEntities();

        private UserManager<ApplicationUser> userManager;

        private RoleManager<ApplicationRole> roleManager;



        public UserController()

        {

     


            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);

            userManager = new UserManager<ApplicationUser>(userStore);



            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);

            roleManager = new RoleManager<ApplicationRole>(roleStore);

        }
        // GET: User
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        [Authorize]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationUser user, HttpPostedFileBase Photo)
        {
            

                if (ModelState.IsValid)

                {

                    WebImage img = new WebImage(Photo.InputStream).Resize(800, 300, false, true);
                    FileInfo fotoinfo = new FileInfo(Photo.FileName);


                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                   
                    img.Save("~/Content/UserProfile/" + newfoto);
                    user.Photo = "/Content/UserProfile/" + newfoto;

                  
                    IdentityResult iResult = userManager.Create(user, user.PasswordHash);



                    if (iResult.Succeeded)

                    {

                        // User isminde bir Role ataması yapacağız. Bu rolü ilerleyen kısımda oluşturacağız

                        userManager.AddToRole(user.Id, "Admin");

                      

                    }

                    else

                    {

                        ModelState.AddModelError("RegisterUser", "Kullanıcı ekleme işleminde hata!");

                    }

                }



                return View(user);

            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string Id)
        {
            ApplicationUser user = null;
            if (Id != null)
                {
                    user = db.Users.Where(x => x.Id ==Id).FirstOrDefault();

            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser  model, HttpPostedFileBase Photo)
        {
            ApplicationUser user = db.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            if (user != null)
            {
                if (Photo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(user.Photo)))
                    {
                        System.IO.File.Delete(Server.MapPath(user.Photo));
                    }
                    WebImage img = new WebImage(Photo.InputStream).Resize(800, 300, false, true);
                    FileInfo fotoinfo = new FileInfo(Photo.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
               
                    img.Save("~/Content/Dosyalar/" + newfoto);
                    user.Photo = "/Content/Dosyalar/" + newfoto;

                   
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
              
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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