using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HotelManagemant.Data;
using HotelManagemant.Models;

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
                    // FormsAuthentication.SetAuthCookie(l.Email);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        var objAdmin = db.Login.FirstOrDefault(a => (a.Email == l.Email && a.Password == l.Password));
                        if (objAdmin.Category == "Admin")
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return Redirect(ReturnUrl);
                        }
                    }
                    else
                    {
                        Session.Add("id", Admin.Id);
                        Session.Add("userEmail", Admin.Email);
                        Session.Add("category", Admin.Category);
                        var objAdmin = db.Login.FirstOrDefault(a => (a.Email == l.Email && a.Password == pass));
                            return RedirectToAction("Index", "Rooms");
                        }

                        //  return RedirectToAction("Index", "CustomerSuperAdmins");
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

        //    var Admin = _db.Login.FirstOrDefault(a => (a.Email));
        //                    if (Admin != null)
        //                    {
        //                               // var pass = Crypto.Hash(l.Password);
        //                                if (string.Compare(pass, Admin.Password) == 0)
        //                                {

        //                                    FormsAuthentication.SetAuthCookie(l.UserName, l.RememberMe);
        //                                    if (Url.IsLocalUrl(ReturnUrl))
        //                                    {
        //                                        // var objAdmin = _db.tblAdmins.FirstOrDefault(a=>(a.UserName == l.UserName || a.Email == l.UserName) && a.Password == l.Password);

        //                                        return Redirect(ReturnUrl);
        //}
        //                                    else
        //                                    {
        //                                        Session.Add("id", Admin.Id);
        //                                        Session.Add("userName", Admin.UserName);
        //                                        Session.Add("userEmail", Admin.Email);
        //                                        Session.Add("FullName", Admin.FullName);
        //                                        Session.Add("Category", Admin.Category);
        //                                        return RedirectToAction("Index", "DashBoard");
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                ModelState.AddModelError("", "Invalid User");
        //                            }
        //                        }
        //                        return View();
        //                    }
        //                }
        //            }
        //                [Authorize]
        //        //        public ActionResult Logout()
        //        //        {

        //        //            Session.Abandon();
        //        //            return RedirectToAction("Login", "Account");
        //        //        }
        //        //        public ActionResult ForgetPassword()
        //        //        {
        //        //            return View();
        //        //        }
        //        //        [HttpPost]

        //        //        public ActionResult ForgetPassword(Admin admin)
        //        //        {

        //        //            if (ModelState.IsValid)
        //        //            {
        //        //                //https://www.google.com/settings/security/lesssecureapps
        //        //                //Make Access for less secure apps=true

        //        //                string from = "dbugtest2016@gmail.com";
        //        //                string fromPassword = "my@test#";
        //        //                string password = Membership.GeneratePassword(6, 1);

        //        //                if (admin.Email != null)
        //        //                {
        //                    using (MailMessage mail = new MailMessage(from, admin.Email))
        //                    {

        //                        try
        //                        {
        //                            var tb = _db.Admin.Where(u => u.Email == admin.Email).FirstOrDefault();

        //                            if (tb == null)
        //                            {
        //                                ViewBag.Message = "email Doesnot Exist Please enter valid email";
        //                            }
        //                            else
        //                            {
        //                                mail.Subject = "Password Recovery";
        //                                mail.Body = "Use this Password to login:" + password;

        //                                mail.IsBodyHtml = false;
        //                                SmtpClient smtp = new SmtpClient();
        //                                smtp.Host = "smtp.gmail.com";
        //                                smtp.EnableSsl = true;
        //                                smtp.UseDefaultCredentials = false;
        //                                NetworkCredential networkCredential = new NetworkCredential(from, fromPassword);

        //                                smtp.Credentials = networkCredential;
        //                                smtp.Port = 587;
        //                                smtp.Send(mail);
        //                                ViewBag.Message = "Your Password Is Sent to your email";
        //                                var query = (from q in _db.Admin
        //                                             where q.Email == admin.Email
        //                                             select q).First();
        //                                query.randompass = password;
        //                                _db.SaveChanges();

        //                            }
        //                        }
        //                        catch
        //                        {
        //                            return RedirectToAction("errorpage");
        //                        }
        //                        TempData["Message"] = admin.Email;
        //                        TempData.Keep();
        //                        return RedirectToAction("conformation");


        //                    }
        //                }


        //            }


        //            return View();


        //            //return RedirectToAction("Index", "Home");
        //        }
        //        public ActionResult conformation()
        //        {
        //            if (TempData["Message"] != null)
        //            {
        //                string message = TempData["Message"].ToString();
        //                TempData.Keep();
        //                if (message != null)
        //                {
        //                    return PartialView();
        //                }
        //            }
        //            return RedirectToAction("ForgetPassword");

        //        }
        //        [HttpPost]
        //        public ActionResult conformation(Admin admin)
        //        {
        //            if (ModelState.IsValid)
        //            {

        //                string message = TempData["Message"].ToString();
        //                TempData.Keep();
        //                TempData["info"] = (_db.Admin.Any(u => u.Email == message && u.randompass == admin.randompass));
        //                if (admin.randompass != null)
        //                {
        //                    if (_db.Admin.Any(u => u.Email == message && u.randompass == admin.randompass))
        //                    {
        //                        return RedirectToAction("NewPassword");
        //                    }
        //                }
        //                return PartialView();

        //            }
        //            ViewBag.message = "conformation code donot match";
        //            return PartialView();
        //        }
        public ActionResult NewPassword()
        {

            return PartialView();

        }
        //[HttpPost]
        //public ActionResult NewPassword(PasswordConform pass)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string message = TempData["message"].ToString();
        //        var query = (from q in _db.Login
        //                     where q.Email == message
        //                     select q).First();
        //        string password = pass.Password;
        //        query.Password = Crypto.Hash(password);
        //        query.randompass = null;
        //        _db.SaveChanges();
        //        return RedirectToAction("Login");



        //    }
        //    return PartialView();
        //}
        //public ActionResult errorpage()
        //{
        //    return PartialView();
        //}
        public ActionResult Logout()
        {

            var Sesson = Session["userEmail"].ToString();
            string category = db.Login.Where(t => t.Email == Sesson).Select(t => t.Category).FirstOrDefault();
            if (category != null)
            {
                Session.Abandon();
                return RedirectToAction("Login");
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login");
            }
        }
        //public ActionResult ActivityLog()
        //{
        //    var count = _db.ActivityLogs.OrderByDescending(u => u.Id);
        //    return View(count.ToList());

        //}

    }
}