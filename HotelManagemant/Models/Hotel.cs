using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelManagemant.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string  Type { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Facilities> Facilities { get; set; }
        public string Location { get; set; }
        public string Email{ get; set; }
        public string Ownername { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
        public Hotel()
        {
            this.Facilities = new HashSet<Facilities>();
        }
    }
}