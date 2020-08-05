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
    public class OGamesController : Controller
    {
        private DB db = new DB();

        // GET: Operators/OGames
        public ActionResult Index()
        {
            if(Session["Data"]!=null)
            {
                return View(db.Games.OrderByDescending(x=>x.Gamers.Count).ToList());
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }

        // GET: Operators/OGames/Details/5
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
            if (Session["Data"] != null)
            {
                return View(game);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string UserName,string Password)
        {
            try
            {
                if (db.Ads_Clients.Any(x => x.UserName == UserName && x.Password == Password&&x.Agency_ID==null&&string.IsNullOrEmpty(x.UserName)==false&& string.IsNullOrEmpty(x.Password)==false))
                {
                    int ID = db.Ads_Clients.Single(x => x.UserName == UserName && x.Password == Password).ID;
                    SIGNINDATA Data = new SIGNINDATA();
                    Data.ID = ID;
                    Data.Type = "ADS";
                    Data.UserName = UserName;
                    Session["Data"] = Data;
                    return RedirectToAction("Index", "OGames", new { area = "Operators" });
                }
                else if (db.Channels.Any(x => x.Username == UserName && x.Password == Password))
                {
                    int ID = db.Channels.Single(x => x.Username == UserName && x.Password == Password).ID;
                    SIGNINDATA Data = new SIGNINDATA();
                    Data.ID = ID;
                    Data.Type = "SPLASH";
                    Data.UserName = UserName;
                    Session["Data"] = Data;
                    return RedirectToAction("Index", "OGames", new { area = "Operators" });
                }
                else if (db.Third_Partner.Any(x => x.User_Name == UserName && x.Password == Password))
                {
                    int ID = db.Third_Partner.Single(x => x.User_Name == UserName && x.Password == Password).ID;
                    SIGNINDATA Data = new SIGNINDATA();
                    Data.ID = ID;
                    Data.Type = "THIRD";
                    Data.UserName = UserName;
                    Session["Data"] = Data;
                    return RedirectToAction("Index", "OGames", new { area = "Operators" });
                }
                else if (db.Operators.Any(x => x.User_Name == UserName && x.Password == Password))
                {
                    int ID = db.Operators.Single(x => x.User_Name == UserName && x.Password == Password).ID;
                    SIGNINDATA Data = new SIGNINDATA();
                    Data.ID = ID;
                    Data.Type = "OPERATOR";
                    Data.UserName = UserName;
                    Session["Data"] = Data;
                    return RedirectToAction("Index", "OGames", new { area = "Operators" });
                }
                else if(db.Agencies.Any(x=>x.UserName==UserName&&x.Password==Password))
                {
                    int ID = db.Agencies.Single(x => x.UserName == UserName && x.Password == Password).ID;
                    SIGNINDATA Data = new SIGNINDATA();
                    Data.ID = ID;
                    Data.Type = "AGENCY";
                    Data.UserName = UserName;
                    Session["Data"] = Data;
                    return RedirectToAction("Index", "OGames", new { area = "Operators" });
                }
                ViewBag.error = "User Name And Password Not Valid ?!!";
            }
            catch
            {
                ViewBag.error = "Error In Sign In ?!!";
            }
            return View();
        }
        public ActionResult SignOut()
        {
            Session.Remove("Data");
            return RedirectToAction("Signin");
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
