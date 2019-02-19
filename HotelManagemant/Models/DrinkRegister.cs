using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class DrinkRegister
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int  Quantity{ get; set; }
        public string UnitName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool status { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
        public ICollection<DrinkImage> DrinkImage { get; set; }
        public DrinkCategory DrinkCategory { get; set; }
        public int DrinkCategoryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public string EditedBy { get; set; }

    }
}