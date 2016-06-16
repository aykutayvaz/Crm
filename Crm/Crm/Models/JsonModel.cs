using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm.Models
{
    public class JsonModel
    {
        string success;

        public string Success
        {
            get { return success; }
            set { success = value; }
        }

        string[] result;

        public string[] Result
        {
            get { return result; }
            set { result = value; }
        }

        string htmlresult;

        public string Htmlresult
        {
            get { return htmlresult; }
            set { htmlresult = value; }
        }

        string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}