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

            relation = new Relationship(SenderID, receiverID, SenderID, "PENDING");
            friendRepository.SaveRelationship(relation);

        }

        public void AcceptFriend(string userID, string actionUserID)
        {
            Relationship relation = friendRepository.GetRelationship(userID, actionUserID);

            if (relation == null)
                return;

            if (relation.ActionUserID != actionUserID)
                return;

            relation.Status = "ACCEPTED";

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

            relation.Status = "DENIED";

            friendRepository.SaveRelationship(relation);
        }

        public void BlockFriend(string userID, string actionUserID)
        {
            Relationship relation = friendRepository.GetRelationship(userID, actionUserID);

            if (relation == null)
                return;

            relation.Status = "BLOCKED";

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
    }
}
