using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class BillingInfo
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public int TableNo { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public DateTime Date { get; set; }
        public ICollection<BillingItem> BillingItems { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public string EditedBy { get; set; }

    }
}