using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HotelManagemant.Data;
using HotelManagemant.Models;
using HotelManagemant.ViewModels;

namespace HotelManagemant.Controllers
{
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
                WebImage img = new WebImage(Image.InputStream);
                if (Image != null)
                {
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
                foreach (var item in facilites)
                {
                    int id = Convert.ToInt32(item);
                    Facilities objfaci = db.facilities.Find(id);
                    objFacilities.Add(objfaci);
                }
                db.hotels.Add(new Hotel
                {
                    Facilities = objFacilities,
                    Email = hotel.Email,
                   ImageName=hotel.ImageName,
                   HotelName=hotel.HotelName,
                   Location=hotel.Location,
                   Ownername=hotel.Ownername
                });
                // db.rooms.Add(room);
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
        public ActionResult Edit([Bind(Include = "Id,HotelName,Type,Location,Email,Ownername,ImageName")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
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
