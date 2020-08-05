using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingGateStudios.Models;
using GamingGateStudios.Areas.Operators.Models;

namespace GamingGateStudios.Areas.Operators.Controllers
{
    public class OSplashesController : Controller
    {
        private DB db = new DB();

        // GET: Operators/OADs
        public ActionResult Index()
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "SPLASH")
                {
                    int Channel_ID = (Session["Data"] as SIGNINDATA).ID;
                    var Splashes = db.Splashes.Where(x => x.Channel_ID == Channel_ID);
                    return View(Splashes.ToList());
                }
                else
                {
                    return RedirectToAction("SignIn", "OGames");
                }
            }
            else
            {
                return RedirectToAction("SignIn", "OGames");
            }
        }

        // GET: Operators/OADs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Splash Splash = db.Splashes.Find(id);
            int CHANNEL_ID = (Session["Data"] as SIGNINDATA).ID;
            if (Splash == null || Splash.Channel_ID != CHANNEL_ID)
            {
                return HttpNotFound();
            }
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "SPLASH")
                {
                    return View(Splash);
                }
                else
                {
                    return RedirectToAction("SignIn", "OGames");
                }
            }
            else
            {
                return RedirectToAction("SignIn", "OGames");
            }
        }
        public ActionResult Data()
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "SPLASH")
                {
                    int CHANNEL_ID = (Session["Data"] as SIGNINDATA).ID;
                    Channel CHANNEL = db.Channels.Find(CHANNEL_ID);
                    return View(CHANNEL);
                }
                else
                {
                    return RedirectToAction("SignIn", "OGames");
                }
            }
            else
            {
                return RedirectToAction("SignIn", "OGames");
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
