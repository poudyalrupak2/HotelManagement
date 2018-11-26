namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Facilities", "Hotel_Id", "dbo.Hotels");
            DropIndex("dbo.Facilities", new[] { "Hotel_Id" });
            DropColumn("dbo.Facilities", "Hotel_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Facilities", "Hotel_Id", c => c.Int());
            CreateIndex("dbo.Facilities", "Hotel_Id");
            AddForeignKey("dbo.Facilities", "Hotel_Id", "dbo.Hotels", "Id");
        }
    }
}
