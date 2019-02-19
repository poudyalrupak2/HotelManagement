using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelManagemant.Data;
using HotelManagemant.Filters;
using HotelManagemant.Models;

namespace HotelManagemant.Controllers
{
    [SessionCheck]
    [Authorize(Roles = "Admin")]

    public class DrinkCategoriesController : Controller
    {
        private Context db = new Context();

        // GET: DrinkCategories
        public ActionResult Index()
        {
            return View(db.drinkCategories.ToList());
        }

        // GET: DrinkCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrinkCategory drinkCategory = db.drinkCategories.Find(id);
            if (drinkCategory == null)
            {
                return HttpNotFound();
            }
            return View(drinkCategory);
        }

        // GET: DrinkCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DrinkCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryName,description")] DrinkCategory drinkCategory)
        {
            if (ModelState.IsValid)
            {
                db.drinkCategories.Add(drinkCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drinkCategory);
        }

        // GET: DrinkCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrinkCategory drinkCategory = db.drinkCategories.Find(id);
            if (drinkCategory == null)
            {
                return HttpNotFound();
            }
            return View(drinkCategory);
        }

        // POST: DrinkCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName,description")] DrinkCategory drinkCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drinkCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drinkCategory);
        }

        // GET: DrinkCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrinkCategory drinkCategory = db.drinkCategories.Find(id);
            if (drinkCategory == null)
            {
                return HttpNotFound();
            }
            return View(drinkCategory);
        }

        // POST: DrinkCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DrinkCategory drinkCategory = db.drinkCategories.Find(id);
            db.drinkCategories.Remove(drinkCategory);
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
