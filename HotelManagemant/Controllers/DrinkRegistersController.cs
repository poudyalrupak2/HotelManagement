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
using HotelManagemant.Filters;
using HotelManagemant.Models;

namespace HotelManagemant.Controllers
{
    [SessionCheck]
    [Authorize(Roles = "Admin")]

    public class DrinkRegistersController : Controller
    {
        private Context db = new Context();

        // GET: DrinkRegisters
        public ActionResult Index()
        {
            var drinkRegisters = db.drinkRegisters.Include(d => d.DrinkCategory);
            return View(drinkRegisters.ToList());
        }

        // GET: DrinkRegisters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrinkRegister drinkRegister = db.drinkRegisters.Include(b => b.DrinkCategory).FirstOrDefault(m => m.Id == id);
            if (drinkRegister == null)
            {
                return HttpNotFound();
            }
            return View(drinkRegister);
        }

        // GET: DrinkRegisters/Create
        public ActionResult Create()
        {
            ViewBag.DrinkCategoryId = new SelectList(db.drinkCategories, "Id", "CategoryName");
            return View();
        }
        public List<DrinkImage> fileDetails = new List<DrinkImage>();
        // POST: DrinkRegisters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DrinkRegister drinkRegister, HttpPostedFileBase FImage, ICollection<HttpPostedFileBase> Image)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in Image)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        WebImage img = new WebImage(file.InputStream);
                        string firstpath = "/Images/";
                        string middlepath = "/Images/Drink/";
                        string subPath = "/Images/Drink/" + drinkRegister.Name + "/";
                        bool imageexist = System.IO.Directory.Exists(Server.MapPath(firstpath));
                        bool Mdexists = System.IO.Directory.Exists(Server.MapPath(middlepath));

                        bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                        if (!imageexist)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(firstpath));

                        }
                        if (!Mdexists)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(middlepath));
                        }

                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                        }

                        string fileName1 = file.FileName;
                        string filename1 = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension1 = Path.GetExtension(file.FileName);
                        filename1 = filename1 + extension1;
                        DrinkImage image = new DrinkImage()
                        {
                            ImageName = fileName1,
                            Path = Path.GetExtension(fileName1)

                        };
                        fileDetails.Add(image);

                        if (img.Width < 1000 || img.Width > 1000)
                            img.Resize(500, 500);
                        filename1 = Path.Combine(Server.MapPath("~/Images/Drink/" + drinkRegister.Name + "/"), filename1);
                        img.Save(filename1);


                    }

                }
                int newid;
                try
                {
                    int getid = db.drinkRegisters.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                    //int id = getid.Id;tch
                    
                     newid = getid + 1;
                }
                catch
                {
                    newid = 1;
                }
                if (FImage != null)
                {
                    string fileName3 = FImage.FileName;
                    string firstpath = "/images/";
                    string subPath = "/images/Drink/"; // your code goes here
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

                    string filename3 = Path.GetFileNameWithoutExtension(FImage.FileName);
                    string extension3 = Path.GetExtension(FImage.FileName);
                    filename3 = newid + extension3;
                    drinkRegister.ImageName = "/images/Drink/" + filename3;
                    filename3 = Path.Combine(Server.MapPath("~/images/Drink/"), filename3);
                    FImage.SaveAs(filename3);
                }
                db.drinkRegisters.Add(drinkRegister);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
            }
            ViewBag.DrinkCategoryId = new SelectList(db.drinkCategories, "Id", "CategoryName", drinkRegister.DrinkCategoryId);
            return View(drinkRegister);
        }

        // GET: DrinkRegisters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrinkRegister drinkRegister = db.drinkRegisters.Find(id);
            if (drinkRegister == null)
            {
                return HttpNotFound();
            }
            ViewBag.DrinkCategoryId = new SelectList(db.drinkCategories, "Id", "Id", drinkRegister.DrinkCategoryId);
            return View(drinkRegister);
        }

        // POST: DrinkRegisters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UnitQuantity,UnitName,UnitPrice,Description,dicount,status,DrinkCategoryId")] DrinkRegister drinkRegister, HttpPostedFileBase FImage)
        {
            if (ModelState.IsValid)
            {
                if (FImage != null)
                {
                    string fileName3 = FImage.FileName;
                    string firstpath = "/images/";
                    string subPath = "/images/Drink/"; // your code goes here
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

                    string filename3 = Path.GetFileNameWithoutExtension(FImage.FileName);
                    string extension3 = Path.GetExtension(FImage.FileName);
                    filename3 = drinkRegister.Id + extension3;
                    drinkRegister.ImageName = "/images/Drink/" + filename3;
                    filename3 = Path.Combine(Server.MapPath("~/images/Drink/"), filename3);
                    FImage.SaveAs(filename3);
                }

                db.Entry(drinkRegister).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DrinkCategoryId = new SelectList(db.drinkCategories, "Id", "Id", drinkRegister.DrinkCategoryId);
            return View(drinkRegister);
        }

        // GET: DrinkRegisters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrinkRegister drinkRegister = db.drinkRegisters.Find(id);
            if (drinkRegister == null)
            {
                return HttpNotFound();
            }
            return View(drinkRegister);
        }

        // POST: DrinkRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DrinkRegister drinkRegister = db.drinkRegisters.Find(id);
            db.drinkRegisters.Remove(drinkRegister);
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
