namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Billings",
                c => new
                    {
                        BillingId = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemainAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillNo = c.String(),
                        CheckOutDate = c.DateTime(nullable: false),
                        NoOfDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillingId)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        bookingNo = c.String(),
                        CheckIn = c.DateTime(nullable: false),
                        CheckoutDate = c.DateTime(nullable: false),
                        NoOfPersons = c.String(),
                        ShortDescriptions = c.String(),
                        AdvancedAmount = c.Int(),
                    })
                .PrimaryKey(t => t.BookingId);
            
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Address = c.String(),
                        NationalIdNo = c.String(),
                        Nationality = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        RoomNo = c.String(),
                        Capacity = c.Int(nullable: false),
                        NoofBeds = c.Int(nullable: false),
                        Roomtype = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        ImageName = c.String(),
                        Status = c.String(),
                        Hotel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Hotels", t => t.Hotel_Id)
                .Index(t => t.Hotel_Id);
            
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HotelName = c.String(),
                        Type = c.String(),
                        Location = c.String(),
                        Email = c.String(),
                        Ownername = c.String(),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BillingInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TableNo = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BillingItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        BillingInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillingInfoes", t => t.BillingInfoId, cascadeDelete: true)
                .Index(t => t.BillingInfoId);
            
            CreateTable(
                "dbo.Bookingtables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableNo = c.Int(nullable: false),
                        Itemname = c.String(),
                        Type = c.String(),
                        ItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuContinents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContinentName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DrinkCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DrinkRegisters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UnitQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        status = c.Boolean(nullable: false),
                        ImageName = c.String(),
                        DrinkCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DrinkCategories", t => t.DrinkCategoryId, cascadeDelete: true)
                .Index(t => t.DrinkCategoryId);
            
            CreateTable(
                "dbo.DrinkImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        Size = c.Long(nullable: false),
                        Path = c.String(),
                        DrinkCategory_Id = c.Int(),
                        DrinkRegister_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DrinkCategories", t => t.DrinkCategory_Id)
                .ForeignKey("dbo.DrinkRegisters", t => t.DrinkRegister_Id)
                .Index(t => t.DrinkCategory_Id)
                .Index(t => t.DrinkRegister_Id);
            
            CreateTable(
                "dbo.FoodCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Descriprion = c.String(),
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
                        MenuRegistration_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.MenuRegistrations", t => t.MenuRegistration_Id)
                .Index(t => t.MenuRegistration_Id);
            
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
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        randompass = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegisterFoods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitQuintity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ingredients = c.String(),
                        status = c.Boolean(nullable: false),
                        ImageName = c.String(),
                        FoodCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodCategories", t => t.FoodCategoryId, cascadeDelete: true)
                .Index(t => t.FoodCategoryId);
            
            CreateTable(
                "dbo.FoodImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        Size = c.Long(nullable: false),
                        Path = c.String(),
                        FoodCategory_Id = c.Int(),
                        RegisterFood_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodCategories", t => t.FoodCategory_Id)
                .ForeignKey("dbo.RegisterFoods", t => t.RegisterFood_Id)
                .Index(t => t.FoodCategory_Id)
                .Index(t => t.RegisterFood_Id);
            
            CreateTable(
                "dbo.TableRegisters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableNo = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.customersBookings",
                c => new
                    {
                        customers_Id = c.Int(nullable: false),
                        Booking_BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.customers_Id, t.Booking_BookingId })
                .ForeignKey("dbo.customers", t => t.customers_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bookings", t => t.Booking_BookingId, cascadeDelete: true)
                .Index(t => t.customers_Id)
                .Index(t => t.Booking_BookingId);
            
            CreateTable(
                "dbo.RoomBookings",
                c => new
                    {
                        Room_id = c.Int(nullable: false),
                        Booking_BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Room_id, t.Booking_BookingId })
                .ForeignKey("dbo.Rooms", t => t.Room_id, cascadeDelete: true)
                .ForeignKey("dbo.Bookings", t => t.Booking_BookingId, cascadeDelete: true)
                .Index(t => t.Room_id)
                .Index(t => t.Booking_BookingId);
            
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
            
            CreateTable(
                "dbo.FacilitiesRooms",
                c => new
                    {
                        Facilities_Id = c.Int(nullable: false),
                        Room_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Facilities_Id, t.Room_id })
                .ForeignKey("dbo.Facilities", t => t.Facilities_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_id, cascadeDelete: true)
                .Index(t => t.Facilities_Id)
                .Index(t => t.Room_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodImages", "RegisterFood_Id", "dbo.RegisterFoods");
            DropForeignKey("dbo.FoodImages", "FoodCategory_Id", "dbo.FoodCategories");
            DropForeignKey("dbo.RegisterFoods", "FoodCategoryId", "dbo.FoodCategories");
            DropForeignKey("dbo.MenuRegistrations", "MenuContinent_Id", "dbo.MenuContinents");
            DropForeignKey("dbo.MenuRegistrations", "MenuCategory_id", "dbo.MenuCategories");
            DropForeignKey("dbo.Images", "MenuRegistration_Id", "dbo.MenuRegistrations");
            DropForeignKey("dbo.DrinkImages", "DrinkRegister_Id", "dbo.DrinkRegisters");
            DropForeignKey("dbo.DrinkImages", "DrinkCategory_Id", "dbo.DrinkCategories");
            DropForeignKey("dbo.DrinkRegisters", "DrinkCategoryId", "dbo.DrinkCategories");
            DropForeignKey("dbo.BillingItems", "BillingInfoId", "dbo.BillingInfoes");
            DropForeignKey("dbo.Billings", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.FacilitiesRooms", "Room_id", "dbo.Rooms");
            DropForeignKey("dbo.FacilitiesRooms", "Facilities_Id", "dbo.Facilities");
            DropForeignKey("dbo.Rooms", "Hotel_Id", "dbo.Hotels");
            DropForeignKey("dbo.HotelFacilities", "Facilities_Id", "dbo.Facilities");
            DropForeignKey("dbo.HotelFacilities", "Hotel_Id", "dbo.Hotels");
            DropForeignKey("dbo.RoomBookings", "Booking_BookingId", "dbo.Bookings");
            DropForeignKey("dbo.RoomBookings", "Room_id", "dbo.Rooms");
            DropForeignKey("dbo.customersBookings", "Booking_BookingId", "dbo.Bookings");
            DropForeignKey("dbo.customersBookings", "customers_Id", "dbo.customers");
            DropIndex("dbo.FacilitiesRooms", new[] { "Room_id" });
            DropIndex("dbo.FacilitiesRooms", new[] { "Facilities_Id" });
            DropIndex("dbo.HotelFacilities", new[] { "Facilities_Id" });
            DropIndex("dbo.HotelFacilities", new[] { "Hotel_Id" });
            DropIndex("dbo.RoomBookings", new[] { "Booking_BookingId" });
            DropIndex("dbo.RoomBookings", new[] { "Room_id" });
            DropIndex("dbo.customersBookings", new[] { "Booking_BookingId" });
            DropIndex("dbo.customersBookings", new[] { "customers_Id" });
            DropIndex("dbo.FoodImages", new[] { "RegisterFood_Id" });
            DropIndex("dbo.FoodImages", new[] { "FoodCategory_Id" });
            DropIndex("dbo.RegisterFoods", new[] { "FoodCategoryId" });
            DropIndex("dbo.MenuRegistrations", new[] { "MenuContinent_Id" });
            DropIndex("dbo.MenuRegistrations", new[] { "MenuCategory_id" });
            DropIndex("dbo.Images", new[] { "MenuRegistration_Id" });
            DropIndex("dbo.DrinkImages", new[] { "DrinkRegister_Id" });
            DropIndex("dbo.DrinkImages", new[] { "DrinkCategory_Id" });
            DropIndex("dbo.DrinkRegisters", new[] { "DrinkCategoryId" });
            DropIndex("dbo.BillingItems", new[] { "BillingInfoId" });
            DropIndex("dbo.Rooms", new[] { "Hotel_Id" });
            DropIndex("dbo.Billings", new[] { "BookingId" });
            DropTable("dbo.FacilitiesRooms");
            DropTable("dbo.HotelFacilities");
            DropTable("dbo.RoomBookings");
            DropTable("dbo.customersBookings");
            DropTable("dbo.TableRegisters");
            DropTable("dbo.FoodImages");
            DropTable("dbo.RegisterFoods");
            DropTable("dbo.Logins");
            DropTable("dbo.MenuCategories");
            DropTable("dbo.MenuRegistrations");
            DropTable("dbo.Images");
            DropTable("dbo.FoodCategories");
            DropTable("dbo.DrinkImages");
            DropTable("dbo.DrinkRegisters");
            DropTable("dbo.DrinkCategories");
            DropTable("dbo.MenuContinents");
            DropTable("dbo.Bookingtables");
            DropTable("dbo.BillingItems");
            DropTable("dbo.BillingInfoes");
            DropTable("dbo.Hotels");
            DropTable("dbo.Facilities");
            DropTable("dbo.Rooms");
            DropTable("dbo.customers");
            DropTable("dbo.Bookings");
            DropTable("dbo.Billings");
        }
    }
}
