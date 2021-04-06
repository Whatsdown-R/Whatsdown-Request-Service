using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Models;

namespace Whatsdown_Friend_Service
{
    public class RelationshipLogic
    {
        FriendRepository friendRepository;
        public RelationshipLogic(FriendContext _context)
        {
            this.friendRepository = new FriendRepository(_context);
        }

        public void RequestFriend(string SenderID, string receiverID )
        {
            Relationship relation = friendRepository.GetRelationship(SenderID, receiverID);

            //Create Exception
            if (relation != null)
                return;

            relation = new Relationship(Guid.NewGuid().ToString(),SenderID, receiverID, SenderID, "PENDING");
            friendRepository.SaveRelationship(relation);
          

        }

        public void AcceptFriend(string userID, string actionUserID)
        {
            Relationship relation = friendRepository.GetRelationship(userID, actionUserID);

            if (relation == null)
                return;

            if (relation.ActionUserID != actionUserID)
                return;

            Relationship newRelation = new Relationship(relation.ID, relation.UserOneID, relation.UserTwoID, relation.ActionUserID, "ACCEPTED");
            

            friendRepository.SaveRelationship(relation);
        }

        public void DenyFriend(string userID, string actionUserID)
        {
            Relationship relation = friendRepository.GetRelationship(userID, actionUserID);

            if (relation == null)
                return;

            if (relation.ActionUserID != actionUserID)
                return;

            if (relation.Status != "PENDING")
                return;

            Relationship newRelation = new Relationship(relation.ID, relation.UserOneID, relation.UserTwoID, relation.ActionUserID, "DENIED");

            friendRepository.SaveRelationship(newRelation);
        }

        public void BlockFriend(string userID, string actionUserID)
        {
            Relationship relation = friendRepository.GetRelationship(userID, actionUserID);

            if (relation == null)
                return;

            Relationship newRelation = new Relationship(relation.ID, relation.UserOneID, relation.UserTwoID, relation.ActionUserID, "BLOCKED");
          

            friendRepository.SaveRelationship(relation);
        }
        public List<Relationship> GetFriends(string userID)
        {
            List<Relationship> relationships = friendRepository.GetAllRelationshipsFromOneUser(userID);
            return relationships;
        }

        public List<Relationship> GetPendingFriends(string userID)
        {
            List<Relationship> relationships = friendRepository.GetAllPendingRelationships(userID);
            return relationships;
        }

        public Relationship GetFriend(string userID, string otherUserID)
        {
            return friendRepository.GetRelationship(userID, otherUserID);
        }
    }
}
