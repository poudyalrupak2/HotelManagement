using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class RegisterFood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UnitQuintity { get; set; }
        public decimal Unit { get; set; }
        public decimal Price { get; set; }
        public string Ingredients { get; set; }
        public FoodCategory FoodCategory { get; set; }
        public int FoodCategoryId { get; set; }
    }
}