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
    public class ChannelsController : Controller
    {
        private DB db = new DB();

        // GET: Channels
        public ActionResult Index()
        {
            return View(db.Channels.ToList());
        }

        // GET: Channels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Channel channel = db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return View(channel);
        }

        // GET: Channels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Channels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Username,Password")] Channel channel,HttpPostedFileBase photo)
        {
            if (db.Third_Partner.Any(x => x.Password == channel.Password && x.User_Name == channel.Username) ||
                 db.Agencies.Any(x => x.UserName == channel.Username && x.Password == channel.Password) ||
                db.Ads_Clients.Any(x => x.Password == channel.Password && x.UserName == channel.Username)||
               db.Channels.Any(x => x.Password == channel.Password && x.Username == channel.Username))
            {
                ViewBag.error = "This Username And Password Is Used !";
                return View(channel);
            }
            if (ModelState.IsValid&&photo!=null)
            {
                db.Channels.Add(channel);
                db.SaveChanges();
                photo.SaveAs(Server.MapPath("~/Uploads/Channels/" + channel.ID + ".jpg"));
                return RedirectToAction("Index");
            }

            return View(channel);
        }

        // GET: Channels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Channel channel = db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return View(channel);
        }

        // POST: Channels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Username,Password")] Channel channel,HttpPostedFileBase photo)
        {
            if (db.Third_Partner.Any(x => x.Password == channel.Password && x.User_Name == channel.Username) ||
                db.Ads_Clients.Any(x => x.Password == channel.Password && x.UserName == channel.Username) ||
                 db.Agencies.Any(x => x.UserName == channel.Username && x.Password == channel.Password) ||
               db.Channels.Any(x => x.Password == channel.Password && x.Username == channel.Username && x.ID != channel.ID))
            {
                ViewBag.error = "This Username And Password Is Used !";
                return View(channel);
            }
            if (ModelState.IsValid)
            {
                db.Entry(channel).State = EntityState.Modified;
                db.SaveChanges();
                if(photo!=null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/Channels/" + channel.ID + ".jpg"));
                }
                return RedirectToAction("Index");
            }
            return View(channel);
        }

        // GET: Channels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Channel channel = db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return View(channel);
        }

        // POST: Channels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Channel channel = db.Channels.Find(id);
            db.Channels.Remove(channel);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Channels/" + id + ".jpg"));
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
