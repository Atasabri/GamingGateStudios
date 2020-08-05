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
    public class MemberShipsController : Controller
    {
        private DB db = new DB();

        // GET: MemberShips
        public ActionResult Index()
        {
            return View(db.MemberShips.OrderByDescending(x=>x.Users.Count).ToList());
        }

        // GET: MemberShips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MemberShip memberShip = db.MemberShips.Find(id);
            if (memberShip == null)
            {
                return HttpNotFound();
            }
            return View(memberShip);
        }

        // GET: MemberShips/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberShips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Limit_Token_Money,color")] MemberShip memberShip,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid&&photo!=null)
            {
                db.MemberShips.Add(memberShip);
                db.SaveChanges();
                photo.SaveAs(Server.MapPath("~/Uploads/Member/" + memberShip.ID + ".jpg"));
                return RedirectToAction("Index");
            }

            return View(memberShip);
        }

        // GET: MemberShips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MemberShip memberShip = db.MemberShips.Find(id);
            if (memberShip == null)
            {
                return HttpNotFound();
            }
            return View(memberShip);
        }

        // POST: MemberShips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Limit_Token_Money,color")] MemberShip memberShip,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberShip).State = EntityState.Modified;
                db.SaveChanges();
                if(photo!=null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/Member/" + memberShip.ID + ".jpg"));
                }
                return RedirectToAction("Index");
            }
            return View(memberShip);
        }

        // GET: MemberShips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MemberShip memberShip = db.MemberShips.Find(id);
            if (memberShip == null)
            {
                return HttpNotFound();
            }
            return View(memberShip);
        }

        // POST: MemberShips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberShip memberShip = db.MemberShips.Find(id);
            db.MemberShips.Remove(memberShip);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Member/" + id + ".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
            return RedirectToAction("Index");
        }

        public ActionResult SendMail(string Subject, HttpPostedFileBase file, List<int> c)
        {
            if (c != null && c.Count() >= 1 && file != null)
            {
                List<string> Users = db.Users.Where(x => c.Contains(x.Member_ID)).Select(x=>x.Email).Distinct().ToList();
                DEL.Send_Mail(Subject, file, Users);
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
