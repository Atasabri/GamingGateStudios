using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingGateStudios.Models;
using System.Drawing;
using System.IO;

namespace GamingGateStudios.Controllers
{
    [Authorize]
    public class ADsController : Controller
    {
        private DB db = new DB();

        // GET: ADs
        public ActionResult Index()
        {
            var aDS = db.ADS.Include(a => a.Ads_Clients);
            return View(aDS.ToList());
        }

        // GET: ADs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            AD aD = db.ADS.Find(id);
            if (aD == null)
            {
                return HttpNotFound();
            }
            return View(aD);
        }

        // GET: ADs/Create
        public ActionResult Create()
        {
            ViewBag.Client_ID = new SelectList(db.Ads_Clients.Where(x=>x.Agency_ID==null), "ID", "Name");
            return View();
        }

        // POST: ADs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Client_ID,Enable,Width,Height,Link")] AD aD,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid&&photo!=null)
            {
                Image IMG = Image.FromStream(photo.InputStream);
                aD.Width = IMG.Width;
                aD.Height = IMG.Height;
                db.ADS.Add(aD);
                db.SaveChanges();
                photo.SaveAs(Server.MapPath("~/Uploads/ADS/"+aD.ID+".jpg"));
                return RedirectToAction("Index");
            }

            ViewBag.Client_ID = new SelectList(db.Ads_Clients.Where(x=>x.Agency_ID==null), "ID", "Name", aD.Client_ID);
            return View(aD);
        }

        // GET: ADs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            AD aD = db.ADS.Find(id);
            if (aD == null||aD.Ads_Clients.Agency_ID!=null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_ID = new SelectList(db.Ads_Clients.Where(x => x.Agency_ID == null), "ID", "Name", aD.Client_ID);
            return View(aD);
        }

        // POST: ADs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Client_ID,Enable,Width,Height,CPM,Link,Click")] AD aD,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if(photo!=null)
                {
                    Image IMG = Image.FromStream(photo.InputStream);
                    aD.Width = IMG.Width;
                    aD.Height = IMG.Height;
                }
                db.Entry(aD).State = EntityState.Modified;
                db.SaveChanges();
                if(photo!=null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/ADS/" + aD.ID + ".jpg"));
                }
                return RedirectToAction("Index");
            }
            ViewBag.Client_ID = new SelectList(db.Ads_Clients.Where(x => x.Agency_ID == null), "ID", "Name", aD.Client_ID);
            return View(aD);
        }

        // GET: ADs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            AD aD = db.ADS.Find(id);
            if (aD == null)
            {
                return HttpNotFound();
            }
            return View(aD);
        }

        // POST: ADs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AD aD = db.ADS.Find(id);
            db.ADS.Remove(aD);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/ADS/" + id + ".jpg"));
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
