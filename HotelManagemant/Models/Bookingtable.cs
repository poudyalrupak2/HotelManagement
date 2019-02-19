using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class Bookingtable
    {
        public int Id { get; set; } 
        public int TableNo { get; set; }
        public string Itemname{ get; set; }
        public string Type { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public string EditedBy { get; set; }

    }
}