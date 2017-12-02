using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.Exceptions
{
    public class DateNotFoundException : ApplicationException
    {
        public DateNotFoundException()
        {
        }
        public DateNotFoundException(string message) : base(message)
        {
        }
        public DateNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}