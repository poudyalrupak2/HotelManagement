using HotelManagemant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagemant.ViewModels
{
    public class ResBillViewModel
    {
        public ResBillViewModel()
        {
           // grandTotal = foodItems.Sum(m => m.subtotal)*(1 - discount/100+vat/100);
        }
        public int TableNo { get; set; }
        public decimal discount { get; set; }
        public decimal vat { get; set; }
        public decimal discountAmount { get; set; }
        public decimal vatAmount { get; set; }
        public string customerName { get; set; }
        public decimal grandTotal { get; set; }
        public DateTime date { get; set; }
        public List<foodItems> foodItems { get; set; }
    }
    public class foodItems{

        public foodItems( )
        {
            subtotal = qty * rate;
        }

        public string name { get; set; }
        public int qty { get; set; }
        public decimal rate { get; set; }
        public decimal subtotal { get; set; }
    }
   
}