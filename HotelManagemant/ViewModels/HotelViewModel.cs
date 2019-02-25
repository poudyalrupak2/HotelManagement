using HotelManagemant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelManagemant.ViewModels
{
    public class HotelViewModel
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string Type { get; set; }
       public string PhoneNo { get; set; }
        public ICollection<int> FacilitiesId { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Ownername { get; set; }
        public string ImageName { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Facilities> Facilities { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }

    }
}