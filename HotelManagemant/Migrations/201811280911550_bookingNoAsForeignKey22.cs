namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingNoAsForeignKey22 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookings", "name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "name", c => c.Int(nullable: false));
        }
    }
}
