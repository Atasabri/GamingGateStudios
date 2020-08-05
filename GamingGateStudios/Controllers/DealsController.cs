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
    public class DealsController : Controller
    {
        private DB db = new DB();

        // GET: Deals
        public ActionResult Index()
        {
            var deals = db.Deals.OrderByDescending(x=>x.Deal_Gamers.Count).Include(d => d.Game);
            return View(deals.ToList());
        }

        // GET: Deals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        // GET: Deals/Create
        public ActionResult Create()
        {
            ViewBag.Game_ID = new SelectList(db.Games, "ID", "Name");
            Session.Remove("Data");
            return View();
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Name_AR,Game_ID")] Deal deal,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid&&Session["Data"]!=null&&photo!=null)
            {
                List<Deal_Member> List = new List<Deal_Member>();
                List = Session["Data"] as List<Deal_Member>;
                if(List.Count!=db.MemberShips.Count())
                {
                    ViewBag.error = "Please Add GGB Count For Every MemberShip";
                    ViewBag.Game_ID = new SelectList(db.Games, "ID", "Name");
                    return View(deal);
                }
                deal.Deal_Member = List;
                db.Deals.Add(deal);
                db.SaveChanges();
                photo.SaveAs(Server.MapPath("~/Uploads/Deals/" + deal.ID + ".jpg"));
                Session.Remove("Data");
                return RedirectToAction("Index");
            }

            ViewBag.Game_ID = new SelectList(db.Games, "ID", "Name", deal.Game_ID);
            return View(deal);
        }

        // GET: Deals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            ViewBag.Game_ID = new SelectList(db.Games, "ID", "Name", deal.Game_ID);
            Session.Remove("Data");
            return View(deal);
        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Name_AR,Game_ID")] Deal deal,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if(Session["Data"]!=null)
                {
                    foreach (var item in Session["Data"] as List<Deal_Member>)
                    {
                        if (db.Deal_Member.Any(x => x.Member_ID == item.Member_ID && x.Deal_ID == deal.ID))
                        {
                            Deal_Member dealmember = db.Deal_Member.Single(x => x.Deal_ID == deal.ID && x.Member_ID == item.Member_ID);
                            db.Deal_Member.Remove(dealmember);
                        }
                        db.Deal_Member.Add(new Deal_Member { Deal_ID = deal.ID, Member_ID = item.Member_ID, GGB = item.GGB });
                    }
                }
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                Session.Remove("Data");
                if(photo!=null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/Deals/" + deal.ID + ".jpg"));
                }
                return RedirectToAction("Index");
            }
            ViewBag.Game_ID = new SelectList(db.Games, "ID", "Name", deal.Game_ID);
            return View(deal);
        }

        // GET: Deals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deal deal = db.Deals.Find(id);
            db.Deals.Remove(deal);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Deals/" + id + ".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add_MemberPrice(int Member_ID,string price)
        {
            List<Deal_Member> List = new List<Deal_Member>();
            if (Session["Data"] == null)
            {
                try
                {
                    List.Add(new Deal_Member { GGB = Convert.ToInt16(price), Member_ID = Member_ID });
                    Session["Data"] = List;
                }
                catch { }
            }
            else
            {
                try
                {
                    List = Session["Data"] as List<Deal_Member>;
                    if(List.Any(x=>x.Member_ID==Member_ID)==false)
                    {
                        List.Add(new Deal_Member { GGB = Convert.ToInt16(price), Member_ID = Member_ID });
                    }
                    Session["Data"] = List;
                }
                catch { }
            }
            return PartialView("_Data");
        }

        //public ActionResult DeleteMP(int MemberID,int id)
        //{
        //    Deal_Member dealmember = db.Deal_Member.Find(MemberID);
        //    db.Deal_Member.Remove(dealmember);
        //    db.SaveChanges();
        //    return RedirectToAction("Edit", new { @id = id });
        //}
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
