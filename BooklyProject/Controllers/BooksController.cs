using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BooklyProject.Context;
using BooklyProject.Entities;

namespace BooklyProject.Controllers
{
    
    public class BooksController : Controller
    {
        BooklyContext context = new BooklyContext();
        public ActionResult Index(string searchText )
        {

            List<Book> values;

            if(searchText != null)
            {
                values = context.Books.Where(x=>x.BookName.Contains(searchText)).ToList();
                return View(values);
            }

            values = context.Books.ToList();
            return View(values);
        }

        public ActionResult DeleteBook(int id)
        {
            var value = context.Books.Find(id);
            context.Books.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            var authors = context.Authors
                .Select(x => new
                {
                    x.AuthorId,
                    FullName = x.Name + " " + x.Surname
                })
                .ToList();

            ViewBag.Authors = new SelectList(authors, "AuthorId", "FullName");
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(Book model)
        {
            if (ModelState.IsValid)
            {
                context.Books.Add(model);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            var authors = context.Authors
                .Select(x => new
                {
                    x.AuthorId,
                    FullName = x.Name + " " + x.Surname
                })
                .ToList();

            ViewBag.Authors = new SelectList(authors, "AuthorId", "FullName");

            return View(model);
        }

        [HttpGet]
        public ActionResult UpdateBook(int id)
        {
            
            var values = context.Books.Find(id);
 
            var authors = context.Authors
                .Select(x => new
                {
                    x.AuthorId,
                    FullName = x.Name + " " + x.Surname
                })
                .ToList();

            ViewBag.Authors = new SelectList(authors, "AuthorId", "FullName", values.AuthorId);
            return View(values);
        }

        [HttpPost]
        public ActionResult UpdateBook(Book model)
        {
            if (ModelState.IsValid)
            {
                var value = context.Books.Find(model.BookId);
                if (value != null)
                {
                    value.BookName = model.BookName;
                    value.CoverImageUrl = model.CoverImageUrl;
                    value.AuthorId = model.AuthorId;
                    value.Price = model.Price;
                    value.Review = model.Review;
                    value.DiscountRate = model.DiscountRate;
                    value.IsOnSale = model.IsOnSale;

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            var authors = context.Authors
                .Select(x => new
                {
                    x.AuthorId,
                    FullName = x.Name + " " + x.Surname
                })
                .ToList();

            ViewBag.Authors = new SelectList(authors, "AuthorId", "FullName", model.AuthorId);

            return View(model);
        }
    }
}