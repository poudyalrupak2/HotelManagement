using HotelManagemant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.ViewModels
{
    public class printListViewModel
    {
        public string bookingno { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string ShortDescriptions { get; set; }
        public string noOfPerson { get; set; }
        public List<customers> customer { get; set; }
        public List<Room> Rooom { get; set; }
        public decimal AdvanceAmount { get; set; }
        public int BookingId { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal RemainAmount { get; set; }
        public int NoOfDays { get; set; }
        public string BillNo { get; set; }
    }
}