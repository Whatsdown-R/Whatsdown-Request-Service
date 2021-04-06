using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Models
{

    public class Relationship
    {
        public string ID { get; private set; }
        public string UserOneID { get; private set; }
        public string UserTwoID { get; private set; }
        public string ActionUserID { get; private set; }
        public string Status { get; private set; }

        public Relationship()
        {
        }

        public Relationship(string ID, string userOneID, string userTwoID, string actionUserID, string status)
        {
            this.ID = ID;
            UserOneID = userOneID;
            UserTwoID = userTwoID;
            ActionUserID = actionUserID;
            Status = status;
        }
        public override string ToString()
        {
            return "ID: " + ID + " | UserOne: " + UserOneID + "| UserTwo: " + UserTwoID + " | action: " + ActionUserID + " | status: " + Status;

        }
    }
}
