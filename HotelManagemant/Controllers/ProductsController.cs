using HotelManagemant.Data;
using HotelManagemant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagemant.Models;
using AutoMapper;
using HotelManagemant.Filters;

namespace HotelManagemant.Controllers
{
    [SessionCheck]
    [Authorize(Roles = "Admin")]


    public class ProductsController : Controller
    {
        // GET: Products
        private Context db = new Context();

        public ActionResult Index(int? id)

        {
            productViewModel model = new productViewModel();

            List<BookingTableVM> listBooking = new List<BookingTableVM>();
            model.tableNo = 0;
            if (id != null)


            {
                // Bookingtable booked = db.Bookingtables.Find(id);

                int tableNo = Convert.ToInt32(id);


                var booking = (from B in db.Bookingtables
                               from F in db.RegisterFoods
                               where B.ItemId == F.Id && B.TableNo == tableNo
                               select new { B, F.Price }).ToList();


                var booking2 = (from B in db.Bookingtables
                                from F in db.drinkRegisters
                                where B.ItemId == F.Id && B.TableNo == tableNo
                                select new { B, F.Price }).ToList();

                booking.Concat(booking2);


                listBooking = booking.Select(m => new BookingTableVM(m.B, m.Price)).ToList();

                model.tableNo = tableNo;

            }

            model.bookingtables = listBooking;
            model.DrinkRegister = db.drinkRegisters;
            model.RegisterFood = db.RegisterFoods;
            model.tableRegister = db.tableRegisters;
            return View(model);
        }

        public JsonResult getproductbyid(int id, string value)
        {
            if (value == "Food")
            {
                var Food = db.RegisterFoods.Where(t => t.Id == id).FirstOrDefault();
                
                return Json(Food, JsonRequestBehavior.AllowGet);
            }

            if (value == "Drink")
            {
                var Drink = db.drinkRegisters.Where(t => t.Id == id).FirstOrDefault();
                return Json(Drink, JsonRequestBehavior.AllowGet);
            }
            return null;

        }



        public JsonResult tableInfo(int tableno)
        {
            var tables = db.tableRegisters.Where(i => i.TableNo == tableno).FirstOrDefault();
            string status = tables.Status;
            if (status == "Available")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpPost]
        public JsonResult CreateBooking(List<Bookingtable> Bookingtable)
        {
            if (ModelState.IsValid)
            {
                Bookingtable objTable = new Bookingtable();

                int tableNo = Bookingtable[0].TableNo;

                List<Bookingtable> lstTable = db.Bookingtables.Where(m => m.TableNo == tableNo).ToList();

                for (int i = 0; i < lstTable.Count; i++)
                {
                    db.Bookingtables.Remove(lstTable[i]);
                    db.SaveChanges();
                }

                foreach (var item in Bookingtable)
                {
                    objTable.ItemId = item.ItemId;
                    objTable.Itemname = item.Itemname;
                    objTable.Quantity = item.Quantity;
                    objTable.TableNo = item.TableNo;
                    objTable.Type = item.Type;

                    db.Bookingtables.Add(objTable);
                    db.SaveChanges();

                    TableRegister table = db.tableRegisters.Where(m => m.TableNo == item.TableNo).FirstOrDefault();
                    table.Status = "Unavailable";
                    db.SaveChanges();
                }
                IEnumerable<int> tables = (from ta in db.Bookingtables
                                           select ta.TableNo).Distinct();

                return Json("success");// RedirectToAction("ViewBilling",tables);
            }

            return Json("fail");// View();
        }


        public ActionResult ViewBilling()
        {

            IEnumerable<int> tables = (from ta in db.Bookingtables
                                       select ta.TableNo).Distinct();

            return View(tables);

        }



