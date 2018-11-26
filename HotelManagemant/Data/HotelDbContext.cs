using HotelManagemant.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelManagemant.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext() : base("defaultconnection")
        {

        }


        public DbSet<customers> Customers { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Hotel> hotels { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Facilities> facilities { get; set; }
    }

}