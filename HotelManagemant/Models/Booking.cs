using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public ICollection<Room> rooms { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string NoOfPersons { get; set; }
        public string ShortDescriptions { get; set; }
        public ICollection<customers> customers { get; set; }
        public Booking()
        {
            this.customers = new HashSet<customers>();
        }

    }
}