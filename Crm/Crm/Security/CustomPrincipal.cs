using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Crm.Security
{
    public class CustomPrincipal:System.Security.Principal.IPrincipal
    {
        public CustomPrincipal(CustomIdentity identity)
        {
            this.Identity = identity;
            
        }

        public IIdentity Identity { get; private set; }


        public bool IsInRole(string role)
        {
            //return true;
            var roles = role.Split(new char[] { ',' });
            return roles.Any(r => this.Identity.AuthenticationType.Contains(role));
        }
    }
}