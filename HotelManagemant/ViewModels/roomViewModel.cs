using HotelManagemant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelManagemant.ViewModels
{
    public class roomViewModel
    {
        public string RoomNo { get; set; }
        public int Capacity { get; set; }
        public int NoofBeds { get; set; }
        public string Roomtype { get; set; }
        public virtual ICollection<int> facilitiesId { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }

        public ICollection<Facilities> Facilities { get; set; }
        public String Status { get; set; }
    }
}