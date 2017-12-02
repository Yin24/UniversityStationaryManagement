using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.Exceptions
{
    public class RequestAlreadyApprovedException : ApplicationException
    {
        public RequestAlreadyApprovedException()
        {
        }
        public RequestAlreadyApprovedException(string message) : base(message)
        {
        }
        public RequestAlreadyApprovedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}