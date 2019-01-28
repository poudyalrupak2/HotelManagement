using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HotelManagemant.Data;
using HotelManagemant.Filters;
using HotelManagemant.Models;
using HotelManagemant.ViewModels;

namespace HotelManagemant.Controllers
{
    [SessionCheck]
    [Authorize(Roles = "Superadmin")]

    public class HotelsController : Controller
    {
        private Context db = new Context();

        // GET: Hotels
        public ActionResult Index()
        {
            return View(db.hotels.ToList());
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        public ActionResult Create()
        {

            HotelViewModel room = new HotelViewModel();
            room.Facilities = db.facilities.ToList();
            return View(room);
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelViewModel hotel, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {

                int data = db.Login.Where(t => t.Email == hotel.Email).Count();
                if(data>0)
                {
                    ModelState.AddModelError("", "Email already exists");

                }
               else 
                { 
                if (Image != null)
                {
                        WebImage img = new WebImage(Image.InputStream);

                        string fileName1 = Image.FileName;
                    string filename1 = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extension1 = Path.GetExtension(Image.FileName);
                    filename1 = hotel.HotelName + extension1;
                    hotel.ImageName = "/images/Hotel/" + filename1;
                    if (img.Width < 1000 || img.Width > 1000)
                        img.Resize(903, 631);
                    filename1 = Path.Combine(Server.MapPath("~/Images/Hotel/"), filename1);
                    img.Save(filename1);
                }
                var facilites = hotel.FacilitiesId;
                ICollection<Facilities> objFacilities = new List<Facilities>();
                    if (facilites != null)
                    {
                        foreach (var item in facilites)
                        {
                            int id = Convert.ToInt32(item);
                            Facilities objfaci = db.facilities.Find(id);
                            objFacilities.Add(objfaci);
                        }
                    }
                db.hotels.Add(new Hotel
                {
                    Facilities = objFacilities,
                    Email = hotel.Email,
                    ImageName = hotel.ImageName,
                    HotelName = hotel.HotelName,
                    Location = hotel.Location,
                    Ownername = hotel.Ownername
                });

                Random generator = new Random();
                String password = generator.Next(0, 999999).ToString("D6");



                var message = new MailMessage();
                message.To.Add(new MailAddress(hotel.Email));
                message.Subject = "Change Password";
                message.Body = "Use this Password to login:" + password;
                    using (var smtp = new SmtpClient())
                    {
                        try
                        {

                            smtp.Send(message);
                            db.Login.Add(new Login
                            {
                                Email = hotel.Email,
                                Category = "Admin",
                                randompass = password
                            });

                            db.SaveChanges();
                            TempData["Message"] = "Hotel Created Successfully.";


                        }
                        catch (Exception e)
                        {
                            return RedirectToAction("Error");
                        }
                    }
                }
                    db.SaveChanges();
                    return RedirectToAction("Index", "Hotels");
                
            }
            hotel.Facilities = db.facilities.ToList();

            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HotelName,Type,Location,Email,Ownername,Image")] Hotel hotel, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    WebImage img = new WebImage(Image.InputStream);

                    string firstpath = "/Images/";
                    string subPath = "/images/Hotel/"; // your code goes here
                    bool imageexist = System.IO.Directory.Exists(Server.MapPath(firstpath));
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                    if (!imageexist)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(firstpath));
                    }
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                    }
                    string fileName1 = Image.FileName;
                    string filename1 = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extension1 = Path.GetExtension(Image.FileName);
                    filename1 = hotel.HotelName + extension1;
                    hotel.ImageName = "/images/Hotel/" + filename1;
                    if (img.Width < 1000 || img.Width > 1000)
                        img.Resize(903, 631);
                    filename1 = Path.Combine(Server.MapPath("/images/Hotel/"), filename1);
                    img.Save(filename1);
                }


                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Hotels Edited Successfully.";

                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.hotels.Find(id);
            db.hotels.Remove(hotel);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
