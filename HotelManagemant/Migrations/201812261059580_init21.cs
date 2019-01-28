namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuRegistrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Itemname = c.String(),
                        Ingrident = c.String(),
                        Price = c.String(),
                        Thumbnail = c.String(),
                        MenuCategory_id = c.Int(),
                        MenuContinent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuCategories", t => t.MenuCategory_id)
                .ForeignKey("dbo.MenuContinents", t => t.MenuContinent_Id)
                .Index(t => t.MenuCategory_id)
                .Index(t => t.MenuContinent_Id);
            
            AddColumn("dbo.Images", "MenuRegistration_Id", c => c.Int());
            CreateIndex("dbo.Images", "MenuRegistration_Id");
            AddForeignKey("dbo.Images", "MenuRegistration_Id", "dbo.MenuRegistrations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuRegistrations", "MenuContinent_Id", "dbo.MenuContinents");
            DropForeignKey("dbo.MenuRegistrations", "MenuCategory_id", "dbo.MenuCategories");
            DropForeignKey("dbo.Images", "MenuRegistration_Id", "dbo.MenuRegistrations");
            DropIndex("dbo.MenuRegistrations", new[] { "MenuContinent_Id" });
            DropIndex("dbo.MenuRegistrations", new[] { "MenuCategory_id" });
            DropIndex("dbo.Images", new[] { "MenuRegistration_Id" });
            DropColumn("dbo.Images", "MenuRegistration_Id");
            DropTable("dbo.MenuRegistrations");
        }
    }
}
