using EsyaStore.Identity;
using EsyaStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace EsyaStore.Controllers
{
    namespace BlogAzureVersion.Controllers

    {

        public class AccountController : Controller

        {

            private UserManager<ApplicationUser> userManager;

            private RoleManager<ApplicationRole> roleManager;



            public AccountController()

            {

                ProductEntities db = new ProductEntities();


                UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);

                userManager = new UserManager<ApplicationUser>(userStore);



                RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);

                roleManager = new RoleManager<ApplicationRole>(roleStore);

            }
            ProductEntities db = new ProductEntities();
            // GET: Login
            public ActionResult Index()
            {
                return View();
            }

            public ActionResult Login()

            {

                return View();

            }
        [AllowAnonymous]
            [HttpPost]
            public ActionResult Login(Login model)
            {
                if (ModelState.IsValid)
                {

                    ProductEntities db = new ProductEntities();
                    if (ModelState.IsValid)

                    {

                        ApplicationUser user = userManager.Find(model.Username, model.Password);


                    

                        if (user != null)

                        {

                            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;



                            ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");

                            AuthenticationProperties authProps = new AuthenticationProperties();

                            authProps.IsPersistent = model.RememberMe;

                            authManager.SignIn(authProps, identity);




                            return RedirectToAction("Index", "Home");

                        }

                        else

                        {

                            ModelState.AddModelError("LoginUser", "Böyle bir kullanıcı bulunamadı");

                        }
                    }
                }
                return View(model);
            }
            [Authorize]
            public ActionResult Logout()

            {

                IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
                authManager.SignOut();

                return RedirectToAction("Index", "Home");

            }

            public ActionResult Register()
            {
                return View();
            }
            [HttpPost]

            [ValidateAntiForgeryToken]

            public ActionResult Register(Register model, HttpPostedFileBase Photo)

            {

                if (ModelState.IsValid)

                {
                  
                        WebImage img = new WebImage(Photo.InputStream);
                        FileInfo fotoinfo = new FileInfo(Photo.FileName);


                        string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                        img.Resize(800, 350);
                        img.Save("~/Content/UserProfile/" + newfoto);
                        model.Photo = "/Content/UserProfile/" + newfoto;
                
                    ApplicationUser user = new ApplicationUser();

                    user.Phone = model.Phone;
                    user.Photo = model.Photo;
                    user.Name = model.Name;

                    user.Surname = model.Surname;

                    user.Email = model.Email;

                    user.UserName = model.Username;



                    IdentityResult iResult = userManager.Create(user, model.PasswordHash);



                    if (iResult.Succeeded)

                    {

                        // User isminde bir Role ataması yapacağız. Bu rolü ilerleyen kısımda oluşturacağız

                        userManager.AddToRole(user.Id, "Admin");

                        return RedirectToAction("Login", "Account");

                    }

                    else

                    {

                        ModelState.AddModelError("RegisterUser", "Kullanıcı ekleme işleminde hata!");

                    }

                }



                return View(model);

            }



        }

    }
}