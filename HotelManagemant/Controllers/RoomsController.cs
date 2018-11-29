using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IdentityModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HotelManagemant.Data;
using HotelManagemant.Models;
using HotelManagemant.ViewModels;
using System.Transactions;


namespace HotelManagemant.Controllers
{
    public class RoomsController : Controller
    {
        private Context db = new Context();

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

            return View(db.rooms.ToList());

        }

        public ActionResult bookRoom(List<int> id)
        {
            List<bookViewModel> objBook = new List<bookViewModel>();
            string bookingno = "";
            if (db.bookings.ToList().Count != 0)
            {
                bookingno = db.bookings.OrderByDescending(m => m.BookingId).FirstOrDefault().bookingNo;
            }
          
            if (bookingno == ""||bookingno==null)
            {
                bookingno = "Dbug2000";
            }
            else
            {
                var num = Regex.Matches(bookingno, @"\D+|\d+")
              .Cast<Match>()
              .Select(m => m.Value).ToList();
                int num1 = Convert.ToInt32(num[1]) + 1;
                bookingno = "DBUG" + num1;

            }
            foreach (int item in id)
            {
                Room objRoom = db.rooms.Find(item);
                bookViewModel objbookVM = new bookViewModel();
                objbookVM.roomId = item;
                objbookVM.roomNo = objRoom.RoomNo;
                objbookVM.bookingno = bookingno;
                objBook.Add(objbookVM);
            }
            return View(objBook);
        }
        [HttpPost]
        public ActionResult bookRoom(List<bookViewModel> book)
        {
            Booking objBook = new Booking();
            List<customers> lstCustomer = new List<customers>();
            foreach (var item in book)
            {
                Room room = db.rooms.Find(item.roomId);

                customers objCustomer = db.Customers.FirstOrDefault(m => m.CustomerName == item.CustomerName && m.Address == item.Address && m.NationalIdNo == item.NationalIdNo && m.Nationality == item.Nationality);

                if (objCustomer == null)
                {
                    db.Customers.Add(new customers
                    {
                        Address = item.Address,
                        CustomerName = item.CustomerName,
                        NationalIdNo = item.NationalIdNo,
                        Nationality = item.Nationality

                    });
                    //db.Customers.Add(objCustomer);
                    db.SaveChanges();
                    objCustomer = db.Customers.FirstOrDefault(m => m.CustomerName == item.CustomerName && m.Address == item.Address && m.NationalIdNo == item.NationalIdNo && m.Nationality == item.Nationality);
                }
            }

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
            {
                Booking booking = new Booking();
                booking.roomId = book[0].roomId;
                booking.bookingNo = book[0].bookingno;
                booking.CheckIn = book[0].CheckIn;
                booking.CheckoutDate = book[0].CheckoutDate;
                booking.customers = lstCustomer;
                booking.NoOfPersons = book[0].NoOfPersons;
                booking.ShortDescriptions = book[0].ShortDescriptions;


                db.bookings.Add(booking);
                db.SaveChanges();

                scope.Complete();


            }




            return RedirectToAction("book");
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
