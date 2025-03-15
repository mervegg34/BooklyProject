using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using BooklyProject.Context;
using BooklyProject.Entities;
using BooklyProject.Models;

namespace BooklyProject.Controllers
{
    public class ProfileController : Controller
    {
        BooklyContext context = new BooklyContext();
        public ActionResult Index()
        {
            string userName = Session["currentUser"].ToString();
            var user = context.Admins.Where(x=>x.UserName == userName).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(Admin model)
        {
            string userName = Session["currentUser"].ToString();
            var user = context.Admins.Where(x => x.UserName == userName).FirstOrDefault();
            if (model.Password != user.Password)
            {
                ModelState.AddModelError(string.Empty, "Girdiğiniz Şifre Hatalı!");
                return View(user);
            }
            
            if (model.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "Images\\";
                var fileName = Path.Combine(saveLocation,model.ImageFile.FileName);
                model.ImageFile.SaveAs(fileName);
                user.ImageUrl = "/Images/" + model.ImageFile.FileName; 
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            context.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            string userName = Session["currentUser"].ToString();
            var user = context.Admins.Where(x => x.UserName == userName).FirstOrDefault();

            if (model.CurrentPassword != user.Password)
            {
                ModelState.AddModelError(string.Empty, "Mevcut Şifreniz Hatalı!");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            user.Password = model.ConfirmPassword;
            context.SaveChanges();  
            return RedirectToAction("Index", "Login");
        }
    }
}