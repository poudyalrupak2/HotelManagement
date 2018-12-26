using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagemant.Models;

namespace HotelManagemant.ViewModels
{
    public class AddRoomViewModel
    {
        public string BookingNo { get; set; }
        public int RoomId { get; set; }
        public IEnumerable<Room> rooms { get; set; }
    }
}