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
    [Authorize(Roles ="Admin")]
    public class MenuContinentsController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            return View(db.Continent.ToList());
        }

        // GET: MenuContinents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuContinent menuContinent = db.Continent.Find(id);
            if (menuContinent == null)
            {
                return HttpNotFound();
            }
            return View(menuContinent);
        }

        // GET: MenuContinents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuContinents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ContinentName")] MenuContinent menuContinent)
        {
            if (ModelState.IsValid)
            {
                db.Continent.Add(menuContinent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menuContinent);
        }

        // GET: MenuContinents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuContinent menuContinent = db.Continent.Find(id);
            if (menuContinent == null)
            {
                return HttpNotFound();
            }
            return View(menuContinent);
        }

        // POST: MenuContinents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContinentName")] MenuContinent menuContinent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuContinent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuContinent);
        }

        // GET: MenuContinents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuContinent menuContinent = db.Continent.Find(id);
            if (menuContinent == null)
            {
                return HttpNotFound();
            }
            return View(menuContinent);
        }

        // POST: MenuContinents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuContinent menuContinent = db.Continent.Find(id);
            db.Continent.Remove(menuContinent);
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
