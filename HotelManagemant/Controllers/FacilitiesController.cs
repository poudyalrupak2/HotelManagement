using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelManagemant.Data;
using HotelManagemant.Models;

namespace HotelManagemant.Controllers
{
    public class FacilitiesController : Controller
    {
        private Context db = new Context();

        // GET: Facilities
        public ActionResult Index()
        {
            return View(db.facilities.ToList());
        }

        // GET: Facilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilities facilities = db.facilities.Find(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            return View(facilities);
        }

        // GET: Facilities/Create
        public ActionResult Create()
        {
            List<SelectListItem> types = new List<SelectListItem> { new SelectListItem { Text = "Room", Value = "Room" }, new SelectListItem { Text = "Hotel", Value = "Hotel" } };
            ViewBag.type = new SelectList(types, "Text", "Value");
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Type")] Facilities facilities)
        {
            

            if (ModelState.IsValid)
            {
                db.facilities.Add(facilities);
                db.SaveChanges();

                TempData["Message"] = "Facilities Created Successfully.";


                return RedirectToAction("Index");
            }
            List<SelectListItem> types = new List<SelectListItem> { new SelectListItem { Text = "Room", Value = "Room" }, new SelectListItem { Text = "Hotel", Value = "Hotel" } };
           
            ViewBag.type = new SelectList(types, "Text", "Value");
            if (facilities.Type != "")
            {
                ViewBag.type = new SelectList(types, "Text", "Value",facilities.Type);
            }

            return View(facilities);
        }

        // GET: Facilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilities facilities = db.facilities.Find(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            return View(facilities);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Type")] Facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilities).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Message"] = "Facilities Updated Successfully.";

                return RedirectToAction("Index");
            }
            return View(facilities);
        }

        // GET: Facilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilities facilities = db.facilities.Find(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            return View(facilities);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facilities facilities = db.facilities.Find(id);
            db.facilities.Remove(facilities);
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
