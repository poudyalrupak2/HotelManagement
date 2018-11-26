namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelFacilities",
                c => new
                    {
                        Hotel_Id = c.Int(nullable: false),
                        Facilities_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Hotel_Id, t.Facilities_Id })
                .ForeignKey("dbo.Hotels", t => t.Hotel_Id, cascadeDelete: true)
                .ForeignKey("dbo.Facilities", t => t.Facilities_Id, cascadeDelete: true)
                .Index(t => t.Hotel_Id)
                .Index(t => t.Facilities_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HotelFacilities", "Facilities_Id", "dbo.Facilities");
            DropForeignKey("dbo.HotelFacilities", "Hotel_Id", "dbo.Hotels");
            DropIndex("dbo.HotelFacilities", new[] { "Facilities_Id" });
            DropIndex("dbo.HotelFacilities", new[] { "Hotel_Id" });
            DropTable("dbo.HotelFacilities");
        }
    }
}
