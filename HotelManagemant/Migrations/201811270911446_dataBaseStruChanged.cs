namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataBaseStruChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.customersBookings", "customers_Id", "dbo.customers");
            DropForeignKey("dbo.customersBookings", "Booking_BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Rooms", "Booking_BookingId", "dbo.Bookings");
            DropIndex("dbo.Rooms", new[] { "Booking_BookingId" });
            DropIndex("dbo.customersBookings", new[] { "customers_Id" });
            DropIndex("dbo.customersBookings", new[] { "Booking_BookingId" });
            AddColumn("dbo.Bookings", "roomId", c => c.Int(nullable: false));
            AddColumn("dbo.customers", "Booking_BookingId", c => c.Int());
            CreateIndex("dbo.Bookings", "roomId");
            CreateIndex("dbo.customers", "Booking_BookingId");
            AddForeignKey("dbo.customers", "Booking_BookingId", "dbo.Bookings", "BookingId");
            AddForeignKey("dbo.Bookings", "roomId", "dbo.Rooms", "id", cascadeDelete: true);
            DropColumn("dbo.Rooms", "Booking_BookingId");
            DropTable("dbo.customersBookings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.customersBookings",
                c => new
                    {
                        customers_Id = c.Int(nullable: false),
                        Booking_BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.customers_Id, t.Booking_BookingId });
            
            AddColumn("dbo.Rooms", "Booking_BookingId", c => c.Int());
            DropForeignKey("dbo.Bookings", "roomId", "dbo.Rooms");
            DropForeignKey("dbo.customers", "Booking_BookingId", "dbo.Bookings");
            DropIndex("dbo.customers", new[] { "Booking_BookingId" });
            DropIndex("dbo.Bookings", new[] { "roomId" });
            DropColumn("dbo.customers", "Booking_BookingId");
            DropColumn("dbo.Bookings", "roomId");
            CreateIndex("dbo.customersBookings", "Booking_BookingId");
            CreateIndex("dbo.customersBookings", "customers_Id");
            CreateIndex("dbo.Rooms", "Booking_BookingId");
            AddForeignKey("dbo.Rooms", "Booking_BookingId", "dbo.Bookings", "BookingId");
            AddForeignKey("dbo.customersBookings", "Booking_BookingId", "dbo.Bookings", "BookingId", cascadeDelete: true);
            AddForeignKey("dbo.customersBookings", "customers_Id", "dbo.customers", "Id", cascadeDelete: true);
        }
    }
}
