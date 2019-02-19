namespace HotelManagemant.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<HotelManagemant.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HotelManagemant.Data.Context context)
        {
            context.Login.AddOrUpdate(x => x.Id, new Models.Login()
            {
                Id = 1,
                Email = "dbugtest2016@gmail.com",
                Password = Crypto.Hash("nepalnepal"),
                Category="SuperAdmin"

            });




        }
    }
}
