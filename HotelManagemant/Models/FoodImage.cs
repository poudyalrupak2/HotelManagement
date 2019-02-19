using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace HotelManagemant.Models
{
    public class FoodImage
    {
        public int Id { get; set; }
        [NotMapped]
        public IEnumerable<HttpPostedFile> Img { get; set; }
        public string ImageName { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
        public virtual FoodCategory FoodCategory { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public string EditedBy { get; set; }

    }
}