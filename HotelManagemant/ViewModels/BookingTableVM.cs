using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagemant.Models;

namespace HotelManagemant.ViewModels
{
    public class BookingTableVM
    {
        public BookingTableVM()
        {

        }
        public BookingTableVM(Bookingtable row,decimal price)
        {
            Id = row.Id;
            TableNo = row.TableNo;
            Itemname = row.Itemname;
            Type = row.Type;
            ItemId = row.ItemId;
            Quantity = row.Quantity;
            Price = price;
            SubTotal = Price * row.Quantity;
        }
        public int Id { get; set; }
        public int TableNo { get; set; }
        public string Itemname { get; set; }
        public string Type { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
    }
}