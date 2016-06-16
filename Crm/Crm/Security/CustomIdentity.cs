using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm.Security
{
    public class CustomIdentity:System.Security.Principal.IIdentity
    {
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }



        public CustomIdentity(string name,string AuthenticationType)
        {
            this.Name = name;
            this.AuthenticationType = AuthenticationType;
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(this.Name); }
            
        }
        
    }
}