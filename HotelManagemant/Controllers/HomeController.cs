using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagemant.Models;
using HotelManagemant.Data;
using HotelManagemant.Filters;

namespace HotelManagemant.Controllers
{
    [SessionCheck]
    [Authorize(Roles = "Admin")]

    public class HomeController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            var objHotel = db.hotels.FirstOrDefault();
            return View(objHotel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}