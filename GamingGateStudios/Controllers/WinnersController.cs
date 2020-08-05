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
    public class WinnersController : Controller
    {
        private DB db = new DB();

        // GET: Winners
        public ActionResult Index()
        {
            var winners = db.Winners.Include(w => w.Gamer).Include(w => w.Prize).Include(w => w.User);
            return View(winners.ToList());
        }

        // GET: Winners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Winner winner = db.Winners.Find(id);
            if (winner == null)
            {
                return HttpNotFound();
            }
            return View(winner);
        }

        //// GET: Winners/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Gamer_ID = new SelectList(db.Gamers, "ID", "Phone");
        //    ViewBag.Prize_ID = new SelectList(db.Prizes, "ID", "Name");
        //    ViewBag.User_ID = new SelectList(db.Users, "ID", "Name");
        //    return View();
        //}

        //// POST: Winners/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,User_ID,Prize_ID,Gamer_ID,Received")] Winner winner)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Winners.Add(winner);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Gamer_ID = new SelectList(db.Gamers, "ID", "Phone", winner.Gamer_ID);
        //    ViewBag.Prize_ID = new SelectList(db.Prizes, "ID", "Name", winner.Prize_ID);
        //    ViewBag.User_ID = new SelectList(db.Users, "ID", "Name", winner.User_ID);
        //    return View(winner);
        //}

        // GET: Winners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Winner winner = db.Winners.Find(id);
            if (winner == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gamer_ID = new SelectList(db.Gamers, "ID", "Phone", winner.Gamer_ID);
            ViewBag.Prize_ID = new SelectList(db.Prizes, "ID", "Name", winner.Prize_ID);
            ViewBag.User_ID = new SelectList(db.Users, "ID", "Name", winner.User_ID);
            return View(winner);
        }

        // POST: Winners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,User_ID,Prize_ID,Gamer_ID,Received")] Winner winner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(winner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Gamer_ID = new SelectList(db.Gamers, "ID", "Phone", winner.Gamer_ID);
            ViewBag.Prize_ID = new SelectList(db.Prizes, "ID", "Name", winner.Prize_ID);
            ViewBag.User_ID = new SelectList(db.Users, "ID", "Name", winner.User_ID);
            return View(winner);
        }

        // GET: Winners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Winner winner = db.Winners.Find(id);
            if (winner == null)
            {
                return HttpNotFound();
            }
            return View(winner);
        }

        // POST: Winners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Winner winner = db.Winners.Find(id);
            db.Winners.Remove(winner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UnReceived()
        {
            var content = db.Winners.Where(x => x.Received == false).ToList();
            return View(content);
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
