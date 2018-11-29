namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingNoAsForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.customers", "Booking_BookingId", "dbo.Bookings");
            DropIndex("dbo.customers", new[] { "Booking_BookingId" });
            RenameColumn(table: "dbo.customers", name: "Booking_BookingId", newName: "BookingNo");
            DropPrimaryKey("dbo.Bookings");
            AlterColumn("dbo.Bookings", "BookingId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "bookingNo", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.customers", "BookingNo", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Bookings", "bookingNo");
            CreateIndex("dbo.customers", "BookingNo");
            AddForeignKey("dbo.customers", "BookingNo", "dbo.Bookings", "bookingNo");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.customers", "BookingNo", "dbo.Bookings");
            DropIndex("dbo.customers", new[] { "BookingNo" });
            DropPrimaryKey("dbo.Bookings");
            AlterColumn("dbo.customers", "BookingNo", c => c.Int());
            AlterColumn("dbo.Bookings", "bookingNo", c => c.String());
            AlterColumn("dbo.Bookings", "BookingId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Bookings", "BookingId");
            RenameColumn(table: "dbo.customers", name: "BookingNo", newName: "Booking_BookingId");
            CreateIndex("dbo.customers", "Booking_BookingId");
            AddForeignKey("dbo.customers", "Booking_BookingId", "dbo.Bookings", "BookingId");
        }
    }
}
