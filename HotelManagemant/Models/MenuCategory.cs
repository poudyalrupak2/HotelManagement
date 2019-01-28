using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class MenuCategory
    {
        public int id { get; set; }
        public String CategoryName { get; set;  }
        public string Detail { get; set; }
        public string thumbnail { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
    }
}