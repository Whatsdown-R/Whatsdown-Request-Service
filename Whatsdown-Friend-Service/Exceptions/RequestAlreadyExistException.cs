using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Exceptions
{
    public class RequestAlreadyExistException : Exception
    {
        public RequestAlreadyExistException()
        {

        }

        public RequestAlreadyExistException(string firstUser, string secondUser)
            : base(String.Format($"Invalid friend request. It already exists with the users:{1} and {2}", firstUser, secondUser))
        {

        }
    }
}
