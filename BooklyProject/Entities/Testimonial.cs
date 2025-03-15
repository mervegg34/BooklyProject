using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooklyProject.Entities
{
    public class Testimonial
    {
        public int TestimonialId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Review { get; set; }
    }
}