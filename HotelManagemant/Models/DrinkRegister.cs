using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class DrinkRegister
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UnitQuantity { get; set; }
        public decimal UnitName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public DrinkCategory DrinkCategory { get; set; }
        public int DrinkCategoryId { get; set; }
    }
}