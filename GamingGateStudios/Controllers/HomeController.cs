using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GamingGateStudios.Models;
using System.Net;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Threading;
using System.Diagnostics;

namespace GamingGateStudios.Controllers
{
    //[HandleError(View ="Error")]
    public class HomeController : Controller
    {
        DB db = new DB();
        public ActionResult Soon()
        {
            return View();
        }
        public ActionResult Home()
        {
                return View(db.Games.ToList());
        }
        public ActionResult About()
        {
                return View(db.Games.OrderByDescending(x=>x.ID).Take(3).ToList());
        }
        public ActionResult Contact()
        {
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(FormCollection form)
        {
            if(Session["AUTH"]!=null)
            {
                if (form["FName"] != string.Empty && form["LName"] != string.Empty &&
                    form["Email"] != string.Empty && form["Message"] != string.Empty)
                {
                    string FirstName = form["FName"];
                    string LastName = form["LName"];
                    string Email = form["Email"];
                    string Message = form["Message"];
                    db.Contacts.Add(new Contact { First_Name = FirstName, Last_Name = LastName, Email = Email, Message = Message });
                    db.SaveChanges();
                }
            }
            else
            {
                return RedirectToAction("Signin");
            }     
            return View();
        }
        public ActionResult Memberships()
        {
            var content = db.MemberShips.ToList();
            return View(content);
        }
        public ActionResult SignUp(int? id)
        {
            if (string.IsNullOrEmpty(Request.QueryString["access_token"]))
            {
                if (id == null)
                {
                    return RedirectToAction("Memberships");
                }
                MemberShip Member = db.MemberShips.Find(id);
                if (Member == null)
                {
                    return RedirectToAction("Memberships");
                }
            }
            ViewData["Member_ID"] = id;
            if (Session["FaceBook"] != null)
            {
                User F_user = Session["FaceBook"] as User;
                return View(F_user);
            }
           if (string.IsNullOrEmpty(Request.QueryString["access_token"])) { return View(new User()); }; //ERROR! No token returned from Facebook!!

            //let's send an http-request to facebook using the token            
            string json = GetFacebookUserJSON(Request.QueryString["access_token"]);

            //and Deserialize the JSON response
            JavaScriptSerializer js = new JavaScriptSerializer();
            FacebookUser oUser = js.Deserialize<FacebookUser>(json);
            if(db.Users.Any(x=>x.Face_ID==oUser.id.ToString()&&x.Active==true))
            {
                User F_User= db.Users.Where(x => x.Face_ID == oUser.id.ToString()).First();
                string color = db.MemberShips.Find(F_User.Member_ID).color;
                AuthUser Auth = new AuthUser(F_User.ID,F_User.Name ,F_User.Member_ID, color, F_User.Face_ID);
                Session["AUTH"] = Auth;
                return RedirectToAction("Home");
            }
            User user = new User();
            user.Name = oUser.name;
            user.Email = oUser.email;
            user.Face_ID = oUser.id.ToString();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SignUp(User user,HttpPostedFileBase photo,int day,int year,int month,int?Pay)
        {
            if (Request.Cookies["Language"] != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                Request.Cookies["Language"].Value = "en";
            }
            user.DateOfBirth =Convert.ToDateTime(month + "-" + day + "-" + year);
            ViewData["Member_ID"] = user.Member_ID;
            if (db.Users.Any(x=>x.Email==user.Email))
            {
                ViewBag.error = "This Email Is Used !";
                return View(user);
            }
            if (db.Users.Any(x => x.Phone == user.Phone))
            {
                ViewBag.error = "This Phone Is Used !";
                return View(user);
            }
            if (user.Face_ID==null)
            {
                if (photo == null)
                {
                    ViewBag.error = "Please , Add Your Picture !";
                    return View(user);
                }
                string ex = Path.GetExtension(photo.FileName).ToLower();
                if (ex != ".jpg" && ex != ".png" && ex != ".gif" && ex != ".bmp" && ex != ".jepg")
                {
                    ViewBag.error = "Photo Extention Must be In (.jpg & .png & .gif & .bmp)";
                    return View(user);
                }
            }         
            int Member_ID = user.Member_ID;
            MemberShip Member = db.MemberShips.Find(Member_ID);
            if(Member==null)
            {
                return RedirectToAction("MemberShips");
            }
            try
            {
                if(Member.Price.HasValue)
                {
                    if(Member.Price.Value!=0)
                    {
                        if (Pay == null)
                        {
                            ViewBag.error = "Please , Choose A Payment Method !";
                            return View(user);
                        }
                        user.Active = false;
                        user.Pay = Pay;
                    }
                    else { user.Active = true; }
                }else { user.Active = true; }
                db.Users.Add(user);
                if(db.Users.Where(x => x.Face_ID == user.Face_ID && x.Active == false).Count()>0)
                {
                    db.Users.RemoveRange(db.Users.Where(x => x.Face_ID == user.Face_ID && x.Active == false));
                }
                db.SaveChanges();
                if (user.Face_ID == null)
                {
                    photo.SaveAs(Server.MapPath("~/Uploads/Users/" + user.ID + ".jpg"));
                }

                #region Payment Method
                if (user.MemberShip.Price.HasValue)
                {
                    if (user.MemberShip.Price.Value != 0)
                    {
                        if (Pay == null)
                        {
                            ViewBag.error = "Please , Choose A Payment Method !";
                            return View(user);
                        }
                        else
                        {
                            string ReturnURL = DEL.Domain + "/Home/C?i=" + DEL.encrypt(user.ID.ToString());
                            return Redirect(DEL.PayMethod(Pay.Value, ReturnURL, "USD", "Sign Up", Member.Price.Value.ToString(), user.Name, user.Name, user.Email, user.Phone, user.Country, "AUS", "Sign Up", "Signup123", DEL.Domain));
                        }
                    }
                }
                #endregion
            }
            catch
            {
                ViewBag.error = "Error In Create Account ,  Please Try Again !";
                return View(user);
            }

            string color = db.MemberShips.Find(user.Member_ID).color;
            AuthUser Auth = new AuthUser(user.ID, user.Name, user.Member_ID, color, user.Face_ID);
            Session["AUTH"] = Auth;

            return RedirectToAction("Home");
        }
        public ActionResult C(string i)
        {
            try
            {
                User user = db.Users.Find(int.Parse(DEL.Decrypt(i)));
                if(user!=null)
                {
                    user.Active = true;
                }
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string color = db.MemberShips.Find(user.Member_ID).color;
                AuthUser Auth = new AuthUser(user.ID, user.Name, user.Member_ID, color, user.Face_ID);
                Session["AUTH"] = Auth;
                return RedirectToAction("Home");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }
        public ActionResult SignIn()
        {
            if (string.IsNullOrEmpty(Request.QueryString["access_token"])) { return View(); }; //ERROR! No token returned from Facebook!!

            //let's send an http-request to facebook using the token            
            string json = GetFacebookUserJSON(Request.QueryString["access_token"]);

            //and Deserialize the JSON response
            JavaScriptSerializer js = new JavaScriptSerializer();
            FacebookUser oUser = js.Deserialize<FacebookUser>(json);
            if (db.Users.Any(x => x.Face_ID == oUser.id.ToString()&&x.Active==true))
            {
                User F_User = db.Users.Where(x => x.Face_ID == oUser.id.ToString()).First();
                string color = db.MemberShips.Find(F_User.Member_ID).color;
                AuthUser Auth = new AuthUser(F_User.ID, F_User.Name, F_User.Member_ID, color, F_User.Face_ID);
                Session["AUTH"] = Auth;

                return RedirectToAction("Home");
            }
            else
            {
                User user = new User();
                user.Name = oUser.name;
                user.Email = oUser.email;
                user.Face_ID = oUser.id.ToString();
                Session["FaceBook"] = user;
                return RedirectToAction("MemberShips");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SignIn(string Email , string Password)
        {
            if(db.Users.Any(x=>x.Email==Email&&x.Password==Password && x.Active == true))
            {
                User user = db.Users.Single(x => x.Email == Email && x.Password == Password);
                string color = db.MemberShips.Find(user.Member_ID).color;
                AuthUser Auth = new AuthUser(user.ID, user.Name, user.Member_ID, color, user.Face_ID);
                Session["AUTH"] = Auth;
                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.error = "Invalid Email And Password !!";
                return View();
            }
        }
        public ActionResult SignOut()
        {
            if(Session["AUTH"]!=null)
            {
                Session.Remove("AUTH");
            }
            return RedirectToAction("SignIn");
        }
        private static string GetFacebookUserJSON(string access_token)
        {
            string url = string.Format("https://graph.facebook.com/me?access_token={0}&fields=email,name,first_name,last_name,link", access_token);

            WebClient wc = new WebClient();
            Stream data = wc.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();

            return s;
        }
        public ActionResult Language(String Code)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Code);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Code);

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = Code;
            Response.Cookies.Add(cookie);
            return RedirectToAction("Home");
        }
        public ActionResult ProfilePage()
        {
            if(Session["AUTH"]!=null)
            {
                int id = (Session["AUTH"] as AuthUser).ID;
                User user = db.Users.Find(id);
                return View(user);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfilePage(string Name,string Password,string Email,string Phone,string Country,DateTime DateOfBirth,int Gender,HttpPostedFileBase photo)
        {
            if(Session["AUTH"]!=null)
            {
                if(Email.Contains("@")==false)
                {
                    TempData["Save"] = SiteResource.VaildEmail;
                    return RedirectToAction("ProfilePage");
                }

                try
                {
                    int id = (Session["AUTH"] as AuthUser).ID;
                    if (db.Users.Any(x => x.Email == Email && x.Password == Password && x.ID !=id))
                    {
                        TempData["Save"] = SiteResource.emailused;
                        return RedirectToAction("ProfilePage");
                    }
                    User user = db.Users.Find(id);
                    user.Name = Name;
                    user.Password = Password;
                    user.Email = Email;
                    user.Phone = Phone;
                    user.Country = Country;
                    user.DateOfBirth =DateOfBirth;
                    user.Gender = Gender;
                    db.SaveChanges();
                    AuthUser auth = Session["AUTH"] as AuthUser;
                    auth.Name = Name;
                    Session["AUTH"] = auth;
                    TempData["Save"] = SiteResource.Saved;
                    if (photo != null)
                    {
                        string path = Path.GetExtension(photo.FileName).ToLower();
                        if (path == ".jpg" || path == ".png" || path == ".gif" || path == ".bmp")
                        {
                            photo.SaveAs(Server.MapPath("~/Uploads/Users/" + id + ".jpg"));
                        }
                        else
                        {
                            TempData["Save"] = SiteResource.ImageError;
                        }
                    }
              }
                catch
            {
                    TempData["Save"] = SiteResource.ErrorSaved;
            }
        }
            else
            {
                return RedirectToAction("SignIn");
            }
            return RedirectToAction("ProfilePage");
        }
        public ActionResult Games()
        {
                return View(db.Games.ToList());
        }
        public ActionResult Game(int? id)
        {
                if(id==null)
                {
                    return RedirectToAction("Games");
                }
                Game game = db.Games.Find(id);
                if(game==null)
                {
                    return RedirectToAction("Games");
                }
                db.GameWatch(id);
                return View(game);
        }
        [HttpPost]
        public ActionResult Subscribe(string Email)
        {
                if (Email != string.Empty && Email.Contains("@"))
                {
                    db.Subscribers.Add(new Subscriber { Email = Email });
                    db.SaveChanges();
                }
            
            return RedirectToAction("Home");
        }
        public ActionResult Error()
        {
                return View();
        }
        public ActionResult Winners()
        {
                return View(db.Winners.Where(x => x.Received == true).ToList());
        }
        public ActionResult Pricing()
        {
            if(Session["AUTH"]!=null)
            {
                return View(db.GGBDeals.ToList());
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(int GGBDeal_ID,int? Pay)
        {
            if(Session["AUTH"]!=null)
            {
                if(Pay==null)
                {
                    return RedirectToAction("Pricing");
                }
                try
                {
                    AuthUser auth = (Session["AUTH"] as AuthUser);
                    User user = db.Users.Find(auth.ID);
                    GGBDeal GGBDeal = db.GGBDeals.Find(GGBDeal_ID);
                    Users_GGBDeals item = new Users_GGBDeals
                    {
                        User_ID = user.ID, GGBDeal_ID = GGBDeal_ID, Active = false,Pay=Pay.Value
                    };
                    db.Users_GGBDeals.Add(item);
                    db.SaveChanges();
                    string ReturnURL = DEL.Domain + "Home/D?i=" +DEL.encrypt(item.ID.ToString());
                    return Redirect(DEL.PayMethod(Pay.Value, ReturnURL, "USD", GGBDeal.Name, GGBDeal.Price.ToString(), user.Name, user.Name, user.Email, user.Phone, user.Country, "AUS", GGBDeal.Name, GGBDeal.Name, DEL.Domain));
                }
                catch
                {
                    return RedirectToAction("Pricing");
                }
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public ActionResult D(string i)
        {
            try
            {
                int id =int.Parse(DEL.Decrypt(i));
                Users_GGBDeals Item = db.Users_GGBDeals.Find(id);
                User user = db.Users.Find(Item.User_ID);
                if(Item.Active==false)
                {
                    Item.Active = true;
                if(user.GGB.HasValue)
                {
                    user.GGB = user.GGB + Item.GGBDeal.GGB;

                }
                else
                {
                    user.GGB= Item.GGBDeal.GGB; ;
                }
                db.Entry(Item).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Pricing");
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
            catch
            {
                return RedirectToAction("Home");
            }
        }

        public ActionResult Prizes()
        {
                return View(db.Prizes.ToList());
        }
        public ActionResult Prize(int? id,int?Pay)
        {
            if(Session["AUTH"]!=null)
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                if (id==null)
                {
                    return RedirectToAction("Prizes");
                }
                Prize prize = db.Prizes.Find(id);
                if(prize==null)
                {
                    return RedirectToAction("Prizes");
                }
                if(prize.Price.HasValue)
                {
                    if(prize.Price.Value!=0)
                    {
                        ViewData["Pay"] = Pay;
                    }
                }
                s.Stop();
                return View(prize);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prize(int? Prize_ID, int? Pay,string Phone,string Password,string PayzaEmail=null)
        {
            if (Session["AUTH"] != null)
            {
                Prize prize = db.Prizes.Find(Prize_ID);
                if (db.Gamers.Any(x => x.Phone == Phone && x.Password == Password))
                {
                    if (prize == null)
                    {
                        return RedirectToAction("Prizes");
                    }
                    else
                    {
                        Gamer gamer = db.Gamers.Single(x => x.Phone == Phone && x.Password == Password);
                        User user = db.Users.Find((Session["AUTH"] as AuthUser).ID);
                        if (prize.Price.HasValue)
                        {
                            if (prize.Price.Value != 0)
                            {
                                if (Pay == null)
                                {
                                    return RedirectToAction("Prizes");
                                }
                                else
                                {
                                    try
                                    {
                                        if (gamer.Points >= prize.Points)
                                        {
                                            MemberShip Member = db.MemberShips.Find(user.Member_ID);
                                            if (user.Money_Token + prize.Price.Value <= Member.Limit_Token_Money)
                                            {
                                                //pay

                                                //if pay done (add gamer money token & add user money token )
                                                //if pay done (decrease Gamer Points)
                                                //if pay done (ADD WINNER)
                                            }
                                            else
                                            {
                                                ViewBag.error = SiteResource.MoneytokenError;
                                            }
                                        }
                                        else
                                        {
                                            ViewBag.error = SiteResource.prizeserrorpoints;
                                        }
                                    }
                                    catch { }
                                }
                            }
                            else
                            {
                                ViewBag.error = DEL.Redeem(gamer, prize, user, db);
                            }
                        }
                        else
                        {
                            ViewBag.error = DEL.Redeem(gamer, prize, user, db);
                        }
                    }
                }
                else
                {
                    ViewBag.error = SiteResource.Prizeerror;
                }
                return View(prize);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }

        public ActionResult Deals()
        {
            if(Session["AUTH"]!=null)
            {
                return View(db.Deals.ToList());
            }
            else
            {
                return RedirectToAction("SignIn");
            }                
        }
        public ActionResult Deal(int? id)
        {
            if (Session["AUTH"] != null)
            {
                if (id == null)
                {
                    return RedirectToAction("Deals");
                }
                Deal Deal = db.Deals.Find(id);
                if (Deal == null)
                {
                    return RedirectToAction("Deals");
                }
                return View(Deal);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deal(int Deal_ID,string Phone,string Password,string Code)
        {
            if (Session["AUTH"] != null)
            {
                Deal deal = db.Deals.Find(Deal_ID);
                if (deal == null)
                {
                    return RedirectToAction("Deals");
                }
                if (db.Gamers.Any(x => x.Phone == Phone && x.Password == Password))
                {
                    if (db.Gamers.Any(x => x.Phone == Phone && x.Password == Password && x.Game_ID == deal.Game_ID))
                    {
                        if (db.Deal_Gamers.Any(x => x.Code == Code))
                        {
                            ViewBag.error = SiteResource.errorcode;
                        }
                        else
                        {
                            try
                            {
                                AuthUser auth = (Session["AUTH"] as AuthUser);
                                User user = db.Users.Find(auth.ID);
                                int GGB = deal.Deal_Member.Single(x => x.Member_ID == user.Member_ID).GGB;
                                Gamer gamer = db.Gamers.Single(x => x.Phone == Phone && x.Password == Password && x.Game_ID == deal.Game_ID);
                                if (user.GGB.HasValue)
                                {
                                    if (user.GGB >= GGB)
                                    {
                                        Deal_Gamers Item = new Deal_Gamers
                                        {
                                            Gamer_ID = gamer.ID,
                                            Deal_ID = deal.ID,
                                            Code = Code
                                        };
                                        user.GGB = user.GGB - GGB;
                                        db.Deal_Gamers.Add(Item);
                                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                        db.SaveChanges();
                                        ViewBag.error = SiteResource.DealDone;
                                    }
                                    else
                                    {
                                        ViewBag.error = SiteResource.DealError;
                                    }
                                }
                                else
                                {
                                    ViewBag.error = SiteResource.DealError;
                                }
                            }
                            catch
                            {
                                ViewBag.error = SiteResource.errorPay;
                            }
                        }
                    }
                    else
                    {
                        ViewBag.error = SiteResource.errorgame;
                    }
                }
                else
                {
                    ViewBag.error = SiteResource.errorgamer;
                }
                return View(deal);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }

        public ActionResult ADClick(int?id)
        {
                if(id==null)
                {
                    return RedirectToAction("Home");
                }
                AD ad = db.ADS.Find(id);
                if(ad==null)
                {
                    return RedirectToAction("Home");
                }
                db.ADClick(ad.ID);
                return Redirect(ad.Link);
        }
    }
}