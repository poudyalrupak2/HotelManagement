namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        CheckIn = c.DateTime(nullable: false),
                        CheckoutDate = c.DateTime(nullable: false),
                        NoOfPersons = c.String(),
                        ShortDescriptions = c.String(),
                    })
                .PrimaryKey(t => t.BookingId);
            
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Address = c.String(),
                        NationalIdNo = c.String(),
                        Nationality = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        RoomNo = c.String(),
                        Capacity = c.Int(nullable: false),
                        NoofBeds = c.Int(nullable: false),
                        Roomtype = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        ImageName = c.String(),
                        Status = c.Boolean(nullable: false),
                        Hotel_Id = c.Int(),
                        Booking_BookingId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Hotels", t => t.Hotel_Id)
                .ForeignKey("dbo.Bookings", t => t.Booking_BookingId)
                .Index(t => t.Hotel_Id)
                .Index(t => t.Booking_BookingId);
            
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Room_id = c.Int(),
                        Hotel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_id)
                .ForeignKey("dbo.Hotels", t => t.Hotel_Id)
                .Index(t => t.Room_id)
                .Index(t => t.Hotel_Id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HotelName = c.String(),
                        Type = c.String(),
                        Location = c.String(),
                        Email = c.String(),
                        Ownername = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.customersBookings",
                c => new
                    {
                        customers_Id = c.Int(nullable: false),
                        Booking_BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.customers_Id, t.Booking_BookingId })
                .ForeignKey("dbo.customers", t => t.customers_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bookings", t => t.Booking_BookingId, cascadeDelete: true)
                .Index(t => t.customers_Id)
                .Index(t => t.Booking_BookingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "Booking_BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Rooms", "Hotel_Id", "dbo.Hotels");
            DropForeignKey("dbo.Facilities", "Hotel_Id", "dbo.Hotels");
            DropForeignKey("dbo.Facilities", "Room_id", "dbo.Rooms");
            DropForeignKey("dbo.customersBookings", "Booking_BookingId", "dbo.Bookings");
            DropForeignKey("dbo.customersBookings", "customers_Id", "dbo.customers");
            DropIndex("dbo.customersBookings", new[] { "Booking_BookingId" });
            DropIndex("dbo.customersBookings", new[] { "customers_Id" });
            DropIndex("dbo.Facilities", new[] { "Hotel_Id" });
            DropIndex("dbo.Facilities", new[] { "Room_id" });
            DropIndex("dbo.Rooms", new[] { "Booking_BookingId" });
            DropIndex("dbo.Rooms", new[] { "Hotel_Id" });
            DropTable("dbo.customersBookings");
            DropTable("dbo.Hotels");
            DropTable("dbo.Facilities");
            DropTable("dbo.Rooms");
            DropTable("dbo.customers");
            DropTable("dbo.Bookings");
        }
    }
}
