using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Exceptions;
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

            if (SenderID == null || receiverID == null)
                throw new ArgumentNullException();

            if (SenderID == ""|| receiverID == "" )
                throw new ArgumentException();


            Relationship relation = friendRepository.GetRelationship(SenderID, receiverID);

            //Create Exception
            if (relation != null)
                throw new RequestAlreadyExistException(SenderID, receiverID);

            relation = new Relationship(Guid.NewGuid().ToString(),SenderID, receiverID, SenderID, "PENDING");
            friendRepository.SaveRelationship(relation);
          

        }

        public void AcceptFriend(string userID, string friendID)
        {
            Relationship relation = friendRepository.GetRelationship(userID, friendID);

            if (relation == null)
                throw new RequestDoesNotExistException();

            if (relation.ActionUserID != friendID)
                return;

            Relationship newRelation = new Relationship(relation.ID, relation.UserOneID, relation.UserTwoID, relation.ActionUserID, "ACCEPTED");
            

            friendRepository.Update(newRelation);
        }

        public void DenyFriend(string userID, string actionUserID)
        {
            Relationship relation = friendRepository.GetRelationship(userID, actionUserID);

            if (relation == null)
                throw new RequestDoesNotExistException();

            if (relation.ActionUserID != actionUserID)
                return;

            if (relation.Status != "PENDING")
                return;

            Relationship newRelation = new Relationship(relation.ID, relation.UserOneID, relation.UserTwoID, relation.ActionUserID, "DENIED");

            friendRepository.Update(newRelation);
        }

        public void BlockFriend(string userID, string actionUserID)
        {
            Relationship relation = friendRepository.GetRelationship(userID, actionUserID);

            if (relation == null)
                throw new RequestDoesNotExistException();

            Relationship newRelation = new Relationship(relation.ID, relation.UserOneID, relation.UserTwoID, relation.ActionUserID, "BLOCKED");


            friendRepository.Update(newRelation);
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
