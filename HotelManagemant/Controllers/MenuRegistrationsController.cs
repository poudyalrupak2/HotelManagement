using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HotelManagemant.Data;
using HotelManagemant.Models;

namespace HotelManagemant.Controllers
{
    [Authorize(Roles = "Admin")]

    public class MenuRegistrationsController : Controller
    {

        private Context db = new Context();

        // GET: MenuRegistrations
        public ActionResult Index()
        {
            return View(db.MenuReg.ToList());
        }

        // GET: MenuRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuRegistration menuRegistration = db.MenuReg.Find(id);
            if (menuRegistration == null)
            {
                return HttpNotFound();
            }
            return View(menuRegistration);
        }

        // GET: MenuRegistrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598
        public List<Image> fileDetails = new List<Image>();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Itemname,Ingrident,Price")] MenuRegistration menuRegistration, HttpPostedFileBase TImage, ICollection<HttpPostedFileBase> Image)
        {
            if (ModelState.IsValid)
            {

                foreach (var file in Image)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        WebImage img = new WebImage(file.InputStream);
                        string firstpath = "/Images/";
                        string middlepath = "/Images/Menu/";
                        string subPath = "/Images/Menu/"+menuRegistration.Itemname + "/"; // your code goes here
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
                        Image image = new Image()
                        {
                            ImageName = fileName1,
                            Path = Path.GetExtension(fileName1)
                            
                        };
                        fileDetails.Add(image);

                        if (img.Width < 1000 || img.Width > 1000)
                            img.Resize(903, 631);
                        filename1 = Path.Combine(Server.MapPath("~/Images/Menu/"+menuRegistration.Itemname+"/"), filename1);
                        img.Save(filename1);


                    }

                }
                menuRegistration.Image = fileDetails;
                if (TImage != null)
                {
                    string fileName3 = TImage.FileName;
                    string firstpath = "/images/";
                    string subPath = "/images/Thumbail/"; // your code goes here
                    string ppath = "/images/Thumbail/Menureg/";
                    bool imageexist = System.IO.Directory.Exists(Server.MapPath(firstpath));
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                    bool ppp = System.IO.Directory.Exists(Server.MapPath(ppath));
                    if (!imageexist)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(firstpath));
                    }
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                    }
                    if (!ppp)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(ppath));

                    }

                    string filename3 = Path.GetFileNameWithoutExtension(TImage.FileName);
                    string extension3 = Path.GetExtension(TImage.FileName);
                    filename3 = menuRegistration.Id + extension3;
                    menuRegistration.Thumbnail = "/images/Thumbail/menureg/" + filename3;
                    filename3 = Path.Combine(Server.MapPath("~/images/Thumbail/Menureg/"), filename3);
                    TImage.SaveAs(filename3);
                }

                db.MenuReg.Add(menuRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");

            }



            return View(menuRegistration);
        }

        // GET: MenuRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuRegistration menuRegistration = db.MenuReg.Find(id);
            if (menuRegistration == null)
            {
                return HttpNotFound();
            }
            return View(menuRegistration);
        }

        // POST: MenuRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Itemname,Ingrident,Price,Thumbnail")] MenuRegistration menuRegistration, HttpPostedFileBase TImage, ICollection<HttpPostedFileBase> Image)
        {
            if (ModelState.IsValid)
            {
                var  model = db.MenuReg.Include(t => t.Image).SingleOrDefault(x => x.Id == menuRegistration.Id);
                string thumbail = db.MenuReg.Include(t => t.Image).Where(x => x.Id == menuRegistration.Id).Select(x => x.Thumbnail).SingleOrDefault();
                string menuname = db.MenuReg.Include(t => t.Image).Where(x => x.Id == menuRegistration.Id).Select(x => x.Itemname).SingleOrDefault();
                MenuRegistration menu = db.MenuReg.SingleOrDefault(x => x.Id == menuRegistration.Id);

                try
                {

                    if (Image.Any(m => m.ContentLength > 0))
                    {
                        Directory.Delete(Server.MapPath("~/Images/Menu/" + model.Itemname + "/"), true);

                        foreach (var images in model.Image)
                        {

                            
                            string firstpath = "/Images/";
                            string middlepath = "/Images/Menu/";
                            string subPath = "/Images/Menu/" + menuRegistration.Itemname + "/"; // your code goes here
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


                            string imagename = images.ImageName.ToString();
                            var fileName = Path.Combine(Server.MapPath("~/Images/Menu/" + model.Itemname + "/"),Path.GetFileName(imagename));



                            db.Database.ExecuteSqlCommand("delete from Images where ImageName='" + imagename + "'");

                        }
                    }

                }
                catch (Exception ex)
                {
                    goto Found;
                }
            Found:
                {
                    List<Image> fileDetails = new List<Image>();
                    foreach (var file in Image)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            WebImage img = new WebImage(file.InputStream);

                            
                            var fileName = Path.Combine(Server.MapPath("/Images/Menu/" + menuRegistration.Itemname + "/"), Path.GetFileName(file.FileName));
                            Image image = new Image()
                            {
                                ImageName = "/Images/Menu/" + menuRegistration.Itemname + "/" + Path.GetFileName(fileName),
                                Path = Path.GetExtension(fileName),
                                MenuRegistration=menu
                            };
                            fileDetails.Add(image);
                            if (img.Width < 1000 || img.Width > 1000)
                                img.Resize(903, 631);
                            img.Save(fileName);
                            db.Entry(image).State = EntityState.Added;
                        }
                    }
                    db.SaveChanges();

                    if (TImage != null)
                    {
                        string fileName3 = TImage.FileName;

                        string filename3 = Path.GetFileNameWithoutExtension(TImage.FileName);
                        string extension3 = Path.GetExtension(TImage.FileName);
                        filename3 = menuRegistration.Id + extension3;
                        menuRegistration.Thumbnail = "/images/Thumbail/menureg/" + filename3;
                        filename3 = Path.Combine(Server.MapPath("~//images/Thumbail/menureg/"), filename3);
                        TImage.SaveAs(filename3);
                    }
                    else
                    {
                        menuRegistration.Thumbnail = thumbail;
                    }
                    var objpackage = db.MenuReg.SingleOrDefault(m => m.Id == menuRegistration.Id);
                    objpackage.Ingrident = menuRegistration.Ingrident;
                    objpackage.Itemname = menuRegistration.Itemname;
                    objpackage.Price = menuRegistration.Price;
                  
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                }
            return View(menuRegistration);

       
        }

        private object Set<T>()
        {
            throw new NotImplementedException();
        }

        // GET: MenuRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuRegistration menuRegistration = db.MenuReg.Find(id);
            if (menuRegistration == null)
            {
                return HttpNotFound();
            }
            return View(menuRegistration);
        }

        // POST: MenuRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuRegistration menuRegistration = db.MenuReg.Find(id);
            db.MenuReg.Remove(menuRegistration);
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
