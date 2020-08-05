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
using System.Drawing;

namespace GamingGateStudios.Controllers
{
    [Authorize]
    public class SplashesController : Controller
    {
        private DB db = new DB();

        // GET: Splashes
        public ActionResult Index()
        {
            var splashes = db.Splashes.Include(s => s.Channel);
            return View(splashes.ToList());
        }

        // GET: Splashes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Splash splash = db.Splashes.Find(id);
            if (splash == null)
            {
                return HttpNotFound();
            }
            return View(splash);
        }

        // GET: Splashes/Create
        public ActionResult Create()
        {
            ViewBag.Channel_ID = new SelectList(db.Channels, "ID", "Name");
            return View();
        }

        // POST: Splashes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Channel_ID,Enable,Width,Height,Game_ID")] Splash splash,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid&&photo!=null)
            {
                Image IMG = Image.FromStream(photo.InputStream);
                splash.Width = IMG.Width;
                splash.Height = IMG.Height;

                db.Splashes.Add(splash);
                db.SaveChanges();
                photo.SaveAs(Server.MapPath("~/Uploads/Splashes/" + splash.ID + ".jpg"));
                return RedirectToAction("Index");
            }

            ViewBag.Channel_ID = new SelectList(db.Channels, "ID", "Name", splash.Channel_ID);
            return View(splash);
        }

        // GET: Splashes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Splash splash = db.Splashes.Find(id);
            if (splash == null)
            {
                return HttpNotFound();
            }
            ViewBag.Channel_ID = new SelectList(db.Channels, "ID", "Name", splash.Channel_ID);
            return View(splash);
        }

        // POST: Splashes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Channel_ID,Enable,Width,Height,Game_ID")] Splash splash,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if(photo!=null)
                {
                    Image IMG = Image.FromStream(photo.InputStream);
                    splash.Width = IMG.Width;
                    splash.Height = IMG.Height;
                }
                db.Entry(splash).State = EntityState.Modified;
                db.SaveChanges();
                if(photo!=null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/Splashes/" + splash.ID + ".jpg"));
                }
                return RedirectToAction("Index");
            }
            ViewBag.Channel_ID = new SelectList(db.Channels, "ID", "Name", splash.Channel_ID);
            return View(splash);
        }

        // GET: Splashes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Splash splash = db.Splashes.Find(id);
            if (splash == null)
            {
                return HttpNotFound();
            }
            return View(splash);
        }

        // POST: Splashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Splash splash = db.Splashes.Find(id);
            db.Splashes.Remove(splash);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Splashes/"+id+".jpg"));
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
