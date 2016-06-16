using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Crm.Security
{
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (!string.IsNullOrEmpty(SimpleSessionPersister.Username))
            {
                //if(!string.IsNullOrEmpty(SimpleSessionPersister.Roles))

                filterContext.HttpContext.User = new CustomPrincipal(new CustomIdentity(SimpleSessionPersister.Username,SimpleSessionPersister.Role));
                //if (string.IsNullOrEmpty(SimpleSessionPersister.Role) || !User.IsInRole(SimpleSessionPersister.Role))
                //{
                //    //filterContext.Result = new RedirectToRouteResult(new RedirectToRouteResult(new RouteValueDictionary(new { controller ="Account",action ="Index"}),"http")));
                
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                //    {
                //        action = "Index",
                //        controller = "AccessDenied",
                //        area = ""
                //    }));
                //}

            }
            base.OnAuthorization(filterContext);
        }
    }
}