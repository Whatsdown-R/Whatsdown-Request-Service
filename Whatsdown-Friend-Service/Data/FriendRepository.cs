using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Models;

namespace Whatsdown_Friend_Service
{
    public class FriendRepository
    {
        FriendContext friendContext;

        public FriendRepository(FriendContext auth)
        {

            this.friendContext = auth;
        }
        public void SaveRelationship(Relationship request)
        {
            friendContext.Relationships.Add(request);
        }

        public Relationship GetRelationship(string userOneID, string userTwoID)
        {
           Relationship relationship = friendContext.Relationships.Where(c => c.UserOneID == userOneID || c.UserTwoID == userOneID).Where(c => c.UserOneID == userTwoID || c.UserTwoID == userTwoID).FirstOrDefault();
            return relationship;
        }

        public List<Relationship> GetAllRelationshipsFromOneUser(string userOneID)
        {
            List<Relationship> relationship = friendContext.Relationships.Where(c => c.UserOneID == userOneID || c.UserTwoID == userOneID).ToList();
            return relationship;
        }

        public List<Relationship> GetAllPendingRelationships(string userID)
        {
            List<Relationship> relationships = friendContext.Relationships.Where(c => c.UserOneID == userID || c.UserTwoID == userID).Where(c => c.ActionUserID != userID).ToList();
            return relationships;
        }

    }
}
