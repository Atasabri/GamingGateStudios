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
    public class Ads_ClientsController : Controller
    {
        private DB db = new DB();

        // GET: Ads_Clients
        public ActionResult Index()
        {
            return View(db.Ads_Clients.Where(x=>x.Agency_ID==null).ToList());
        }

        // GET: Ads_Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Ads_Clients ads_Clients = db.Ads_Clients.Find(id);
            if (ads_Clients == null||ads_Clients.Agency_ID.HasValue)
            {
                return HttpNotFound();
            }
            return View(ads_Clients);
        }

        // GET: Ads_Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ads_Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Target_CPM,UserName,Password")] Ads_Clients ads_Clients,HttpPostedFileBase photo)
        {
            if (db.Third_Partner.Any(x => x.Password == ads_Clients.Password && x.User_Name == ads_Clients.UserName) ||
               db.Channels.Any(x => x.Password == ads_Clients.Password && x.Username == ads_Clients.UserName)||
               db.Ads_Clients.Any(x => x.Password == ads_Clients.Password && x.UserName == ads_Clients.UserName) ||
                    db.Agencies.Any(x => x.UserName == ads_Clients.UserName && x.Password == ads_Clients.Password))
            {
                ViewBag.error = "This Username And Password Is Used !";
                return View(ads_Clients);
            }
            if (ModelState.IsValid&&photo!=null)
            {
                db.Ads_Clients.Add(ads_Clients);
                db.SaveChanges();
                photo.SaveAs(Server.MapPath("~/Uploads/AdsClients/"+ads_Clients.ID+".jpg"));
                return RedirectToAction("Index");
            }

            return View(ads_Clients);
        }

        // GET: Ads_Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Ads_Clients ads_Clients = db.Ads_Clients.Find(id);
            if (ads_Clients == null || ads_Clients.Agency_ID.HasValue)
            {
                return HttpNotFound();
            }
            return View(ads_Clients);
        }

        // POST: Ads_Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Target_CPM,UserName,Password")] Ads_Clients ads_Clients,HttpPostedFileBase photo)
        {
            if (db.Third_Partner.Any(x => x.Password == ads_Clients.Password && x.User_Name == ads_Clients.UserName) ||
                db.Channels.Any(x => x.Password == ads_Clients.Password && x.Username == ads_Clients.UserName) ||
                db.Agencies.Any(x => x.UserName == ads_Clients.UserName && x.Password == ads_Clients.Password)||
               db.Ads_Clients.Any(x => x.Password == ads_Clients.Password && x.UserName == ads_Clients.UserName&&x.ID!=ads_Clients.ID))
            {
                ViewBag.error = "This Username And Password Is Used !";
                return View(ads_Clients);
            }
            if (ModelState.IsValid)
            {
                db.Entry(ads_Clients).State = EntityState.Modified;
                db.SaveChanges();
                if(photo!=null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/AdsClients/" + ads_Clients.ID + ".jpg"));
                }
                return RedirectToAction("Index");
            }
            return View(ads_Clients);
        }

        // GET: Ads_Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Ads_Clients ads_Clients = db.Ads_Clients.Find(id);
            if (ads_Clients == null)
            {
                return HttpNotFound();
            }
            return View(ads_Clients);
        }

        // POST: Ads_Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ads_Clients ads_Clients = db.Ads_Clients.Find(id);
            db.Ads_Clients.Remove(ads_Clients);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/AdsClients/" + id + ".jpg"));
            if (F.Exists)
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
