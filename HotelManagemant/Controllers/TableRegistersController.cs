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


    public class TableRegistersController : Controller
    {
        private Context db = new Context();

        // GET: TableRegisters
        public ActionResult Index()
        {
            return View(db.tableRegisters.ToList());
        }

        // GET: TableRegisters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableRegister tableRegister = db.tableRegisters.Find(id);
            if (tableRegister == null)
            {
                return HttpNotFound();
            }
            return View(tableRegister);
        }

        // GET: TableRegisters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TableRegisters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TableNo,Status")] TableRegister tableRegister)
        {
            if (ModelState.IsValid)
            {
                tableRegister.CreatedAt = DateTime.Now;
                tableRegister.CreatedBy = Session["userEmail"].ToString();

                db.tableRegisters.Add(tableRegister);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tableRegister);
        }

        // GET: TableRegisters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableRegister tableRegister = db.tableRegisters.Find(id);
            if (tableRegister == null)
            {
                return HttpNotFound();
            }
            return View(tableRegister);
        }

        // POST: TableRegisters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TableNo,Status")] TableRegister tableRegister)
        {
            if (ModelState.IsValid)
            {
                tableRegister.EditedAt = DateTime.Now;
                tableRegister.EditedBy = Session["userEmail"].ToString();

                db.Entry(tableRegister).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tableRegister);
        }

        // GET: TableRegisters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableRegister tableRegister = db.tableRegisters.Find(id);
            if (tableRegister == null)
            {
                return HttpNotFound();
            }
            return View(tableRegister);
        }

        // POST: TableRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TableRegister tableRegister = db.tableRegisters.Find(id);
            db.tableRegisters.Remove(tableRegister);
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
