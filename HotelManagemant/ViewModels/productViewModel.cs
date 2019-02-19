using HotelManagemant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.ViewModels
{
    public class productViewModel
    {
        public IEnumerable<RegisterFood> RegisterFood { get; set; }
        public IEnumerable<DrinkRegister> DrinkRegister { get; set; }
        public IEnumerable<TableRegister> tableRegister { get; set; }
        public List<BookingTableVM>  bookingtables{ get; set; }
        public int tableNo { get; set; }
    }
}