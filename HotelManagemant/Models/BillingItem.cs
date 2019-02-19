using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class BillingItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemType { get; set; }
        public int Quantity { get; set; }
        public  BillingInfo  BillingInfo{ get; set; }
        public int BillingInfoId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public string EditedBy { get; set; }


    }
}