using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Exceptions
{
    public class RequestDoesNotExistException : Exception
    {

        public RequestDoesNotExistException() : base(String.Format($"Friend request does not exist"))
        {

        }

       
    }
}
