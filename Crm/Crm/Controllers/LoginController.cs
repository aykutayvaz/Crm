using Crm.Models;
using Crm.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Crm.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            ViewBag.notificationmessage = null;
            if (Request.Cookies["User"] != null)
            {
                if (!String.IsNullOrEmpty(Request.Cookies["User"].Value.ToString()))
                {
                    String sUser = Request.Cookies["User"].Value as String;
                    //change the language of the application to the chosen
                    //save it to cookie
                    string[] parameters = sUser.Split(',');
                    SimpleSessionPersister.UserId = parameters[0];
                    SimpleSessionPersister.Username = parameters[1];
                    SimpleSessionPersister.Role = parameters[2];
                    return RedirectToAction("Index", "Home");
                    // Response.Redirect
                }
            }
            

            
            return View();
        }

        //
        // GET: /Login/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Login/Create

        [HttpPost]
        public string Register(FormCollection collection)
        {
            Thread.Sleep(500);
            UserFunctions userclass = new UserFunctions();
            try
            {
                string name = collection["NickName"];
                string email = collection["Email"];
                string password = collection["Password"];
                userclass.Register(name, email, password);
               

            }
            catch (Exception e)
            {
                userclass.Jsonmodel.Message = "Kayıt başarısız";
                userclass.Jsonmodel.Success = "fail";
            }
            return userclass.ReturnJson();
                // TODO: Add insert logic here
        }

        [HttpPost]
        public string Post(FormCollection collection)
        {
            Thread.Sleep(500);
            string check=null;
            UserFunctions userclass = new UserFunctions();

            try
            {
                string email = collection["Email"];
                string password = collection["Password"];
                if(!string.IsNullOrEmpty(collection["RememberMe"]))
                    check = collection["RememberMe"];

                if (userclass.Login(email, password, check))
                {

                    //Cookie remember aktifse
                    if (!string.IsNullOrEmpty(check) && check.CompareTo("on") == 0)
                    {
                        string sUser = SimpleSessionPersister.UserId + "," + SimpleSessionPersister.Username + "," + SimpleSessionPersister.Role;
                        HttpCookie myCookie = new HttpCookie("User");
                        myCookie.Value = sUser;
                        myCookie.Expires = DateTime.Now.AddHours(6);
                        Response.Cookies.Add(myCookie);
                    }
                }

            }
            catch(Exception e)
            {
                userclass.Jsonmodel.Message = "Giriş başarısız";
                userclass.Jsonmodel.Success = "fail";
            }
            return userclass.ReturnJson();
        }

        [HttpPost]
        public string Logout()
        {
            string success = "success";
            string message = "Başarıyla çıkış yapıldı";
            try
            {
                if (Request.Cookies["User"] != null)
                {
                    var c = new HttpCookie("User");
                    c.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(c);
                }
                SimpleSessionPersister.UserId = null;
                SimpleSessionPersister.Username = null;
                SimpleSessionPersister.Role = null;
                Session.RemoveAll();

            }
            catch (Exception)
            {
                success = "fail";
                message = "Çıkış sırasında hata ile karşılaşıldı";
            }
            string json = "";
            var x = new
            {
                success = success,
                message = message
            };

            //var x = new { value = value };
            json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(x);
            return json;
        
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public string ForgotPass(FormCollection collection)
        {
            try
            {
                //var fromAddress = new MailAddress("pau.aykutayvaz@gmail.com", "AykutAYVAZ");
                //var toAddress = new MailAddress("aa@fides.com.tr", "To Name");
                //const string fromPassword = "special04x";

                UserFunctions usercontrol = new UserFunctions();
                if (!string.IsNullOrEmpty(collection["email"]))
                {
                    string email = collection["email"];
                    if (usercontrol.isUsedEmail(email))
                    {
                        string verificationurl = usercontrol.CreateVerification(email, Request.Url.Authority);
                        


                        var fromAddress = new MailAddress("aa@fides.com.tr", "AykutAYVAZ");
                        var toAddress = new MailAddress(email, "Sayın kullanıcı");
                        const string fromPassword = "aa@*2015";

                        const string subject = "Şifre Sıfırlama";

                        string url = "<a href='" + verificationurl + "'>" + verificationurl + "</a>";

                        string messagebody = "Şifre sıfırlama isteği: \n " + "Alttaki linkten şifrenizi sıfırlayabilirsiniz \n" + url;

                        string body = String.Format(messagebody);

                       
                        
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.fides.com.tr",
                            Port = 587,
                            EnableSsl = false,//EnableSsl = true gmailde
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                            Timeout = 20000
                            
                        };
                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            IsBodyHtml=true,
                            Subject = subject,
                            Body = body
                        })
                        {
                            smtp.Send(message);
                        }
                        return "success";
                    }
                    return "fail";
                }
            }
            catch (Exception)
            {
                return "fail";
            }

            return "fail";
        }

        public ActionResult ChangePassword()
        {
            //if (string.IsNullOrEmpty(Request.QueryString["code"]))
            //    return RedirectToAction("Index", "AccessDenied");
            //else 
            //{
                UserFunctions usercontrol = new UserFunctions();
                string verification = Request.QueryString["code"];
                dt_User user = usercontrol.FindUserByVerification(verification);
                if (user != null)
                {
                //    ViewBag.userid = user.UserId;
                    ViewBag.username = "12";
                //    return View();
                }
                ViewBag.username = verification;
            //    else
            //    {
            //        return RedirectToAction("Index", "AccessDenied");
            //    }
            //}
            return View();
        }
    }
}
