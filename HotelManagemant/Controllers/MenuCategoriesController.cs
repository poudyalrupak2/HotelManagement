using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelManagemant.Data;
using HotelManagemant.Filters;
using HotelManagemant.Models;

namespace HotelManagemant.Controllers
{
    [Authorize(Roles = "Admin")]
    [SessionCheck]


    public class MenuCategoriesController : Controller
    {
        private Context db = new Context();

        // GET: MenuCategories
        public ActionResult Index()
        {
            return View(db.Menu.ToList());
        }

        // GET: MenuCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = db.Menu.Find(id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            return View(menuCategory);
        }

        // GET: MenuCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CategoryName,Detail")] MenuCategory menuCategory,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    string fileName3 = Image.FileName;
                    string firstpath = "/images/";
                    string subPath = "/images/Thumbail/"; // your code goes here
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
                   
                    string filename3 = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extension3 = Path.GetExtension(Image.FileName);
                    filename3 = menuCategory.CategoryName + extension3;
                    menuCategory.thumbnail = "/images/Thumbail/" + filename3;
                    filename3 = Path.Combine(Server.MapPath("~/images/Thumbail/"), filename3);
                    Image.SaveAs(filename3);


                }

                db.Menu.Add(menuCategory);
                db.SaveChanges();
                TempData["Message"] = "Category Created Successfully.";

                return RedirectToAction("Index");
            }

            return View(menuCategory);
        }

        // GET: MenuCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = db.Menu.Find(id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            return View(menuCategory);
        }

        // POST: MenuCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CategoryName,Detail,thumbnail,Image")] MenuCategory menuCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuCategory);
        }

        // GET: MenuCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = db.Menu.Find(id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            return View(menuCategory);
        }

        // POST: MenuCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuCategory menuCategory = db.Menu.Find(id);
            db.Menu.Remove(menuCategory);
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
