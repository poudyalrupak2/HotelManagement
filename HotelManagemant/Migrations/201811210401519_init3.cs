namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Status", c => c.Boolean(nullable: false));
        }
    }
}
