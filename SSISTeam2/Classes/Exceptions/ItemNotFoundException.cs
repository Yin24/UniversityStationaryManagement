using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.Exceptions
{
    public class ItemNotFoundException : ApplicationException
    {
        public ItemNotFoundException()
        {
        }
        public ItemNotFoundException(string message) : base(message)
        {
        }
        public ItemNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}