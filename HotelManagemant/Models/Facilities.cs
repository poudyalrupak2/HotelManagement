using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using HotelManagemant.Models;

namespace HotelManagemant.Models
{
    public class Facilities
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public string Type { get; set; }
        public ICollection<Room> Room { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
        public Facilities()
        {
            this.Room = new HashSet<Room>();
            this.Hotels = new HashSet<Hotel>();
        }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public string EditedBy { get; set; }


    }
}