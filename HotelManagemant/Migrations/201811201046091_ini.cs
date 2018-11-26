namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Facilities", "Room_id", "dbo.Rooms");
            DropIndex("dbo.Facilities", new[] { "Room_id" });
            CreateTable(
                "dbo.FacilitiesRooms",
                c => new
                    {
                        Facilities_Id = c.Int(nullable: false),
                        Room_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Facilities_Id, t.Room_id })
                .ForeignKey("dbo.Facilities", t => t.Facilities_Id, cascadeDelete: false)
                .ForeignKey("dbo.Rooms", t => t.Room_id, cascadeDelete:false)
                .Index(t => t.Facilities_Id)
                .Index(t => t.Room_id);
            
            DropColumn("dbo.Facilities", "Room_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Facilities", "Room_id", c => c.Int());
            DropForeignKey("dbo.FacilitiesRooms", "Room_id", "dbo.Rooms");
            DropForeignKey("dbo.FacilitiesRooms", "Facilities_Id", "dbo.Facilities");
            DropIndex("dbo.FacilitiesRooms", new[] { "Room_id" });
            DropIndex("dbo.FacilitiesRooms", new[] { "Facilities_Id" });
            DropTable("dbo.FacilitiesRooms");
            CreateIndex("dbo.Facilities", "Room_id");
            AddForeignKey("dbo.Facilities", "Room_id", "dbo.Rooms", "id");
        }
    }
}
