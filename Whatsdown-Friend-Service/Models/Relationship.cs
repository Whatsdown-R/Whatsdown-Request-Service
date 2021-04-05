using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Models
{
    public class Relationship
    {
        
        public string UserOneID;
        public string UserTwoID;
        public string ActionUserID;
        public string Status;

        public Relationship()
        {
        }

        public Relationship(string userOneID, string userTwoID, string actionUserID, string status)
        {
            UserOneID = userOneID;
            UserTwoID = userTwoID;
            ActionUserID = actionUserID;
            Status = status;
        }
    }
}
