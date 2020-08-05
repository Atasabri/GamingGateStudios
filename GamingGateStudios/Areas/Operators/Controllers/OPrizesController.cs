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
    public class OPrizesController : Controller
    {
        private DB db = new DB();

        // GET: Operators/OPrizes
        public ActionResult Index()
        {
            if(Session["Data"]!=null)
            {
                if((Session["Data"]as SIGNINDATA).Type=="THIRD")
                {
                    int Third_ID = (Session["Data"] as SIGNINDATA).ID;
                    var prizes = db.Prizes.Where(x=>x.Third_Partner==Third_ID&&x.Type==1);
                    return View(prizes.ToList());
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

        // GET: Operators/OPrizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Prize prize = db.Prizes.Find(id);
            int Third_ID = (Session["Data"] as SIGNINDATA).ID;
            if (prize == null||prize.Third_Partner!=Third_ID||prize.Type==0)
            {
                return HttpNotFound();
            }
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "THIRD")
                {
                    return View(prize);
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
        public ActionResult Winners()
        {
            if(Session["Data"]!=null)
            {
                if((Session["Data"]as SIGNINDATA).Type=="THIRD")
                {
                    int Third_ID = (Session["Data"] as SIGNINDATA).ID;
                    var Winners = db.Winners.Where(x => x.Prize.Third_Partner == Third_ID && x.Prize.Type == 1);
                    return View(Winners.ToList());
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
        public ActionResult Unreceived()
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "THIRD")
                {
                    int Third_ID = (Session["Data"] as SIGNINDATA).ID;
                    var Winners = db.Winners.Where(x => x.Prize.Third_Partner == Third_ID && x.Prize.Type == 1&&x.Received==false);
                    return View(Winners.ToList());
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

        [HttpPost]
        public ActionResult Win(int id)
        {
            if(Session["Data"]!=null)
            {
                if((Session["Data"] as SIGNINDATA).Type == "THIRD")
                {
                    Winner winner = db.Winners.Find(id);
                    if (winner == null)
                    {
                        return RedirectToAction("Unreceived");
                    }
                    winner.Received = true;
                    db.Entry(winner).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Unreceived");
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
