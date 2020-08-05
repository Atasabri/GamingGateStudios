using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using GamingGateStudios.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GamingGateStudios
{
    public class DEL
    {
        //Keys
        private static string CipherKey = ConfigurationManager.AppSettings["Cipher"].ToString();
        private static string PayZaEmail = ConfigurationManager.AppSettings["PayZaEmail"].ToString();
        private static string PayZaPassword = ConfigurationManager.AppSettings["PayZaPassword"].ToString();
        private static string PayZaWebsiteURL = ConfigurationManager.AppSettings["PayZaWebsiteURL"].ToString();
        private static string PayTabMerchantEmail = ConfigurationManager.AppSettings["PayTabMerchantEmail"].ToString();
        private static string PayTabSecretKey = ConfigurationManager.AppSettings["PayTabSecretKey"].ToString();
        private static string PayTabWebsiteURL = ConfigurationManager.AppSettings["PayTabWebsiteURL"].ToString();
        public static string Domain = ConfigurationManager.AppSettings["Domain"].ToString();
        public static string TestMode= ConfigurationManager.AppSettings["TestMode"].ToString();
        //Functions
        public static void Send_Mail(string Subject, HttpPostedFileBase file, List<string> To)
        {
            if (To.Count > 0)
            {
                string From = ConfigurationManager.AppSettings["Email"].ToString();
                string Pass = ConfigurationManager.AppSettings["Password"].ToString();
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(From);
                foreach (var item in To)
                {
                    if (item.Contains("@"))
                    {
                        mail.To.Add(item);
                    }
                }
                mail.Subject = Subject;
                StreamReader read = new StreamReader(file.InputStream);
                mail.Body = read.ReadToEnd();
                mail.IsBodyHtml = true;
                ///-------------------------------------------------------------------------//
                SmtpClient smtpMail = new SmtpClient();
                smtpMail.EnableSsl = true;                 ///
                smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpMail.Host = "smtp.gmail.com";          ///
                smtpMail.Port = 587;                       ///

                smtpMail.UseDefaultCredentials = false;
                smtpMail.Credentials = new NetworkCredential(From, Pass);
                ///-------------------------------------------------------------------------//
                smtpMail.Send(mail);
            }
        }

        public static string Redeem(Gamer gamer,Prize prize,User user,DB db)
        {
            try
            {
                if (gamer.Points.HasValue)
             {
                if (gamer.Points >= prize.Points)
                {
                    Winner winner = new Winner
                    {
                        User_ID = user.ID,
                        Prize_ID = prize.ID,
                        Gamer_ID = gamer.ID,
                        Received = false
                    };
                    int? points = gamer.Points - prize.Points;
                    gamer.Points = points;
                    db.Entry(gamer).State = System.Data.Entity.EntityState.Modified;
                    db.Winners.Add(winner);
                    db.SaveChanges();
                    return SiteResource.prizedone;
                }
                else
                {
                    return SiteResource.prizeserrorpoints;
                }
            }
            else
            {
                return SiteResource.prizeserrorpoints;
            }
            }
            catch
            {
                return SiteResource.redeemerror;
            }
        }

        public static AD GetAD (int width,int height)
        {    
            DB db = new DB();
            List<AD> ADS = db.ADS.Where(x => x.Width == width && x.Height == height 
            && x.Enable == true).ToList();
            ADS = ADS.Where(x => x.Ads_Clients.Target_CPM > x.Ads_Clients.ADS.Select(m => m.CPM)
              .Aggregate((a, b) => a + b)).ToList();
            if (ADS.Count==0)
            {
                return null;
            }            
            return randomAD(ADS, db);
        }
        public static AD randomAD(List<AD> ADS,DB db)
        {
            if(ADS.Count==0)
            {
                return null;
            }
            Random random = new Random();
            int ID = random.Next(ADS.First().ID, ADS.Last().ID+1);
            AD ad = db.ADS.Find(ID);

            if (ad == null)
            {
                return randomAD(ADS,db);
            }
            else if (ADS.Contains(ad) == false )
            {
                return randomAD(ADS, db);
            }
            else if(ISVALID(ad) == false)
            {
                ADS.Remove(ad);
                return randomAD(ADS, db);
            }
            else
            {
                db.ADWatch(ad.ID);
                return ad;
            }
        }
        public static bool ISVALID(AD ad)
        {
            if (ad.Ads_Clients.Agency == null)
            {
                return true;
            }
            else
            {
                Agency agency = ad.Ads_Clients.Agency;
                int Actual_CPM = 0;
                foreach (var item in agency.Ads_Clients)
                {
                    Actual_CPM += item.ADS.Select(x => x.CPM).Aggregate((a, b) => a + b);
                }
                if(agency.Target_CPM>Actual_CPM)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string encrypt(string encryptString)
        {
            string EncryptionKey = CipherKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = CipherKey;
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
           });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string PayMethod(int Pay,string ReturnUrl,
            string Currency,string ItemName,string Paid,
            string Fname=null,string Lname=null,string Email=null,
            string Phone=null,string City=null,string CountryCode=null,
            string ItemDescription=null,string ItemCode=null,
            string CancelUrl = null)
        {
            if(Pay==1)
            {
                #region Payza
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                            | SecurityProtocolType.Tls11
                            | SecurityProtocolType.Tls12
                            | SecurityProtocolType.Ssl3;
                string URL = PayZaMethod(PayZaEmail,
                PayZaPassword, PayZaWebsiteURL, "other", ItemName,
                 ItemDescription, ItemCode, Convert.ToDouble(Paid), Currency, 0, 0, 0, ReturnUrl,
                CancelUrl, "No Info", "No Info",
                "No Info", "No Info", "No Info", "No Info");
                if (URL != null)
                {
                    try
                    {
                        string Token = URL.Split('&')[2];
                        return "https://secure.payza.com/checkout?ap_productid=" + Token.Substring(6)+ "&ap_testmode="+TestMode+"&ap_quantity=1";
                    }
                    catch
                    {
                        return "error";
                    }
                }
                else
                {
                    return "error";
                }
                #endregion
            }
            else if(Pay==2)
            {
                #region PayTab
                var objrequest = new PayPageRequest();
                objrequest.MerchantEmail = PayTabMerchantEmail;
                objrequest.SecretKey = PayTabSecretKey;
                objrequest.Currency = Currency;
                objrequest.Amount = Paid;
                objrequest.SiteUrl = PayTabWebsiteURL;
                objrequest.Title = ItemName;
                objrequest.Quantity = "1";
                objrequest.UnitPrice = Paid;
                objrequest.ProductsPerTitle = ItemName;
                objrequest.ReturnUrl = ReturnUrl;
                objrequest.CcFirstNname = Fname;
                objrequest.CcLastName = Lname;
                objrequest.CcPhoneNumber = Phone;
                objrequest.Phonenumber = Phone;
                objrequest.BillingAddress = City;
                objrequest.City = City;
                objrequest.State = City;
                objrequest.PostalCode = "32512";
                objrequest.Country = CountryCode;
                objrequest.Email = Email;
                objrequest.AddressShipping = City;
                objrequest.CityShipping = City;
                objrequest.StateShipping = City;
                objrequest.PostalCodeShipping = "32512";
                objrequest.CountryShipping = CountryCode;
                objrequest.PaymentReference = GenerateReferenceNumber();
                objrequest.PaymentDate = DateTime.Now;

                return PayTabMethod(objrequest);
                #endregion
            }
            return "";
        }
           
        //PayTab Methods 
        public static string MakeWebServiceCall(string methodCall, string formContent)
    {
        formContent += "&p_id=" + 10021041;
        string ApiURL = "https://www.paytabs.com/apiv2/";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiURL + methodCall);
        request.Method = "POST";

        byte[] byteArray;
        WebResponse response;
        StreamReader reader;
        Stream dataStream;
        string responseFromServer;

        byteArray = Encoding.UTF8.GetBytes(formContent);
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;
        dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        response = request.GetResponse();
        dataStream = response.GetResponseStream();
        reader = new StreamReader(dataStream);
        responseFromServer = HttpUtility.UrlDecode(reader.ReadToEnd());

        reader.Close();
        dataStream.Close();
        response.Close();

        return responseFromServer;
    }
        public static string CreatePayPage(PayPageRequest objPayPageRequest)
    {
        return "merchant_email=" + objPayPageRequest.MerchantEmail
                            + "&secret_key=" + objPayPageRequest.SecretKey
                            + "&currency=" + objPayPageRequest.Currency
                            + "&amount=" + objPayPageRequest.Amount
                            + "&site_url=" + objPayPageRequest.SiteUrl
                            + "&title=" + objPayPageRequest.ProductsPerTitle
                            + "&quantity=" + objPayPageRequest.Quantity
                            + "&unit_price=" + objPayPageRequest.UnitPrice
                            + "&products_per_title=" + objPayPageRequest.ProductsPerTitle
                            + "&return_url=" + objPayPageRequest.SiteUrl
                            + "&cc_first_name=" + objPayPageRequest.CcFirstNname
                            + "&cc_last_name=" + objPayPageRequest.CcLastName
                            + "&cc_phone_number=" + objPayPageRequest.CcPhoneNumber
                            + "&phone_number=" + objPayPageRequest.CcPhoneNumber
                            + "&billing_address=" + objPayPageRequest.BillingAddress
                            + "&city=" + objPayPageRequest.City
                            + "&state=" + objPayPageRequest.State
                            + "&postal_code=" + objPayPageRequest.PostalCode
                            + "&country=" + objPayPageRequest.Country
                            + "&email=" + objPayPageRequest.Email
                            + "&ip_customer=" + System.Net.Dns.GetHostName()
                            + "&ip_merchant=100.100.100.100"
                            + "&address_shipping=" + objPayPageRequest.AddressShipping
                            + "&city_shipping=" + objPayPageRequest.CityShipping
                            + "&state_shipping=" + objPayPageRequest.StateShipping
                            + "&postal_code_shipping=" + objPayPageRequest.PostalCodeShipping
                            + "&country_shipping=" + objPayPageRequest.CountryShipping
                            + "&other_charges=0"
                            + "&discount=0"
                            + "&reference_no=" + GenerateReferenceNumber()
                            + "&msg_lang=English"
                            + "&cms_with_version=API";
    }
        public static string GenerateReferenceNumber()
    {
        return "PT" + DateTime.Now.ToString("yyyyMMddHHmmssffffff");
    }
        public static string PayTabMethod(PayPageRequest objrequest)
        {          
            var tmp = new PayPageResonse();
            try
            {

                string serviceResponse = MakeWebServiceCall("create_pay_page", CreatePayPage(objrequest));
                tmp = JsonConvert.DeserializeObject<PayPageResonse>(serviceResponse);

                if (tmp.response_code != null && tmp.response_code != "4012")
                {
                    //code
                }
                else if (tmp.payment_url != "")
                {
                    return tmp.payment_url;
                }
                return "error";
            }
            catch (System.Net.WebException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Payza Methods
        protected static string PayZaMethod(string myUserName,
                      string apiPassword,
                      string strWebsiteUrl,
                      string strPurchaseType,
                      string strItemName,
                      string strItemDescription,
                      string strItemCode,
                      double decAmountPaid,
                      string strCurrency,
                      double decShippingAmount,
                      double decAddAmount,
                      double decTaxAmount,
                      string strReturnUrl,
                      string strCancelUrl,
                      string strApc_1,
                      string strApc_2,
                      string strApc_3,
                      string strApc_4,
                      string strApc_5,
                      string strApc_6)
        {
            string dataToSend; // The data that will be sent to the API
            string Server = "https://api.payza.com"; // The server address of the API
            string Url = "/svc/api.svc/GetPaymentToken"; // The exact URL of the API

            StringBuilder sbDataToSend = new StringBuilder();

            sbDataToSend.AppendFormat("USER={0}&PASSWORD={1}&ap_url={2}&ap_purchasetype={3}&ap_itemname={4}&ap_description={5}&ap_itemcode={6}&ap_amount={7}&ap_currency={8}&ap_shippingcharges={9}&ap_additionalcharges={10}&ap_taxamount={11}&ap_returnurl={12}&ap_cancelurl={13}&apc_1={14}&apc_2={15}&apc_3={16}&apc_4={17}&apc_5={18}&apc_6={19}",
                                      HttpUtility.UrlEncode(myUserName), HttpUtility.UrlEncode(apiPassword),
                                      HttpUtility.UrlEncode(strWebsiteUrl), HttpUtility.UrlEncode(strPurchaseType),
                                      HttpUtility.UrlEncode(strItemName), HttpUtility.UrlEncode(strItemDescription),
                                      HttpUtility.UrlEncode(strItemCode), HttpUtility.UrlEncode(decAmountPaid.ToString()),
                                      HttpUtility.UrlEncode(strCurrency), HttpUtility.UrlEncode(decShippingAmount.ToString()),
                                      HttpUtility.UrlEncode(decAddAmount.ToString()), HttpUtility.UrlEncode(decTaxAmount.ToString()),
                                      HttpUtility.UrlEncode(strReturnUrl), HttpUtility.UrlEncode(strCancelUrl),
                                      HttpUtility.UrlEncode(strApc_1), HttpUtility.UrlEncode(strApc_2),
                                      HttpUtility.UrlEncode(strApc_3), HttpUtility.UrlEncode(strApc_4),
                                      HttpUtility.UrlEncode(strApc_5), HttpUtility.UrlEncode(strApc_6));
            dataToSend = sbDataToSend.ToString();

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = null;

            // send the post
            objRequest = (HttpWebRequest)WebRequest.Create(Server + Url);
            objRequest.Method = "POST";
            objRequest.ContentLength = dataToSend.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";

            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(dataToSend);
            myWriter.Close();

            // read the response
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            StreamReader sr = new StreamReader(objResponse.GetResponseStream());

            string response = sr.ReadLine();
            sr.Close();

            if (response != null)
            {
                // decode the API response string
                response = HttpUtility.UrlDecode(response);
                return response;
            }
            else
            {
                // something is wrong, no response is received from Payza
                return null;
            }
        }
    }
}