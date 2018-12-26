namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class advanceamountadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "AdvancedAmount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "AdvancedAmount");
        }
    }
}
