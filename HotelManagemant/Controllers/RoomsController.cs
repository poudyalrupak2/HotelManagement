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
    public class RoomsController : Controller
    {
        private HotelDbContext db = new HotelDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            return View(db.rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            roomViewModel room = new roomViewModel();
            room.Facilities = db.facilities.ToList();
            return View(room);
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(roomViewModel room, HttpPostedFileBase Image)
        {
         if (ModelState.IsValid)
            {
                WebImage img = new WebImage(Image.InputStream);
                if (Image!= null)
                {
                    string fileName1 = Image.FileName;
                    string filename1 = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extension1 = Path.GetExtension(Image.FileName);
                    filename1 = room.RoomNo + extension1;
                    room.ImageName = "/images/room/" + filename1;
                    if (img.Width <1000||img.Width>1000)
                        img.Resize(903, 631);
                    filename1 = Path.Combine(Server.MapPath("~/Images/room/"), filename1);
                    img.Save(filename1);
                }
                var facilites = room.facilitiesId;
                ICollection<Facilities> objFacilities = new List<Facilities>();
                foreach (var item in facilites)
                {
                    int id = Convert.ToInt32(item);
                    Facilities objfaci = db.facilities.Find(id);
                    objFacilities.Add(objfaci);
                }
                db.rooms.Add(new Room
                {
                    Facilities = objFacilities,
                    Capacity = room.Capacity,
                    Description = room.Description,
                    Image = room.Image,
                    ImageName = room.ImageName,
                    NoofBeds = room.NoofBeds,
                    Price = room.Price,
                    RoomNo = room.RoomNo,
                    Roomtype = room.Roomtype,
                    Status = room.Status
                    

                });
               // db.rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,RoomNo,Capacity,NoofBeds,Roomtype,Price,Description,ImageName,Status")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.rooms.Find(id);
            db.rooms.Remove(room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult book()
        { 
            //bookViewModel objbook = new bookViewModel();
            //objbook.roomId = roomId;
            //objbook.CheckIn = DateTime.Now;
            //objbook.CheckoutDate = DateTime.Now;
            //objbook.roomNo = "abc";
            List<customerInfo> lstCustomers = new List<customerInfo>();
            lstCustomers.Add(new customerInfo
            {
                Id = 0,
                CustomerName = "",
                Address = "",
                NationalIdNo = "",
                Nationality = ""
            });
           // objbook.customers = lstCustomers;
            return View(lstCustomers);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult book(List<customerInfo> book)
        {
            if (ModelState.IsValid)
            {
                    
            }
            return View();
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
