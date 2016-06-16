using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Routing;

namespace Crm.Security
{
    public class CustomAuthorize :AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.User = new CustomPrincipal(new CustomIdentity(SimpleSessionPersister.Username, SimpleSessionPersister.Role));
            
            if (string.IsNullOrEmpty(SimpleSessionPersister.Username))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        action = "Index",
                        controller = "Login",
                        area = ""
                    }));
            }
            else if(string.IsNullOrEmpty(SimpleSessionPersister.Role))
            {
                 filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        action = "Index",
                        controller = "AccessDenied",
                        area = ""
                    }));
            }
            else if (!AuthenticateRole(Roles))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "AccessDenied",
                    area = ""
                }));
            }
            else
            {
            
                  //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                  //  {
                  //      action = "Index",
                  //      controller = "Home",
                  //      area = ""
                  //  }));
            }

               
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
        private bool AuthenticateRole(string Role)
        {
            string[] Roles = Role.Split(',');
            if (Roles.Contains(SimpleSessionPersister.Role))
                return true;
            return false;
        }
    }
}