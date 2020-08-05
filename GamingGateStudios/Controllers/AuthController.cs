using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GamingGateStudios.Models;
using System.Web.Security;

namespace GamingGateStudios.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        DB DB = new DB();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")] Admin admin,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (DB.Admins.Any(x => x.Username==admin.Username && x.Password==admin.Password))
                {
                    FormsAuthentication.SetAuthCookie(admin.Username, false);
                    if (ReturnUrl != null)
                    {
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Admins");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admins");
                    }
                }
                else
                {
                    ViewBag.error = "User Name And Password Is Not Valid !!";
                }
            }
            else
            {
                ViewBag.error = "SomeThing Is Error !!";
            }
            return View();
        }

        public ActionResult Signout()
        {
            if(User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Login");
        }
    }
}