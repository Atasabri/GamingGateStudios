using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GamingGateStudios.Areas.Operators.Models;
using GamingGateStudios.Models;
using System.Drawing;
using System.IO;

namespace GamingGateStudios.Areas.Operators.Controllers
{
    public class OAgenciesController : Controller
    {
        DB db = new DB();
        // GET: Operators/OAgencies
        public ActionResult Index()
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    if(agent==null)
                    {
                        return RedirectToAction("SignIn", "OGames");
                    }
                    return View(agent.Ads_Clients.ToList());
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        public ActionResult CreateClient()
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    return View();
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        [HttpPost]
        public ActionResult CreateClient(string Name,int Target_CPM,double Price)
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    Ads_Clients Ads_Client = new Ads_Clients
                    {
                        Name = Name,
                        Target_CPM = Target_CPM,
                        Price=Price,  
                        Agency_ID=agent.ID                     
                    };
                    db.Ads_Clients.Add(Ads_Client);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        public ActionResult DetailsClient(int? id)
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    if(id==null)
                    {
                        return RedirectToAction("Index");
                    }
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    Ads_Clients ads_client = db.Ads_Clients.Find(id);

                    if(ads_client==null||agent.Ads_Clients.Contains(ads_client)==false)
                    {
                        return RedirectToAction("Index");
                    }

                    return View(ads_client);
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        public ActionResult EditClient(int? id)
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    Ads_Clients ads_client = db.Ads_Clients.Find(id);

                    if (ads_client == null || agent.Ads_Clients.Contains(ads_client) == false)
                    {
                        return RedirectToAction("Index");
                    }

                    return View(ads_client);
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        [HttpPost]
        public ActionResult EditClient(int? ID , string Name,int Target_CPM,double Price)
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    if (ID == null)
                    {
                        return RedirectToAction("Index");
                    }
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    Ads_Clients ads_client = db.Ads_Clients.Find(ID);

                    if (ads_client == null || agent.Ads_Clients.Contains(ads_client) == false)
                    {
                        return View(ads_client);
                    }
                    ads_client.Name = Name;
                    ads_client.Target_CPM = Target_CPM;
                    ads_client.Price = Price;
                    db.Entry(ads_client).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        [HttpPost]
        public ActionResult DeleteClient(int ?id)
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    Ads_Clients ads_client = db.Ads_Clients.Find(id);

                    if (ads_client == null || agent.Ads_Clients.Contains(ads_client) == false)
                    {
                        return RedirectToAction("Index");
                    }
                    db.Ads_Clients.Remove(ads_client);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        public ActionResult ADS()
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    if (agent == null)
                    {
                        return RedirectToAction("SignIn", "OGames");
                    }
                    List<AD> List = new List<AD>();
                    foreach (var item in agent.Ads_Clients)
                    {
                        foreach (var i in item.ADS)
                        {
                            List.Add(i);
                        }
                    }
                    return View(List);
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        public ActionResult CreateAD()
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    ViewBag.Client_ID = new SelectList(db.Ads_Clients.Where(x => x.Agency_ID == Agent_ID), "ID", "Name");
                    return View();
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }
        [HttpPost]
        public ActionResult CreateAD(AD ad,HttpPostedFileBase photo)
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    ViewBag.Client_ID = new SelectList(db.Ads_Clients.Where(x => x.Agency_ID == Agent_ID), "ID", "Name");
                    if (ModelState.IsValid)
                    {
                        if(photo==null)
                        {
                            ViewBag.error = "Please Select Image !!";
                            return View(ad);
                        }
                        string extention = Path.GetExtension(photo.FileName).ToLower();
                        if(extention==".jpg"|| extention == ".png"|| extention == ".gif"|| extention == ".bmp")
                        {
                            Image IMG = Image.FromStream(photo.InputStream);
                            ad.Width = IMG.Width;
                            ad.Height = IMG.Height;
                            db.ADS.Add(ad);
                            db.SaveChanges();
                            photo.SaveAs(Server.MapPath("~/Uploads/ADS/"+ad.ID+".jpg"));
                            return RedirectToAction("ADS");
                        }
                        else
                        {
                            ViewBag.error = "Please Select Image !!";
                        }
                    }
                    return View(ad);
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        public ActionResult DetailsAD(int? id)
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    AD ad = db.ADS.Find(id);

                    if (ad == null || agent.Ads_Clients.Contains(ad.Ads_Clients) == false)
                    {
                        return RedirectToAction("Index");
                    }

                    return View(ad);
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        public ActionResult EditAD(int? id)
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    if (id == null)
                    {
                        return RedirectToAction("ADS");
                    }
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    AD ad = db.ADS.Find(id);

                    if (ad == null || agent.Ads_Clients.Contains(ad.Ads_Clients) == false)
                    {
                        return RedirectToAction("ADS");
                    }
                    ViewBag.Client_ID = new SelectList(db.Ads_Clients.Where(x => x.Agency_ID == Agent_ID), "ID", "Name");
                    return View(ad);
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        [HttpPost]
        public ActionResult EditAD(int? ID, string Name, int Client_ID, bool Enable,string Link,HttpPostedFileBase photo)
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    if (ID == null)
                    {
                        return RedirectToAction("ADS");
                    }
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    ViewBag.Client_ID = new SelectList(db.Ads_Clients.Where(x => x.Agency_ID == Agent_ID), "ID", "Name");
                    AD ad = db.ADS.Find(ID);

                    if (ad == null || agent.Ads_Clients.Contains(ad.Ads_Clients) == false)
                    {
                        return View(ad);
                    }
                    if (photo != null)
                    {
                        string extention = Path.GetExtension(photo.FileName).ToLower();
                        if (extention == ".jpg" || extention == ".png" || extention == ".gif" || extention == ".bmp")
                        {
                            Image IMG = Image.FromStream(photo.InputStream);
                            ad.Width = IMG.Width;
                            ad.Height = IMG.Height;

                        }
                        else
                        {
                            ViewBag.error = "Please Select Image ?!";
                            return View(ad);
                        }
                    }
                    ad.Name = Name;
                    ad.Client_ID = Client_ID;
                    ad.Enable = Enable;
                    ad.Link = Link;
                    db.Entry(ad).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("ADS");
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        [HttpPost]
        public ActionResult DeleteAD(int? id)
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    if (id == null)
                    {
                        return RedirectToAction("ADS");
                    }
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    AD ad = db.ADS.Find(id);

                    if (ad == null || agent.Ads_Clients.Contains(ad.Ads_Clients) == false)
                    {
                        return RedirectToAction("ADS");
                    }
                    db.ADS.Remove(ad);
                    db.SaveChanges();
                    return RedirectToAction("ADS");
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }

        public ActionResult Data()
        {
            if (Session["Data"] != null)
            {
                if ((Session["Data"] as SIGNINDATA).Type == "AGENCY")
                {
                    int Agent_ID = (Session["Data"] as SIGNINDATA).ID;
                    Agency agent = db.Agencies.Find(Agent_ID);
                    if (agent == null)
                    {
                        return RedirectToAction("SignIn","OGames");
                    }
                    return View(agent);
                }
            }
            return RedirectToAction("SignIn", "OGames");
        }



        //
        public ActionResult Compagin()
        {
            return View();
        }
    }
}