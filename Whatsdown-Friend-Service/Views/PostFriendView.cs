using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Views
{
    public class PostFriendView
    {
        public string UserOneID { get;  set; }
        public string UserTwoID { get;  set; }
        public string ActionUserID { get;  set; }
        public string Status { get;  set; }

        public PostFriendView()
        {
        }

        public PostFriendView(string userOneID, string userTwoID, string actionUserID, string status)
        {
            UserOneID = userOneID;
            UserTwoID = userTwoID;
            ActionUserID = actionUserID;
            Status = status;
        }
    }
}
