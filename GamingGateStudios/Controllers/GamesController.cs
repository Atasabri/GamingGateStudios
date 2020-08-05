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
    public class GamesController : Controller
    {
        private DB db = new DB();

        // GET: Games
        public ActionResult Index()
        {
            return View(db.Games.OrderByDescending(x=>x.Gamers.Count).ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Name_AR,Description,Description_AR,Android_URL,Ituens_URL,Windows_URL,Facebook_URL,KindleFire_URL,EB_URL,Game_Link,Trailer,Start_Tries,Start_Points")] Game game
        ,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid&&photo!=null)
            {
                db.Games.Add(game);
                db.SaveChanges();
                photo.SaveAs(Server.MapPath("~/Uploads/Games/" + game.ID + ".jpg"));              
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Name_AR,Description,Description_AR,Android_URL,Ituens_URL,Windows_URL,Facebook_URL,KindleFire_URL,EB_URL,Game_Link,Trailer,Watch,Start_Tries,Start_Points")] Game game
        , HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                if(photo!=null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/Games/" + game.ID + ".jpg"));
                }       
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            List<Gamer> List = new List<Gamer>();
            List = db.Gamers.Where(x => x.Game_ID == id).ToList();
            db.Gamers.RemoveRange(List);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Games/" + id + ".jpg"));
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
