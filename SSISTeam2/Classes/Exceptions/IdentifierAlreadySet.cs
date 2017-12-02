using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Exceptions
{
    class IdentifierAlreadySetException : ApplicationException
    {
        public IdentifierAlreadySetException()
        {
        }
        public IdentifierAlreadySetException(string message) : base(message)
        {
        }
        public IdentifierAlreadySetException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
