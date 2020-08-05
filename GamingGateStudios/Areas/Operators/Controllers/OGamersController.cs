using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingGateStudios.Models;

namespace GamingGateStudios.Areas.Operators.Controllers
{
    public class OGamersController : Controller
    {
        private DB db = new DB();

        // GET: Operators/OGamers
        public ActionResult Index()
        {
            if(Session["Data"]!=null)
            {
                var gamers = db.Gamers.Include(g => g.Game);
                return View(gamers.ToList());
            }
            else
            {
                return RedirectToAction("SignIn", "OGames", new {area="Operators" });
            }
        }

        // GET: Operators/OGamers/Details/5
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
            if (Session["Data"] != null)
            {
                return View(gamer);
            }
            else
            {
                return RedirectToAction("SignIn", "OGames", new {area="Operators" });
            }
        }
        public ActionResult Age()
        {
            if(Session["Data"]!=null)
            {
                var content = db.Gamers.GroupBy(x => DateTime.Now.Year-x.DateOfBirth.Year);
                return View(content);
            }
            else
            {
                return RedirectToAction("SignIn", "OGames", new { area = "Operators" });
            }
        }
        public ActionResult Country()
        {
            if (Session["Data"] != null)
            {
                var content = db.Gamers.GroupBy(x => x.Country.ToLower());
                return View(content);
            }
            else
            {
                return RedirectToAction("SignIn", "OGames", new { area = "Operators" });
            }
        }
        public ActionResult Gender()
        {
            if (Session["Data"] != null)
            {
                var content = db.Gamers.GroupBy(x => x.Gender);
                return View(content);
            }
            else
            {
                return RedirectToAction("SignIn", "OGames", new { area = "Operators" });
            }
        }
        public ActionResult Active()
        {
            if (Session["Data"] != null)
            {
                var content = db.Gamers.Where(x => x.Active==true).ToList();
                return View(content);
            }
            else
            {
                return RedirectToAction("SignIn", "OGames", new { area = "Operators" });
            }
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
