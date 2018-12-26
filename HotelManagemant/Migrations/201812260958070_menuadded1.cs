namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class menuadded1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuContinents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContinentName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        Size = c.Long(nullable: false),
                        Path = c.String(),
                        TradingGoodsId = c.Int(nullable: false),
                        Image_ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Images", t => t.Image_ImageId)
                .Index(t => t.Image_ImageId);
            
            CreateTable(
                "dbo.MenuCategories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Detail = c.String(),
                        thumbnail = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Image_ImageId", "dbo.Images");
            DropIndex("dbo.Images", new[] { "Image_ImageId" });
            DropTable("dbo.MenuCategories");
            DropTable("dbo.Images");
            DropTable("dbo.MenuContinents");
        }
    }
}
