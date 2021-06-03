using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Exceptions
{
    public class RequestException : Exception
    {
        public RequestException(string message) : base(message) { }
    }
}
