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
    public class UsersController : Controller
    {
        private DB db = new DB();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Password,Phone,Country,DateOfBirth,Gender,Member_ID,Active")] User user,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid&&photo!=null)
            {
                if(db.Users.Any(x => x.Email == user.Email && x.Password == user.Password) == false)
                {
                    MemberShip m = db.MemberShips.Find(user.Member_ID);
                    if (m.Price.HasValue)
                    {
                        if (m.Price.Value != 0)
                        {
                            user.Active = false;
                        }
                        else { user.Active = true; }
                    }
                    else { user.Active = true; }
                    db.Users.Add(user);
                    db.SaveChanges();
                    photo.SaveAs(Server.MapPath("~/Uploads/Users/" + user.ID + ".jpg"));
                    return RedirectToAction("Index");
                }
                else
                {

                    ViewBag.error = "This User Email & Password Is Exists !!";
                }
            }
            return View(user);
        }

        //// GET: Users/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Name,Email,Password,Phone,Country,DateOfBirth,Gender,Face_ID")] User user,HttpPostedFileBase photo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (db.Users.Any(x => x.Email == user.Email && x.Password == user.Password) == false)
        //        {
        //            User u = db.Users.Find(user.ID);
        //            u.Name = user.Name;
        //            u.Email = user.Email;
        //            u.Password = user.Password;
        //            u.Phone = user.Phone;
        //            u.Country = user.Country;
        //            u.DateOfBirth = user.DateOfBirth;
        //            u.Gender = user.Gender;
        //            u.Face_ID = user.Face_ID;
        //            db.Entry(u).State = EntityState.Modified;
        //            db.SaveChanges();
        //            if (photo != null)
        //            {
        //                photo.SaveAs(Server.MapPath("~/Uploads/Users/" + user.ID + ".jpg"));
        //            }
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.error = "This User Email & Password Is Exists !!";
        //        }
        //    }
        //    return View(user);
        //}

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Users/" + id + ".jpg"));
            if (F.Exists)
            {
                F.Delete();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SendMail(string Subject, HttpPostedFileBase file, List<string> c)
        {
            if (c != null && c.Count() >= 1 && file != null)
            {
                List<string> Users = c.Distinct().ToList();
                DEL.Send_Mail(Subject, file, Users);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Countries()
        {
            var content = db.Users.GroupBy(x => x.Country.ToLower());
            return View(content);
        }
        public ActionResult Age()
        {
            var content = db.Users.GroupBy(x => DateTime.Now.Year-x.DateOfBirth.Year);
            return View(content);
        }
        public ActionResult Gender()
        {
            var content = db.Users.GroupBy(x => x.Gender);
            return View(content);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RestMoney()
        {
            db.Rest_Users();
            return RedirectToAction("Index");
        }
        public ActionResult PaymentMethod()
        {
            return View(db.Users.GroupBy(x => x.Pay));
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