        public ActionResult Bill(int id)
        {
            productViewModel model = new productViewModel();

            List<BookingTableVM> listBooking = new List<BookingTableVM>();

            int tableNo = Convert.ToInt32(id);


            var booking = (from B in db.Bookingtables
                           from F in db.RegisterFoods
                           where B.ItemId == F.Id && B.TableNo == tableNo && B.Type=="Food"
                           select new { B, F.Price }).ToList();


            var booking2 = (from B in db.Bookingtables
                            from F in db.drinkRegisters
                            where B.ItemId == F.Id && B.TableNo == tableNo && B.Type == "Drink"
                            select new { B, F.Price }).ToList();

            booking=booking.Concat(booking2).ToList();


            listBooking = booking.Select(m => new BookingTableVM(m.B, m.Price)).ToList();

            model.tableNo = tableNo;



            model.bookingtables = listBooking;
            model.DrinkRegister = db.drinkRegisters;
            model.RegisterFood = db.RegisterFoods;
            model.tableRegister = db.tableRegisters;
            return View(model);

        }
        public ActionResult PrintBill(int id)
        {
            ResBillViewModel model = new ResBillViewModel();

            BillingInfo objBilling = db.BillingInfos.OrderByDescending(m => m.Id).FirstOrDefault();

            model.discount = objBilling.Discount;
            model.TableNo = objBilling.TableNo;
            model.vat = objBilling.Vat;
            model.date = objBilling.Date;
            model.customerName = objBilling.Name;

            var objfood = (from B in db.billingItems
                           from F in db.RegisterFoods
                           where B.ItemId == F.Id && B.BillingInfoId == objBilling.Id && B.ItemType == "Food"
                           select new { name = F.Name, qty = B.Quantity, rate = F.Price }).ToList();
            var objdrink = (from B in db.billingItems
                           from F in db.drinkRegisters
                           where B.ItemId == F.Id && B.BillingInfoId == objBilling.Id && B.ItemType == "Drink"
                           select new { name = F.Name, qty = B.Quantity, rate = F.Price }).ToList();
          //  objfood.Concat(objdrink);


            List<foodItems> foo = new List<foodItems>();
            for (int i=0;i<objfood.Count;i++)
            {
                foodItems foos = new foodItems
                {

                    name = objfood[i].name,
                    qty = objfood[i].qty,
                    rate = objfood[i].rate,
                };
                foo.Add(foos); 
            }

            for (int i = 0; i < objdrink.Count; i++)
            {
                foodItems foos = new foodItems
                {

                    name = objdrink[i].name,
                    qty = objdrink[i].qty,
                    rate = objdrink[i].rate,
                };
                foo.Add(foos);
            }
            model.grandTotal = foo.Sum(m => m.qty * m.rate);
            model.vatAmount = (model.grandTotal  * model.vat)/100;
            model.grandTotal = model.grandTotal - model.discount + model.vatAmount;
            model.foodItems = foo;
            return View(model);

        }

        public JsonResult createbill(List<BillingItem> BillingItem, List<BillingInfo> BillingInfo)
        {
            BillingItem bil = new BillingItem();

            BillingInfo Info = new BillingInfo();

            for (int i = 0; i < BillingItem.Count; i++)
            {
                bil.ItemId = BillingItem[i].ItemId;
                bil.ItemType = BillingItem[i].ItemType;
                bil.Quantity = BillingItem[i].Quantity;
            }
            Info.BillingItems = BillingItem;

            Info.Name = BillingInfo[0].Name;
            Info.Discount = BillingInfo[0].Discount;
            Info.TableNo = BillingInfo[0].TableNo;
            Info.Vat = BillingInfo[0].Vat;
            Info.Date = DateTime.Now;
            db.BillingInfos.Add(Info);

            db.Database.ExecuteSqlCommand("delete from Bookingtables where TableNo='" + Info.TableNo + "'");
            db.Database.ExecuteSqlCommand("Update TableRegisters SET Status ='Available'  where TableNo='" + Info.TableNo + "'");



            db.SaveChanges();
            return Json("success");


        }


        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
