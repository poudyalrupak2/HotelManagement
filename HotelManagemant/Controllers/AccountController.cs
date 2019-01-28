using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using HotelManagemant.Data;
using HotelManagemant.Models;
using HotelManagemant.ViewModels;

namespace HotelManagemant.Controllers
{
    public class AccountController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login l, string ReturnUrl = "")
        {


            var Admin = db.Login.FirstOrDefault(a => (a.Email == l.Email));

            if (Admin != null)
            {
                string pass = Crypto.Hash(l.Password);
                if (string.Compare(pass, Admin.Password) == 0)
                {
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        var objAdmin = db.Login.FirstOrDefault(a => (a.Email == l.Email && a.Password == l.Password));
                        if (objAdmin.Category == "Admin")
                        {
                            FormsAuthentication.SetAuthCookie(l.Email, false);

                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(l.Email, false);


                            return Redirect(ReturnUrl);
                        }
                    }
                    else
                    {
                        Session.Add("id", Admin.Id);
                        Session.Add("userEmail", Admin.Email);
                        Session.Add("category", Admin.Category);
                        string img = db.hotels.Where(a => a.Email == l.Email).FirstOrDefault().ImageName;
                        Session.Add("Image", img);
                        var objAdmin = db.Login.FirstOrDefault(a => (a.Email == l.Email && a.Password == pass));
                        
                        FormsAuthentication.SetAuthCookie(l.Email, false);
                        string[] roles = Roles.GetRolesForUser(Admin.Email);
                        if (roles.Contains("Superadmin"))
                        {
                            return RedirectToAction("Index", "Rooms");

                        }
                        if (roles.Contains("Admin"))
                        {

                            return RedirectToAction("Index", "Home");
                        }
                        }

                    }
                

                else if (l.Password == Admin.randompass)
                {
                    TempData["message"] = Admin.Email;


                    return RedirectToAction("NewPassword");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User");
                }
            }
            return View();

        }

        public ActionResult NewPassword()
        {

            return PartialView();

        }
        [HttpPost]
        public ActionResult NewPassword(PasswordConform pass)
        {
            if (ModelState.IsValid)
            {
                string message = TempData["message"].ToString();
                var query = (from q in db.Login
                             where q.Email == message
                             select q).First();
                string password = pass.Password;
                query.Password = Crypto.Hash(password);
                query.randompass = null;
                db.SaveChanges();
                return RedirectToAction("Login");



            }
            return PartialView();
        }
        public ActionResult Logout()
        {

            var Sesson = Session["userEmail"].ToString();
            string category = db.Login.Where(t => t.Email == Sesson).Select(t => t.Category).FirstOrDefault();
            if (category != null)
            {
                FormsAuthentication.SignOut();

                Session.Abandon();
                return RedirectToAction("Login");
            }
            else
            {
                FormsAuthentication.SignOut();

                Session.Abandon();
                return RedirectToAction("Login");
            }
        }
      
    }
}