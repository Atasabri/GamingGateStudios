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
using System.Drawing;

namespace GamingGateStudios.Controllers
{
    [Authorize]
    public class PortfoliosController : Controller
    {
        private DB db = new DB();

        // GET: Portfolios
        public ActionResult Index()
        {
            return View(db.Portfolios.ToList());
        }

        // GET: Portfolios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // GET: Portfolios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Portfolios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Portfolio portfolio,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid&&photo!=null)
            {
                string path = Path.GetExtension(photo.FileName).ToLower();
                if(path==".png"||path==".jpg"||path==".gif"||path==".bmp")
                {
                        try
                        {
                            db.Portfolios.Add(portfolio);
                            db.SaveChanges();
                            photo.SaveAs(Server.MapPath("~/Uploads/Portfolio/" + portfolio.ID + ".jpg"));
                            return RedirectToAction("Index");
                        }
                        catch
                        {
                            ViewData["Error"] = "Error In Create Portfolio";
                            return View(portfolio);
                        }                   
                }
                else
                {
                    ViewData["Error"] = "Photo Extention Must Be in(.jpg & .png & .gif & .bmp)";
                    return View(portfolio);
                }
            }
            ViewData["Error"] = "Error In Create Portfolio";
            return View(portfolio);
        }

        // GET: Portfolios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // POST: Portfolios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Portfolio portfolio,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portfolio).State = EntityState.Modified;
                db.SaveChanges();
                if(photo!=null)
                {
                    string path = Path.GetExtension(photo.FileName);
                    if (path == ".png" || path == ".jpg" || path == ".gif" || path == ".bmp")
                    {

                            photo.SaveAs(Server.MapPath("~/Uploads/Portfolio/" + portfolio.ID + ".jpg"));
                            return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Error"] = "Photo Extention Must Be in(.jpg & .png & .gif & .bmp)";
                        return View(portfolio);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(portfolio);
        }

        // GET: Portfolios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // POST: Portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Portfolio portfolio = db.Portfolios.Find(id);
            db.Portfolios.Remove(portfolio);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Portfolio/" + id + ".jpg"));
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
