namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingNoAsForeignKey21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "name", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "name");
        }
    }
}
