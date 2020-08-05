using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingGateStudios.Models;

namespace GamingGateStudios.Controllers
{
    [Authorize]
    public class GGBDealsController : Controller
    {
        private DB db = new DB();

        // GET: GGBDeals
        public ActionResult Index()
        {
            return View(db.GGBDeals.ToList());
        }

        // GET: GGBDeals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            GGBDeal gGBDeal = db.GGBDeals.Find(id);
            if (gGBDeal == null)
            {
                return HttpNotFound();
            }
            return View(gGBDeal);
        }

        // GET: GGBDeals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GGBDeals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,GGB")] GGBDeal gGBDeal)
        {
            if (ModelState.IsValid)
            {
                db.GGBDeals.Add(gGBDeal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gGBDeal);
        }

        // GET: GGBDeals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            GGBDeal gGBDeal = db.GGBDeals.Find(id);
            if (gGBDeal == null)
            {
                return HttpNotFound();
            }
            return View(gGBDeal);
        }

        // POST: GGBDeals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,GGB")] GGBDeal gGBDeal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gGBDeal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gGBDeal);
        }

        // GET: GGBDeals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            GGBDeal gGBDeal = db.GGBDeals.Find(id);
            if (gGBDeal == null)
            {
                return HttpNotFound();
            }
            return View(gGBDeal);
        }

        // POST: GGBDeals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GGBDeal gGBDeal = db.GGBDeals.Find(id);
            db.GGBDeals.Remove(gGBDeal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PaymentMethod()
        {
            return View(db.Users_GGBDeals.GroupBy(x => x.Pay));
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
