using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooklyProject.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public virtual List<BookCategory> BookCategories { get; set; }
    }
}