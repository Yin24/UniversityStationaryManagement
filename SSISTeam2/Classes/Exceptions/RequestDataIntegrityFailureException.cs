using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.Exceptions
{
    public class RequestDataIntegrityFailureException : ApplicationException
    {
        public RequestDataIntegrityFailureException()
        {
        }
        public RequestDataIntegrityFailureException(string message) : base(message)
        {
        }
        public RequestDataIntegrityFailureException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}