using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BooklyProject.Context;
using BooklyProject.Entities;

namespace BooklyProject.Controllers
{
    public class PhotoGalleryController : Controller
    {
        BooklyContext context = new BooklyContext();
        public ActionResult Index()
        {
            var values = context.PhotoGalleries.ToList();
            return View(values);
        }

        public ActionResult DeletePhoto(int id)
        {
            var value = context.PhotoGalleries.Find(id);
            context.PhotoGalleries.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(PhotoGallery model)
        {
            context.PhotoGalleries.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}