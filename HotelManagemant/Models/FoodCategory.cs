using System;

namespace HotelManagemant.Models
{
    public class FoodCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Descriprion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public string EditedBy { get; set; }

    }
}