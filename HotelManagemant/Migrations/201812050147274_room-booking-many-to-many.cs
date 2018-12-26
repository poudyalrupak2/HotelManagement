namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roombookingmanytomany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "roomId", "dbo.Rooms");
            DropIndex("dbo.Bookings", new[] { "roomId" });
            CreateTable(
                "dbo.RoomBookings",
                c => new
                    {
                        Room_id = c.Int(nullable: false),
                        Booking_BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Room_id, t.Booking_BookingId })
                .ForeignKey("dbo.Rooms", t => t.Room_id, cascadeDelete: true)
                .ForeignKey("dbo.Bookings", t => t.Booking_BookingId, cascadeDelete: true)
                .Index(t => t.Room_id)
                .Index(t => t.Booking_BookingId);
            
            DropColumn("dbo.Bookings", "roomId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "roomId", c => c.Int(nullable: false));
            DropForeignKey("dbo.RoomBookings", "Booking_BookingId", "dbo.Bookings");
            DropForeignKey("dbo.RoomBookings", "Room_id", "dbo.Rooms");
            DropIndex("dbo.RoomBookings", new[] { "Booking_BookingId" });
            DropIndex("dbo.RoomBookings", new[] { "Room_id" });
            DropTable("dbo.RoomBookings");
            CreateIndex("dbo.Bookings", "roomId");
            AddForeignKey("dbo.Bookings", "roomId", "dbo.Rooms", "id", cascadeDelete: true);
        }
    }
}
