using Crm.Models;
using Crm.Security;
using System;
using System.Collections.Generic;
using System.Linq;
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



      


       
    }
}
