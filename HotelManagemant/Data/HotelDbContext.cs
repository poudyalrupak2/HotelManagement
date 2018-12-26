﻿using HotelManagemant.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelManagemant.Data
{
    public class Context : DbContext
    {
        public Context() : base("defaultconnection")
        {

        }

        public DbSet<Login> Login { get; set; }
        public DbSet<MenuCategory> Menu { get; set; }
        public DbSet<MenuContinent> Continent { get; set; }
        public DbSet<Image> Image { get; set; }

        public DbSet<customers> Customers { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Hotel> hotels { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Facilities> facilities { get; set; }
        public DbSet<Billing> billing{ get; set; }
    }

}