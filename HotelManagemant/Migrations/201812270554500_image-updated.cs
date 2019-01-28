namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageupdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Image_ImageId", "dbo.Images");
            DropIndex("dbo.Images", new[] { "Image_ImageId" });
            DropColumn("dbo.Images", "TradingGoodsId");
            DropColumn("dbo.Images", "Image_ImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Image_ImageId", c => c.Int());
            AddColumn("dbo.Images", "TradingGoodsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "Image_ImageId");
            AddForeignKey("dbo.Images", "Image_ImageId", "dbo.Images", "ImageId");
        }
    }
}
