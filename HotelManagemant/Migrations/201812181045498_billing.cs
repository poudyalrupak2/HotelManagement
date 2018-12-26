namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class billing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Billings",
                c => new
                    {
                        BillingId = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemainAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillNo = c.String(),
                        CheckOutDate = c.DateTime(nullable: false),
                        NoOfDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillingId)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .Index(t => t.BookingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Billings", "BookingId", "dbo.Bookings");
            DropIndex("dbo.Billings", new[] { "BookingId" });
            DropTable("dbo.Billings");
        }
    }
}
