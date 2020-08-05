using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingGateStudios.Models;
using System.IO;

namespace GamingGateStudios.Controllers
{
    [Authorize]
    public class AdminsController : Controller
    {
        private DB db = new DB();

        // GET: Admins
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated &&db.Admins.Any(x => x.Username ==User.Identity.Name))
            {
                return View(db.Admins.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            if (new DB().Admins.First(x => x.Username == User.Identity.Name).Admin_Type == 1)
            {
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (new DB().Admins.First(x => x.Username == User.Identity.Name).Admin_Type == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,Admin_Type")] Admin admin,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    if (photo != null)
                    {
                        photo.SaveAs(Server.MapPath("~/Uploads/Admins/" + admin.ID + ".jpg"));
                    }
                    return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            if (new DB().Admins.First(x => x.Username == User.Identity.Name).Admin_Type == 1)
            {
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Admin_Type")] Admin admin,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {

                    bool auth = false;
                    if (admin.Username == User.Identity.Name)
                    {
                        auth = true;
                    }
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();
                    if (photo != null)
                    {
                        photo.SaveAs(Server.MapPath("~/Uploads/Admins/" + admin.ID + ".jpg"));
                    }
                    if (auth == false)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                              
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            if (new DB().Admins.First(x => x.Username == User.Identity.Name).Admin_Type == 1)
            {
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            bool auth = false;
            if(admin.Username==User.Identity.Name)
            {
                auth = true;
            }
            db.Admins.Remove(admin);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Admins/" + id + ".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
            if(auth==false)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login","Auth");
            }
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
