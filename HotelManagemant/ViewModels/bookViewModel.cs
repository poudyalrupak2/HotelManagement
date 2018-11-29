using HotelManagemant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.ViewModels
{
    public class bookViewModel
    {
        public int roomId { get; set; }
        public string bookingno { get; set; }
        public string roomNo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string NoOfPersons { get; set; }
        public string ShortDescriptions { get; set; }
        public int Id { get; set; } 
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string NationalIdNo { get; set; }
        public string Nationality { get; set; }
    }
}