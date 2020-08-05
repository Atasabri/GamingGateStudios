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
    public class AgenciesController : Controller
    {
        private DB db = new DB();

        // GET: Agencies
        public ActionResult Index()
        {
            return View(db.Agencies.ToList());
        }

        // GET: Agencies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Agency agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // GET: Agencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Target_CPM,Price,UserName,Password")] Agency agency,HttpPostedFileBase photo)
        {
            if (db.Third_Partner.Any(x => x.Password == agency.Password && x.User_Name == agency.UserName) ||
                     db.Channels.Any(x => x.Password == agency.Password && x.Username == agency.UserName) ||
                    db.Ads_Clients.Any(x => x.Password == agency.Password && x.UserName == agency.UserName)||
                    db.Agencies.Any(x=>x.UserName==agency.UserName&&x.Password==agency.Password))
            {
                ViewBag.error = "This Username And Password Is Used !";
                return View(agency);
            }
            if (ModelState.IsValid&&photo!=null)
            {
                db.Agencies.Add(agency);
                db.SaveChanges();
                photo.SaveAs(Server.MapPath("~/Uploads/Agency/" + agency.ID + ".jpg"));
                return RedirectToAction("Index");
            }

            return View(agency);
        }

        // GET: Agencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Agency agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Target_CPM,Price,UserName,Password")] Agency agency,HttpPostedFileBase photo)
        {
            if (db.Third_Partner.Any(x => x.Password == agency.Password && x.User_Name == agency.UserName) ||
            db.Channels.Any(x => x.Password == agency.Password && x.Username == agency.UserName) ||
            db.Ads_Clients.Any(x => x.Password == agency.Password && x.UserName == agency.UserName) ||
            db.Agencies.Any(x => x.UserName == agency.UserName && x.Password == agency.Password&&x.ID!=agency.ID))
            {
                ViewBag.error = "This Username And Password Is Used !";
                return View(agency);
            }
            if (ModelState.IsValid)
            {
                db.Entry(agency).State = EntityState.Modified;
                db.SaveChanges();
                if(photo!=null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/Agency/" + agency.ID + ".jpg"));
                }
                return RedirectToAction("Index");
            }
            return View(agency);
        }

        // GET: Agencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Agency agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agency agency = db.Agencies.Find(id);
            db.Agencies.Remove(agency);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Agency/" + id + ".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
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
