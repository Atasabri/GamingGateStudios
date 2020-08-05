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
    public class PrizesController : Controller
    {
        private DB db = new DB();

        // GET: Prizes
        public ActionResult Index()
        {
            var prizes = db.Prizes.Include(p => p.Third_Partner1);
            return View(prizes.ToList());
        }

        // GET: Prizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        // GET: Prizes/Create
        public ActionResult Create()
        {
            ViewBag.Third_Partner = new SelectList(db.Third_Partner, "ID", "Name");
            return View();
        }

        // POST: Prizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Name_AR,Type,Third_Partner,Points,Price")] Prize prize,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid&&photo!=null)
            {
                if(prize.Type==1)
                {
                    prize.Price = null;
                }
                else
                {
                    prize.Third_Partner = null;
                }
                db.Prizes.Add(prize);
                db.SaveChanges();
                photo.SaveAs(Server.MapPath("~/Uploads/Prizes/" + prize.ID + ".jpg"));
                return RedirectToAction("Index");
            }

            ViewBag.Third_Partner = new SelectList(db.Third_Partner, "ID", "Name", prize.Third_Partner);
            return View(prize);
        }

        // GET: Prizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            ViewBag.Third_Partner = new SelectList(db.Third_Partner, "ID", "Name", prize.Third_Partner);
            return View(prize);
        }

        // POST: Prizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Name_AR,Type,Third_Partner,Points,Price")] Prize prize,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (prize.Type == 1)
                {
                    prize.Price = null;
                }
                else
                {
                    prize.Third_Partner = null;
                }
                db.Entry(prize).State = EntityState.Modified;
                db.SaveChanges();
                if(photo!=null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/Prizes/" + prize.ID + ".jpg"));
                }
                return RedirectToAction("Index");
            }
            ViewBag.Third_Partner = new SelectList(db.Third_Partner, "ID", "Name", prize.Third_Partner);
            return View(prize);
        }

        // GET: Prizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        // POST: Prizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prize prize = db.Prizes.Find(id);
            db.Prizes.Remove(prize);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Prizes/" + id + ".jpg"));
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
