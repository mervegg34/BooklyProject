using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BooklyProject.Context;
using BooklyProject.Entities;

namespace BooklyProject.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        BooklyContext context = new BooklyContext();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult DefaultCategories()
        {
            var values = context.Categories.OrderByDescending(x=>x.CategoryId).Take(6).ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultBooks()
        {
            var values = context.Books.OrderByDescending(x => x.BookId).Take(6).ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultBanner()
        {
            var values = context.Banners.OrderBy(x => x.BannerId).ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultServices()
        {
            var values = context.Services.OrderBy(x => x.ServiceId).ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultFeatured()
        {
            var values = context.Books
                       .OrderBy(x => Guid.NewGuid()) 
                       .Take(3)
                       .ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultBestReview()
        {
            var values = context.Books
                        .OrderByDescending(x => x.Review)
                         .Take(3)
                        .ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultBestPrice()
        {
            var values = context.Books
                        .OrderBy(x => x.Price)
                        .Take(3)
                        .ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultOnSale()
        {
            var values = context.Books
                         .Where(x => x.IsOnSale)
                         .OrderBy(x => x.BookId)
                         .Take(3)
                         .ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultTestimonials()
        {
            var values = context.Testimonials.OrderBy(x => x.TestimonialId).ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultPhotoGallery()
        {
            var values = context.PhotoGalleries.OrderBy(x => x.PhotoGalleryId).ToList();
            return PartialView(values);
        }

        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult SendMessage(Message model)
        {
            context.Messages.Add(model);
            context.SaveChanges();
            Thread.Sleep(2000);
            return RedirectToAction("Index");
        }
    }
}