using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;
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
                friendContext.SaveChanges();
        }

        public Relationship GetRelationship(string userOneID, string userTwoID)
        {
           List<Relationship> relationship = friendContext.Relationships.Where(c => c.UserOneID == userOneID || c.UserTwoID == userOneID).Where(c => c.UserOneID == userTwoID || c.UserTwoID == userTwoID).ToList();
            return relationship.FirstOrDefault();
        }

        public Relationship GetRelationshipById(string relationShipId)
        {
            List<Relationship> relationship = friendContext.Relationships.Where(c => c.ID == relationShipId ).ToList();
            return relationship.FirstOrDefault();
        }

        public List<Relationship> GetAllRelationshipsFromOneUser(string userOneID)
        {
            List<Relationship> relationship = friendContext.Relationships.Where(c => c.UserOneID == userOneID || c.UserTwoID == userOneID).ToList();
            return relationship;
        }

        public List<Relationship> GetAllAcceptedRelationshipsFromOneUser(string userOneID)
        {
            List<Relationship> relationship = friendContext.Relationships.Where(c => c.UserOneID == userOneID || c.UserTwoID == userOneID).Where(c => c.Status == "ACCEPTED").ToList();
            return relationship;
        }

        public List<Relationship> GetAllPendingRelationships(string userID)
        {
            List<Relationship> relationships = friendContext.Relationships.Where(c => c.UserOneID == userID || c.UserTwoID == userID).Where(c => c.ActionUserID != userID&& c.Status =="Pending").ToList();
            return relationships;
        }

        public void Update(Relationship rel)
        {
            Relationship relationToUpdate  = friendContext.Relationships.Where(c => c.ID == rel.ID).FirstOrDefault();
            friendContext.Entry(relationToUpdate).CurrentValues.SetValues(rel);
            friendContext.SaveChanges();
        }

    

    }
}
