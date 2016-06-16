using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm.Security
{
    public class SimpleSessionPersister
    {
        static string usernameSessionVar = "username";
        static string useridSessionVar = "userid";
        static string role;

        public static string Role
        {
            get { return SimpleSessionPersister.role; }
            set { SimpleSessionPersister.role = value; }
        }
        public static string Username
        {
            get
            {
                if (HttpContext.Current == null) return string.Empty;
                    var sessionVar = HttpContext.Current.Session[usernameSessionVar];
                    if (sessionVar != null)
                        return sessionVar as string;
                    return null;
            }
            set 
            {
                HttpContext.Current.Session[usernameSessionVar] = value;
            }

        }
        public static string UserId
        {
            get
            {
                if (HttpContext.Current == null) return string.Empty;
                var sessionVar = HttpContext.Current.Session[useridSessionVar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session[useridSessionVar] = value;
            }

        }
    }
}