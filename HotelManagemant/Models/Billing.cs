using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class Billing
    {
        public int BillingId { get; set; }
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        public decimal Discount { get; set; }
        public decimal RemainAmount { get; set; }
        public string BillNo { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NoOfDays { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public string EditedBy { get; set; }


    }
}