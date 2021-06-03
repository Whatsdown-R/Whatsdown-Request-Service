using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Views
{
    public class BasicFriendView
    {
       public string profileId { get; set; }
       public string relationCode { get; set; }

        public BasicFriendView(string userId, string relationCode)
        {
            this.profileId = userId;
            this.relationCode = relationCode;
        }
    }
}
