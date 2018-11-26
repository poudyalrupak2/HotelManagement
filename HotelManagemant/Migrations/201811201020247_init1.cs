namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "ImageName");
        }
    }
}
