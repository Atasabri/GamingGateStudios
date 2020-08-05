using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GamingGateStudios.Models;

namespace GamingGateStudios.Controllers
{
    public class ManageGameController : Controller
    {
        DB db = new DB();
        // GET: ManageGame

        [HttpGet]
        public JsonResult SignIn(string phone, string pass, string gameID)
        {
            Dictionary<string, string> Dictionary = new Dictionary<string, string>();
            try
            {
                string Phone = DEL.Decrypt(phone);
                string Pass = DEL.Decrypt(pass);
                int GameID = int.Parse(DEL.Decrypt(gameID));
                if (db.Gamers.Any(x => x.Phone == Phone && x.Password == Pass && x.Game_ID == GameID) == false)
                {
                    if (db.Users.Any(x => x.Phone == Phone && x.Password == Pass))
                    {
                        User user = db.Users.Single(x => x.Phone == Phone && x.Password == Pass && x.Active == true);
                        Game game = db.Games.Find(GameID);
                        if (game != null)
                        {
                            Gamer gamer = new Gamer
                            {
                                UserName = user.Name,
                                Password = user.Password,
                                Country = user.Country,
                                DateOfBirth = user.DateOfBirth,
                                Active = true,
                                Game_ID = GameID,
                                Gender = user.Gender,
                                Tries = game.Start_Tries,
                                Points = game.Start_Points,
                                Phone = user.Phone,
                            };
                            db.Gamers.Add(gamer);
                            db.SaveChanges();
                            Dictionary.Add("Result", "Gamer Added Successfully");
                            Dictionary.Add("UserName", gamer.UserName);
                            Dictionary.Add("Tries", gamer.Tries.ToString());
                            Dictionary.Add("Points", gamer.Points.ToString());
                            db.GamerActive(gamer.ID);
                        }
                        else
                        {
                            Dictionary.Add("Result", "This Game Not Found");
                        }
                    }
                    else
                    {
                        Dictionary.Add("Result", "User Not Found");
                    }
                }
                else
                {
                    Gamer gamer = db.Gamers.Single(x => x.Phone == Phone && x.Password == Pass && x.Game_ID == GameID);
                    Dictionary.Add("Result", "SignIn Done");
                    Dictionary.Add("UserName", gamer.UserName);
                    Dictionary.Add("Tries", gamer.Tries.ToString());
                    Dictionary.Add("Points", gamer.Points.ToString());
                    db.GamerActive(gamer.ID);
                }
            }
            catch
            {
                Dictionary.Add("Result", "Error");
            }
            return Json(Dictionary, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Splashes(string game_id)
        {
            Dictionary<string, object> Dictionary = new Dictionary<string, object>();
            try
            {
                int Game_ID =int.Parse(DEL.Decrypt(game_id));;
                List<Splash> Splashes = db.Splashes.Where(x => x.Game_ID == Game_ID).ToList();
                if (Splashes.Count == 0)
                {
                    Dictionary.Add("Result", "null");
                }
                else
                {
                    Dictionary.Add("Result", Splashes.Count.ToString());
                    Dictionary<string, string> Data = new Dictionary<string, string>();
                    for (int i = 0; i < Splashes.Count; i++)
                    {
                        Data.Add(i.ToString(), Splashes[i].ID.ToString());
                    }
                    Dictionary.Add("Splashes", Data);
                }
            }
            catch
            {
                Dictionary.Add("Results", "Error");
            }
            return Json(Dictionary, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ADS(string width,string height,string num)
        {
            Dictionary<string, object> Dictionary = new Dictionary<string, object>();
            try
            {
                int Width = int.Parse(DEL.Decrypt(width));
                int Height = int.Parse(DEL.Decrypt(height));
                int NumOfADS = int.Parse(DEL.Decrypt(num));
                Dictionary<string, string> Data = new Dictionary<string, string>();
                for (int i = 0; i < NumOfADS; i++)
                {
                    AD ad = DEL.GetAD(Width, Height);
                    if (ad == null)
                    {
                        break;
                    }
                    else
                    {                      
                        Data.Add(i.ToString(), ad.ID.ToString());
                    }
                }
                Dictionary.Add("Result", Data.Count.ToString());
                Dictionary.Add("ADS", Data);
            }
            catch
            {
                Dictionary.Add("Results", "Error");
            }
            return Json(Dictionary, JsonRequestBehavior.AllowGet);
        }


        //Not Finish
        [HttpGet]
        public JsonResult CheckAccount(string Phone,int Game_ID)
        {
            //Check Account
            bool Result = false;
            if (db.Gamers.Any(x=>x.Phone==Phone&&x.Game_ID==Game_ID))
            {
                Result = true;
            }
            Dictionary<string, bool> Dictionary = new Dictionary<string, bool>();
            Dictionary.Add("Result", Result);
            return Json(Dictionary, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PhoneActive(int Phone)
        {
            //Check Phone Active From Operator

            bool Result = true;

            Dictionary<string, bool> Dictionary = new Dictionary<string, bool>();
            Dictionary.Add("Result", Result);
            return Json(Dictionary, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckMember(int Phone)
        {
            //check Member From DB
            string Member = "DB";

            Dictionary<string, string> Dictionary = new Dictionary<string, string>();
            Dictionary.Add("Membership", Member);
            return Json(Dictionary, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelectMembership(int Phone,string Member)
        {
            //Add Member To DB

            Dictionary<string, string> Dictionary = new Dictionary<string, string>();
            Dictionary.Add("Result", "Done");
            return Json(Dictionary, JsonRequestBehavior.AllowGet);
        }
    }
}

