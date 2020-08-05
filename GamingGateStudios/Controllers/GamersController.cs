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
    public class GamersController : Controller
    {
        private DB db = new DB();

        // GET: Gamers
        public ActionResult Index()
        {
            var gamers = db.Gamers.Include(g => g.Game);
            return View(gamers.ToList());
        }

        // GET: Gamers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Gamer gamer = db.Gamers.Find(id);
            if (gamer == null)
            {
                return HttpNotFound();
            }
            return View(gamer);
        }

        // GET: Gamers/Create
        public ActionResult Create()
        {
            ViewBag.Game_ID = new SelectList(db.Games, "ID", "Name");
            return View();
        }

        // POST: Gamers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Phone,UserName,Password,Member,Country,DateOfBirth,Gender,Points,Tries,Mony_Send,Money_Token,Active,Game_ID")] Gamer gamer)
        {
            if (ModelState.IsValid)
            {
                db.Gamers.Add(gamer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Game_ID = new SelectList(db.Games, "ID", "Name", gamer.Game_ID);
            return View(gamer);
        }

        //// GET: Gamers/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    Gamer gamer = db.Gamers.Find(id);
        //    if (gamer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Game_ID = new SelectList(db.Games, "ID", "Name", gamer.Game_ID);
        //    return View(gamer);
        //}

        //// POST: Gamers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Phone,UserName,Password,Member,Country,DateOfBirth,Gender,Points,Tries,Mony_Send,Money_Token,Active,Game_ID")] Gamer gamer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(gamer).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Game_ID = new SelectList(db.Games, "ID", "Name", gamer.Game_ID);
        //    return View(gamer);
        //}

        // GET: Gamers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Gamer gamer = db.Gamers.Find(id);
            if (gamer == null)
            {
                return HttpNotFound();
            }
            return View(gamer);
        }

        // POST: Gamers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gamer gamer = db.Gamers.Find(id);
            db.Gamers.Remove(gamer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Countries()
        {
            var content = db.Gamers.GroupBy(x => x.Country.ToLower());
            return View(content);
        }
        public ActionResult Age()
        {
            var content = db.Gamers.GroupBy(x => DateTime.Now.Year - x.DateOfBirth.Year);
            return View(content);
        }
        public ActionResult Gender()
        {
            var content = db.Gamers.GroupBy(x => x.Gender);
            return View(content);
        }
        public ActionResult Active()
        {
            var content = db.Gamers.Where(x => x.Active).ToList();
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
