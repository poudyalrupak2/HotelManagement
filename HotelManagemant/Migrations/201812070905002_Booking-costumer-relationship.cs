namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bookingcostumerrelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.customers", "Booking_BookingId", "dbo.Bookings");
            DropIndex("dbo.customers", new[] { "Booking_BookingId" });
            RenameColumn(table: "dbo.customers", name: "Booking_BookingId", newName: "BookingId");
            AlterColumn("dbo.customers", "BookingId", c => c.Int(nullable: false));
            CreateIndex("dbo.customers", "BookingId");
            AddForeignKey("dbo.customers", "BookingId", "dbo.Bookings", "BookingId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.customers", "BookingId", "dbo.Bookings");
            DropIndex("dbo.customers", new[] { "BookingId" });
            AlterColumn("dbo.customers", "BookingId", c => c.Int());
            RenameColumn(table: "dbo.customers", name: "BookingId", newName: "Booking_BookingId");
            CreateIndex("dbo.customers", "Booking_BookingId");
            AddForeignKey("dbo.customers", "Booking_BookingId", "dbo.Bookings", "BookingId");
        }
    }
}
