using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class MenuRegistration
    {
        public int Id { get; set; }
        public MenuCategory MenuCategory { get; set; }
        public String Itemname { get; set; }
        public string Ingrident { get; set; }
        public MenuContinent MenuContinent { get; set; }
        public String Price { get; set; }
        public string Thumbnail { get; set; }
        [NotMapped]
        public HttpPostedFileBase TImage { get; set; }
        public ICollection<Image> Image { get; set; }
    }
}