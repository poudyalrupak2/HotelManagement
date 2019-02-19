namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Billings", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Billings", "CreatedBy", c => c.String());
            AddColumn("dbo.Billings", "EditedAt", c => c.DateTime());
            AddColumn("dbo.Billings", "EditedBy", c => c.String());
            AddColumn("dbo.Bookings", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Bookings", "CreatedBy", c => c.String());
            AddColumn("dbo.Bookings", "EditedAt", c => c.DateTime());
            AddColumn("dbo.Bookings", "EditedBy", c => c.String());
            AddColumn("dbo.customers", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.customers", "CreatedBy", c => c.String());
            AddColumn("dbo.customers", "EditedAt", c => c.DateTime());
            AddColumn("dbo.customers", "EditedBy", c => c.String());
            AddColumn("dbo.Rooms", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Rooms", "CreatedBy", c => c.String());
            AddColumn("dbo.Rooms", "EditedAt", c => c.DateTime());
            AddColumn("dbo.Rooms", "EditedBy", c => c.String());
            AddColumn("dbo.Facilities", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Facilities", "CreatedBy", c => c.String());
            AddColumn("dbo.Facilities", "EditedAt", c => c.DateTime());
            AddColumn("dbo.Facilities", "EditedBy", c => c.String());
            AddColumn("dbo.Hotels", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Hotels", "CreatedBy", c => c.String());
            AddColumn("dbo.Hotels", "EditedAt", c => c.DateTime());
            AddColumn("dbo.Hotels", "EditedBy", c => c.String());
            AddColumn("dbo.BillingInfoes", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.BillingInfoes", "CreatedBy", c => c.String());
            AddColumn("dbo.BillingInfoes", "EditedAt", c => c.DateTime());
            AddColumn("dbo.BillingInfoes", "EditedBy", c => c.String());
            AddColumn("dbo.BillingItems", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.BillingItems", "CreatedBy", c => c.String());
            AddColumn("dbo.BillingItems", "EditedAt", c => c.DateTime());
            AddColumn("dbo.BillingItems", "EditedBy", c => c.String());
            AddColumn("dbo.Bookingtables", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Bookingtables", "CreatedBy", c => c.String());
            AddColumn("dbo.Bookingtables", "EditedAt", c => c.DateTime());
            AddColumn("dbo.Bookingtables", "EditedBy", c => c.String());
            AddColumn("dbo.DrinkCategories", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.DrinkCategories", "CreatedBy", c => c.String());
            AddColumn("dbo.DrinkCategories", "EditedAt", c => c.DateTime());
            AddColumn("dbo.DrinkCategories", "EditedBy", c => c.String());
            AddColumn("dbo.DrinkRegisters", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.DrinkRegisters", "CreatedBy", c => c.String());
            AddColumn("dbo.DrinkRegisters", "EditedAt", c => c.DateTime());
            AddColumn("dbo.DrinkRegisters", "EditedBy", c => c.String());
            AddColumn("dbo.FoodCategories", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.FoodCategories", "CreatedBy", c => c.String());
            AddColumn("dbo.FoodCategories", "EditedAt", c => c.DateTime());
            AddColumn("dbo.FoodCategories", "EditedBy", c => c.String());
            AddColumn("dbo.MenuCategories", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.MenuCategories", "CreatedBy", c => c.String());
            AddColumn("dbo.MenuCategories", "EditedAt", c => c.DateTime());
            AddColumn("dbo.MenuCategories", "EditedBy", c => c.String());
            AddColumn("dbo.RegisterFoods", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.RegisterFoods", "CreatedBy", c => c.String());
            AddColumn("dbo.RegisterFoods", "EditedAt", c => c.DateTime());
            AddColumn("dbo.RegisterFoods", "EditedBy", c => c.String());
            AddColumn("dbo.FoodImages", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.FoodImages", "CreatedBy", c => c.String());
            AddColumn("dbo.FoodImages", "EditedAt", c => c.DateTime());
            AddColumn("dbo.FoodImages", "EditedBy", c => c.String());
            AddColumn("dbo.TableRegisters", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.TableRegisters", "CreatedBy", c => c.String());
            AddColumn("dbo.TableRegisters", "EditedAt", c => c.DateTime());
            AddColumn("dbo.TableRegisters", "EditedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TableRegisters", "EditedBy");
            DropColumn("dbo.TableRegisters", "EditedAt");
            DropColumn("dbo.TableRegisters", "CreatedBy");
            DropColumn("dbo.TableRegisters", "CreatedAt");
            DropColumn("dbo.FoodImages", "EditedBy");
            DropColumn("dbo.FoodImages", "EditedAt");
            DropColumn("dbo.FoodImages", "CreatedBy");
            DropColumn("dbo.FoodImages", "CreatedAt");
            DropColumn("dbo.RegisterFoods", "EditedBy");
            DropColumn("dbo.RegisterFoods", "EditedAt");
            DropColumn("dbo.RegisterFoods", "CreatedBy");
            DropColumn("dbo.RegisterFoods", "CreatedAt");
            DropColumn("dbo.MenuCategories", "EditedBy");
            DropColumn("dbo.MenuCategories", "EditedAt");
            DropColumn("dbo.MenuCategories", "CreatedBy");
            DropColumn("dbo.MenuCategories", "CreatedAt");
            DropColumn("dbo.FoodCategories", "EditedBy");
            DropColumn("dbo.FoodCategories", "EditedAt");
            DropColumn("dbo.FoodCategories", "CreatedBy");
            DropColumn("dbo.FoodCategories", "CreatedAt");
            DropColumn("dbo.DrinkRegisters", "EditedBy");
            DropColumn("dbo.DrinkRegisters", "EditedAt");
            DropColumn("dbo.DrinkRegisters", "CreatedBy");
            DropColumn("dbo.DrinkRegisters", "CreatedAt");
            DropColumn("dbo.DrinkCategories", "EditedBy");
            DropColumn("dbo.DrinkCategories", "EditedAt");
            DropColumn("dbo.DrinkCategories", "CreatedBy");
            DropColumn("dbo.DrinkCategories", "CreatedAt");
            DropColumn("dbo.Bookingtables", "EditedBy");
            DropColumn("dbo.Bookingtables", "EditedAt");
            DropColumn("dbo.Bookingtables", "CreatedBy");
            DropColumn("dbo.Bookingtables", "CreatedAt");
            DropColumn("dbo.BillingItems", "EditedBy");
            DropColumn("dbo.BillingItems", "EditedAt");
            DropColumn("dbo.BillingItems", "CreatedBy");
            DropColumn("dbo.BillingItems", "CreatedAt");
            DropColumn("dbo.BillingInfoes", "EditedBy");
            DropColumn("dbo.BillingInfoes", "EditedAt");
            DropColumn("dbo.BillingInfoes", "CreatedBy");
            DropColumn("dbo.BillingInfoes", "CreatedAt");
            DropColumn("dbo.Hotels", "EditedBy");
            DropColumn("dbo.Hotels", "EditedAt");
            DropColumn("dbo.Hotels", "CreatedBy");
            DropColumn("dbo.Hotels", "CreatedAt");
            DropColumn("dbo.Facilities", "EditedBy");
            DropColumn("dbo.Facilities", "EditedAt");
            DropColumn("dbo.Facilities", "CreatedBy");
            DropColumn("dbo.Facilities", "CreatedAt");
            DropColumn("dbo.Rooms", "EditedBy");
            DropColumn("dbo.Rooms", "EditedAt");
            DropColumn("dbo.Rooms", "CreatedBy");
            DropColumn("dbo.Rooms", "CreatedAt");
            DropColumn("dbo.customers", "EditedBy");
            DropColumn("dbo.customers", "EditedAt");
            DropColumn("dbo.customers", "CreatedBy");
            DropColumn("dbo.customers", "CreatedAt");
            DropColumn("dbo.Bookings", "EditedBy");
            DropColumn("dbo.Bookings", "EditedAt");
            DropColumn("dbo.Bookings", "CreatedBy");
            DropColumn("dbo.Bookings", "CreatedAt");
            DropColumn("dbo.Billings", "EditedBy");
            DropColumn("dbo.Billings", "EditedAt");
            DropColumn("dbo.Billings", "CreatedBy");
            DropColumn("dbo.Billings", "CreatedAt");
        }
    }
}
