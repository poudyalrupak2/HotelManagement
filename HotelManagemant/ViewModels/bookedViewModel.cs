using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using HotelManagemant.Models;

namespace HotelManagemant.ViewModels
{
    public class bookedViewModel
    {
        public string bookingno { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string ShortDescriptions { get; set; }
        public string roomNo { get; set; }
        public string noOfPerson { get; set; }
        public List<customers> customer { get; set; }
        public List<Room> Rooom { get; set; }
        public int Roomid  { get; set; }

        //public bookedViewModel()
        //{
        //    Rooom = new Collection<Room>();
        //}
    }


}