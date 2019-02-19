namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unitremoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DrinkRegisters", "UnitQuantity");
            DropColumn("dbo.RegisterFoods", "UnitQuintity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisterFoods", "UnitQuintity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DrinkRegisters", "UnitQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
