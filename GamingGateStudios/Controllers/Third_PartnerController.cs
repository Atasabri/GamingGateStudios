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
    public class Third_PartnerController : Controller
    {
        private DB db = new DB();

        // GET: Third_Partner
        public ActionResult Index()
        {
            return View(db.Third_Partner.ToList());
        }

        // GET: Third_Partner/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Third_Partner third_Partner = db.Third_Partner.Find(id);
            if (third_Partner == null)
            {
                return HttpNotFound();
            }
            return View(third_Partner);
        }

        // GET: Third_Partner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Third_Partner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,User_Name,Password,Name")] Third_Partner third_Partner,HttpPostedFileBase photo)
        {
            if(db.Ads_Clients.Any(x => x.Password == third_Partner.Password&&x.UserName==third_Partner.User_Name)||
               db.Agencies.Any(x => x.UserName == third_Partner.User_Name && x.Password == third_Partner.Password) ||
                db.Channels.Any(x => x.Password == third_Partner.Password && x.Username == third_Partner.User_Name)||
               db.Third_Partner.Any(x => x.Password == third_Partner.Password && x.User_Name == third_Partner.User_Name))
            {
                ViewBag.error = "This Username And Password Is Used !";
                return View(third_Partner);
            }
            if (ModelState.IsValid&&photo!=null)
            {
                db.Third_Partner.Add(third_Partner);
                db.SaveChanges();
                photo.SaveAs(Server.MapPath("~/Uploads/ThirdPartner/"+third_Partner.ID+".jpg"));
                return RedirectToAction("Index");
            }

            return View(third_Partner);
        }

        // GET: Third_Partner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Third_Partner third_Partner = db.Third_Partner.Find(id);
            if (third_Partner == null)
            {
                return HttpNotFound();
            }
            return View(third_Partner);
        }

        // POST: Third_Partner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,User_Name,Password,Name")] Third_Partner third_Partner,HttpPostedFileBase photo)
        {
            if (db.Ads_Clients.Any(x => x.Password == third_Partner.Password && x.UserName == third_Partner.User_Name) ||
                 db.Agencies.Any(x => x.UserName == third_Partner.User_Name && x.Password == third_Partner.Password) ||
                db.Channels.Any(x => x.Password == third_Partner.Password && x.Username == third_Partner.User_Name)||
                 db.Third_Partner.Any(x => x.Password == third_Partner.Password && x.User_Name == third_Partner.User_Name && x.ID != third_Partner.ID))
            {
                ViewBag.error = "This Username And Password Is Used !";
                return View(third_Partner);
            }
            if (ModelState.IsValid)
            {
                db.Entry(third_Partner).State = EntityState.Modified;
                db.SaveChanges();
                if(photo!=null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/ThirdPartner/" + third_Partner.ID + ".jpg"));
                }
                return RedirectToAction("Index");
            }
            return View(third_Partner);
        }

        // GET: Third_Partner/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Third_Partner third_Partner = db.Third_Partner.Find(id);
            if (third_Partner == null)
            {
                return HttpNotFound();
            }
            return View(third_Partner);
        }

        // POST: Third_Partner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Third_Partner third_Partner = db.Third_Partner.Find(id);
            db.Third_Partner.Remove(third_Partner);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/ThirdPartner/" + id + ".jpg"));
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
