using Crm.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Crm
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_BeginRequest(object sender, EventArgs e)
        {
            //if user is changing language
            //if (HttpContext.Current.Request.Cookies["User"] != null)
            //{
            //    if (!String.IsNullOrEmpty(HttpContext.Current.Request.Cookies["User"].Value.ToString()))
            //    {
            //        String sUser = HttpContext.Current.Request.Cookies["User"].Value as String;
            //        //change the language of the application to the chosen
            //        System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            //        //save it to cookie
            //        string[] parameters = sUser.Split(',');
            //        SimpleSessionPersister.UserId = parameters[0];
            //        SimpleSessionPersister.Username = parameters[1];
            //        SimpleSessionPersister.Role = parameters[2];
            //    }
            //}
        }
    }
}