using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public string bookingNo { get; set; }
        [ForeignKey("Room")]
        public int roomId { get; set; }
        public virtual Room Room { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string NoOfPersons { get; set; }
        public string ShortDescriptions { get; set; }
        public ICollection<customers> customers { get; set; }
      

    }
}