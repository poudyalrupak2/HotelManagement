using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.ViewModels
{
    public class ChangeStatusViewModel
    {
        public int RoomId { get; set; }
        public string RoomNo { get; set; }
        public string Status { get; set; }
        public List<String> RoomStatus { get; set; }
    }
}