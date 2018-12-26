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
                if (Image != null)
                {
                    WebImage img = new WebImage(Image.InputStream);
                
                    string firstpath = "/Images/";
                    string subPath = "/Images/Rooms/"; // your code goes here
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
                    filename1 = room.RoomNo + extension1;
                    room.ImageName = "/images/room/" + filename1;
                    if (img.Width < 1000 || img.Width > 1000)
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
                    Status = "Available"
                });
                // db.rooms.Add(room);
                db.SaveChanges();

                TempData["Message"] = "Room Created Successfully.";

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
        public ActionResult Edit([Bind(Include = "id,RoomNo,Capacity,NoofBeds,Roomtype,Price,Description,ImageName,Status")] Room room, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    string firstpath = "/Images/";
                    string subPath = "/Images/Rooms/"; // your code goes here
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
                    string fileName1 =Image.FileName;
                    string filename1 = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extension1 = Path.GetExtension(Image.FileName);
                    filename1 = room.RoomNo + extension1;
                  room.ImageName= "/images/Rooms/" + filename1;
                    filename1 = Path.Combine(Server.MapPath("~/Images/Rooms/"), filename1);
                    Image.SaveAs(filename1);
                }

                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Message"] = "Room Updated Successfully.";
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



        public ActionResult ChangeStatus(int id)
        {
            ChangeStatusViewModel objStatus = new ChangeStatusViewModel();
            objStatus.RoomStatus=new List<string> { "Maintenance", "Available" };
            Room objRoom = db.rooms.Find(id);
            objStatus.RoomId = objRoom.id;
            objStatus.RoomNo = objRoom.RoomNo;
            objStatus.Status = objRoom.Status;
            return View(objStatus);
        }

        [HttpPost]
        public ActionResult ChangeStatus(ChangeStatusViewModel changeStatus)
        {
            Room objRoom = db.rooms.Find(changeStatus.RoomId);
            objRoom.Status = changeStatus.Status;
            db.SaveChanges();   
            TempData["Message"] = "Status Changed Successfully.";
            return RedirectToAction("Book");
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

            if (bookingno == "" || bookingno == null)
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
                objbookVM.Price = objRoom.Price;
                objBook.Add(objbookVM);
            }
            return View(objBook);
        }
        [HttpPost]
        public ActionResult bookRoom(List<bookViewModel> book)
        {
            Booking objBook = new Booking();
            List<customers> lstCustomer = new List<customers>();
            List<Room> objRoom = new List<Room>();
            foreach (var item in book)
            {
                Room room = db.rooms.Find(item.roomId);
                objRoom.Add(room);
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
                lstCustomer.Add(objCustomer);
            }


            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
            {
                Booking booking = new Booking();
                booking.bookingNo = book[0].bookingno;
                booking.CheckIn = book[0].CheckIn;
                booking.CheckoutDate = book[0].CheckoutDate;
                booking.AdvancedAmount = book[0].Advanceprice;
                booking.customers = lstCustomer;
                booking.NoOfPersons = book[0].NoOfPersons;
                booking.ShortDescriptions = book[0].ShortDescriptions;
                booking.Room = objRoom;
                db.bookings.Add(booking);
                db.SaveChanges();

                scope.Complete();

            }

            Room roomBooked = db.rooms.Find(book[0].roomId);
            roomBooked.Status = "Booked";
            db.SaveChanges();

            TempData["Message"] = "Room Booked Successfully.";
            return RedirectToAction("BookedRoom");
        }

        public ActionResult BookedRoom()
        {
            List<bookedViewModel> objBooked = new List<bookedViewModel>();
            foreach (var item in db.bookings.ToList())
            {
                bookedViewModel booked = new bookedViewModel();
                booked.bookingno = item.bookingNo;
                booked.CheckIn = item.CheckIn;
                booked.CheckoutDate = item.CheckoutDate;
                booked.customer = db.bookings.Where(m => m.BookingId == item.BookingId).SelectMany(m => m.customers).ToList();
                booked.noOfPerson = item.NoOfPersons;
                booked.Rooom = db.bookings.Where(m => m.BookingId == item.BookingId).SelectMany(m => m.Room).ToList();
                booked.ShortDescriptions = item.ShortDescriptions;
                objBooked.Add(booked);


            }
            return View(objBooked);
            var userProfiles = db.bookings.ToList().SelectMany(c => c.Room).ToList();
            // var userProfileViewModels = userProfiles.Select(userProfile => userProfile.ToViewModel()).ToList();

            // IQueryable<Booking> query = db.bookings;
            //query = query.Where( ... ); // filters down to exactly one
            //query = query.Include(r => r.Room);


            List<bookedViewModel> book = new List<bookedViewModel>();



            //return View(userProfileViewModels);
        }


        public ActionResult AddCustomer(string id)
        {
            AddCustomerViewModel objAddCustomer = new AddCustomerViewModel();
            objAddCustomer.BookingNo = id;
            return View(objAddCustomer);
        }

        [HttpPost]
        public ActionResult AddCustomer(AddCustomerViewModel addCust)
        {
            Booking objBooking = db.bookings.FirstOrDefault(m => m.bookingNo == addCust.BookingNo);

            db.Customers.Add(new customers { CustomerName = addCust.CustomerName, Address = addCust.Address, NationalIdNo = addCust.NationalIdNo, Nationality = addCust.Nationality });
            db.SaveChanges();

            objBooking.customers.Add(db.Customers.OrderByDescending(m => m.Id).FirstOrDefault());
            db.SaveChanges();


            TempData["Message"] = "Customer Added Successfully.";

            return RedirectToAction("BookedRoom");
        }




        public ActionResult AddRoom(string id)
        {
            AddRoomViewModel objAddCustomer = new AddRoomViewModel();
            List<Room> lstRoom = new List<Room>();
            objAddCustomer.rooms = db.rooms.ToList();
            objAddCustomer.BookingNo = id;
            return View(objAddCustomer);
        }

        [HttpPost]
        public ActionResult AddRoom(AddRoomViewModel addRoom)
        {
            Booking objBooking = db.bookings.FirstOrDefault(m => m.bookingNo == addRoom.BookingNo);
            var objRoom = db.rooms.Find(Convert.ToInt32(addRoom.RoomId));
            objBooking.Room.Add(objRoom);
            db.SaveChanges();

            TempData["Message"] = "Rooms Added Successfully.";
            return RedirectToAction("BookedRoom");
        }
        //[HttpPost]
        //public ActionResult AddRoom(string roomId, string BookingNo)
        //{
        //    Booking objBooking = db.bookings.FirstOrDefault(m => m.bookingNo == BookingNo);
        //    var objRoom = db.rooms.Find(Convert.ToInt32(roomId));
        //    //db.Customers.Add(new customers { CustomerName = addCust.CustomerName, Address = addCust.Address, NationalIdNo = addCust.NationalIdNo, Nationality = addCust.Nationality });
        //    //db.SaveChanges();
        //    objBooking.Room.Add(objRoom);
        //    db.SaveChanges();
        //    return View();
        //}





        public ActionResult PrintList(string id)
        {
            printListViewModel vw = new printListViewModel();
            var objBooking = db.bookings.FirstOrDefault(m => m.bookingNo == id);
            List<Room> objRooms = db.bookings.Where(m => m.bookingNo == id).SelectMany(m => m.Room).ToList();
            decimal advanceAmount = Convert.ToDecimal(db.bookings.FirstOrDefault(m => m.bookingNo == id).AdvancedAmount);
            //  List<customers> objCustomer = db.bookings.Where(m => m.bookingNo == id).SelectMany(m => m.customers).ToList();
            vw.AdvanceAmount = advanceAmount;
            // vw.customer = objCustomer;
            vw.Rooom = objRooms;
            vw.bookingno = id;
            vw.CheckIn = objBooking.CheckIn;
            vw.CheckoutDate = objBooking.CheckoutDate;
            vw.BookingId = objBooking.BookingId;
            vw.DiscountAmount = 0;
            vw.RemainAmount = 0;
            vw.BillNo = "BN1000";
            return View(vw);
        }


        [HttpPost]
        public ActionResult PrintList(printListViewModel objPrint)
        {
            var objBilling = db.billing.FirstOrDefault(m => m.BookingId == objPrint.BookingId);
            if (db.billing.FirstOrDefault(m => m.BookingId == objPrint.BookingId) == null)
            {
                db.billing.Add(new Billing
                {
                    BookingId = objPrint.BookingId,
                    BillNo = objPrint.BillNo,
                    CheckOutDate = objPrint.CheckoutDate,
                    Discount = objPrint.DiscountAmount,
                    NoOfDays = objPrint.NoOfDays,
                    RemainAmount = objPrint.RemainAmount
                });
                db.SaveChanges();
            }
            else
            {
                db.billing.Remove(objBilling);
                db.SaveChanges();

                db.billing.Add(new Billing
                {
                    BookingId = objPrint.BookingId,
                    BillNo = objPrint.BillNo,
                    CheckOutDate = objPrint.CheckoutDate,
                    Discount = objPrint.DiscountAmount,
                    NoOfDays = objPrint.NoOfDays,
                    RemainAmount = objPrint.RemainAmount
                });
                db.SaveChanges();
            }

            {
                List<Room> listRoom = db.bookings.Where(m => m.BookingId == objPrint.BookingId).SelectMany(m => m.Room).ToList();
                foreach (var item in listRoom)
                {
                    item.Status = "Available";
                }
                db.SaveChanges();
            }


            var billing = db.billing.FirstOrDefault(m => m.BookingId == objPrint.BookingId);

            return RedirectToAction("BillPrint",new {id=billing.BillingId });
        }


        public ActionResult BillIndex()
        {
            return View(db.billing.Include(m=>m.Booking).ToList());
        }

        public ActionResult BillPrint(int id)
        {
            var objBill = db.billing.Include(m=>m.Booking.Room).FirstOrDefault(m => m.BillingId == id);
            return View(objBill);
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
