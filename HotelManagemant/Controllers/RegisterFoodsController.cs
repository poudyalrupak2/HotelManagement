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


    public class RegisterFoodsController : Controller
    {
        private Context db = new Context();

        // GET: RegisterFoods
        public ActionResult Index()
        {
            var registerFoods = db.RegisterFoods.Include(r => r.FoodCategory);
            return View(registerFoods.ToList());
        }

        // GET: RegisterFoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterFood registerFood = db.RegisterFoods.Include(a => a.FoodCategory).FirstOrDefault(a => a.Id == id);
                if (registerFood == null)
            {
                return HttpNotFound();
            }
            return View(registerFood);
        }

        // GET: RegisterFoods/Create
        public ActionResult Create()
        {
            ViewBag.FoodCategoryId = new SelectList(db.foodCategories, "Id", "CategoryName");
            return View();
        }

        // POST: RegisterFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public List<FoodImage> fileDetails = new List<FoodImage>();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Quantity,Unit,Price,Ingredients,dicount,status,ImageName,FoodCategoryId")] RegisterFood registerFood, HttpPostedFileBase FImage, ICollection<HttpPostedFileBase> Image)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in Image)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        WebImage img = new WebImage(file.InputStream);
                        string firstpath = "/Images/";
                        string middlepath = "/Images/Food/";
                        string subPath = "/Images/Food/" + registerFood.Name + "/"; 
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
                        FoodImage image = new FoodImage()
                        {
                            ImageName = fileName1,
                            Path = Path.GetExtension(fileName1)

                        };
                        fileDetails.Add(image);

                        if (img.Width < 1000 || img.Width > 1000)
                            img.Resize(500, 500);
                        filename1 = Path.Combine(Server.MapPath("~/Images/Food/" + registerFood.Name + "/"), filename1);
                        img.Save(filename1);


                    }

                }
                int newid;
                try
                {

                    int getid = db.RegisterFoods.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                     newid = getid + 1;

                }
                catch
                {
                     newid =  1;

                }

                registerFood.FoodImages = fileDetails;
                if (FImage != null)
                {
                    string fileName3 = FImage.FileName;
                    string firstpath = "/images/";
                    string subPath = "/images/Food/"; // your code goes here
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
                    filename3 =newid + extension3;
                    registerFood.ImageName = "/images/Food/" + filename3;
                    filename3 = Path.Combine(Server.MapPath("~/images/Food/"), filename3);
                    FImage.SaveAs(filename3);
                }
                registerFood.CreatedAt = DateTime.Now;
                registerFood.CreatedBy = Session["userEmail"].ToString();
                db.RegisterFoods.Add(registerFood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FoodCategoryId = new SelectList(db.foodCategories, "Id", "CategoryName", registerFood.FoodCategoryId);
            return View(registerFood);
        }

        // GET: RegisterFoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterFood registerFood = db.RegisterFoods.Find(id);
            if (registerFood == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodCategoryId = new SelectList(db.foodCategories, "Id", "CategoryName", registerFood.FoodCategoryId);
            return View(registerFood);
        }

        // POST: RegisterFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Quantity,Unit,Price,Ingredients,dicount,status,ImageName,FoodCategoryId")] RegisterFood registerFood, HttpPostedFileBase FImage )
        {
            if (ModelState.IsValid)
            {
                if (FImage != null)
                {
                    string fileName3 = FImage.FileName;
                    string firstpath = "/images/";
                    string subPath = "/images/Food/"; // your code goes here
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
                    filename3 = registerFood.Id + extension3;
                    registerFood.ImageName = "/images/Food/" + filename3;
                    filename3 = Path.Combine(Server.MapPath("~/images/Food/"), filename3);
                    FImage.SaveAs(filename3);
                }
                registerFood.EditedAt = DateTime.Now;
                registerFood.EditedBy = Session["userEmail"].ToString();

                db.Entry(registerFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FoodCategoryId = new SelectList(db.foodCategories, "Id", "CategoryName", registerFood.FoodCategoryId);
            return View(registerFood);
        }

        // GET: RegisterFoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterFood registerFood = db.RegisterFoods.Find(id);
            if (registerFood == null)
            {
                return HttpNotFound();
            }
            return View(registerFood);
        }

        // POST: RegisterFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterFood registerFood = db.RegisterFoods.Find(id);
            db.RegisterFoods.Remove(registerFood);
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
