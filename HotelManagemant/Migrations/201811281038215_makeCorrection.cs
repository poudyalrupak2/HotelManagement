namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class makeCorrection : DbMigration
    {
        public override void Up()
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

        public override void Down()
        {
            
        }
    }
        
    }

