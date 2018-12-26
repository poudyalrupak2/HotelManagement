using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.ViewModels
{
    public class AddCustomerViewModel
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string NationalIdNo { get; set; }
        public string BookingNo { get; set; }
        public string Nationality { get; set; }
    }
}