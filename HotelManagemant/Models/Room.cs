using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class Room
    {
        public int id { get; set; }
        public Hotel Hotel { get; set; }
        public string RoomNo { get; set; }
        public int Capacity { get; set; }
        public int NoofBeds { get; set; }
        public string Roomtype { get; set; }
        public virtual ICollection<Facilities> Facilities { get; set; }
        public virtual  ICollection<Booking> Bookings { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }

        public String Status { get; set; }
        public Room()
        {
            this.Facilities = new HashSet<Facilities>();
            this.Bookings = new HashSet<Booking>();
        }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public string EditedBy { get; set; }


    }

}