namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cusbookmtmr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.customers", "Booking_BookingId", "dbo.Bookings");
            DropIndex("dbo.customers", new[] { "Booking_BookingId" });
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
            
            DropColumn("dbo.customers", "Booking_BookingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.customers", "Booking_BookingId", c => c.Int());
            DropForeignKey("dbo.customersBookings", "Booking_BookingId", "dbo.Bookings");
            DropForeignKey("dbo.customersBookings", "customers_Id", "dbo.customers");
            DropIndex("dbo.customersBookings", new[] { "Booking_BookingId" });
            DropIndex("dbo.customersBookings", new[] { "customers_Id" });
            DropTable("dbo.customersBookings");
            CreateIndex("dbo.customers", "Booking_BookingId");
            AddForeignKey("dbo.customers", "Booking_BookingId", "dbo.Bookings", "BookingId");
        }
    }
}
