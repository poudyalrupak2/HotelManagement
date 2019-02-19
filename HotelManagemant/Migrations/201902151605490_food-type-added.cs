namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foodtypeadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillingItems", "ItemType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillingItems", "ItemType");
        }
    }
}
