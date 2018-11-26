using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class customers
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string NationalIdNo { get; set; }
        public string Nationality { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public customers()
        {
            this.Bookings = new HashSet<Booking>();
        }
    }
}