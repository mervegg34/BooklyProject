using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BooklyProject.Context;
using BooklyProject.Entities;

namespace BooklyProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        BooklyContext context = new BooklyContext();
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin model)
        {
            var admin = context.Admins.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
            if (admin == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı!");
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(admin.UserName,false);
            Session["currentUser"] = admin.UserName;
            return RedirectToAction("Index", "Category");
        }

       public ActionResult Logout()
       {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Default");
       }
    }
}