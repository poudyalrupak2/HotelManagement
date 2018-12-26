namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bookingcostumerrelationship2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.customers", "BookingId", "dbo.Bookings");
            DropIndex("dbo.customers", new[] { "BookingId" });
            RenameColumn(table: "dbo.customers", name: "BookingId", newName: "Booking_BookingId");
            AlterColumn("dbo.customers", "Booking_BookingId", c => c.Int());
            CreateIndex("dbo.customers", "Booking_BookingId");
            AddForeignKey("dbo.customers", "Booking_BookingId", "dbo.Bookings", "BookingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.customers", "Booking_BookingId", "dbo.Bookings");
            DropIndex("dbo.customers", new[] { "Booking_BookingId" });
            AlterColumn("dbo.customers", "Booking_BookingId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.customers", name: "Booking_BookingId", newName: "BookingId");
            CreateIndex("dbo.customers", "BookingId");
            AddForeignKey("dbo.customers", "BookingId", "dbo.Bookings", "BookingId", cascadeDelete: true);
        }
    }
}
