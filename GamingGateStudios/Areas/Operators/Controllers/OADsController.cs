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
    public class OADsController : Controller
    {
        private DB db = new DB();

        // GET: Operators/OADs
        public ActionResult Index()
        {
            if(Session["Data"]!=null)
            {
                if((Session["Data"]as SIGNINDATA).Type=="ADS")
                {
                    int AdsClient_ID = (Session["Data"] as SIGNINDATA).ID;
                    var aDS = db.ADS.Where(x=>x.Client_ID==AdsClient_ID);
                    return View(aDS.ToList());
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
            AD aD = db.ADS.Find(id);
            int AdsClient_ID = (Session["Data"] as SIGNINDATA).ID;
            if (aD == null||aD.Client_ID!=AdsClient_ID)
            {
                return HttpNotFound();
            }
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "ADS")
                {
                    return View(aD);
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
                if ((Session["Data"] as SIGNINDATA).Type == "ADS")
                {
                    int AdsClient_ID = (Session["Data"] as SIGNINDATA).ID;
                    Ads_Clients Client = db.Ads_Clients.Find(AdsClient_ID);
                    return View(Client);
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
