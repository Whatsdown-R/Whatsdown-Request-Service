using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Exceptions;
using Whatsdown_Friend_Service.Models;
using Whatsdown_Friend_Service.Views;

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
            Console.WriteLine("Requesting friend with Id:" + receiverID, " , the sender: " + SenderID);

            if (SenderID == null || receiverID == null)
                throw new ArgumentNullException();

            if (SenderID == ""|| receiverID == "" )
                throw new ArgumentException();


            Relationship relation = friendRepository.GetRelationship(SenderID, receiverID);

            //Create Exception
            if (relation != null)
            {
                Console.WriteLine("Requesting friend with Id:" + receiverID, " , the sender: " + SenderID + "  ALREADY EXISTS");
                throw new RequestAlreadyExistException(SenderID, receiverID);
            
            }
            relation = new Relationship(Guid.NewGuid().ToString(),SenderID, receiverID, SenderID, "PENDING");
            friendRepository.SaveRelationship(relation);
            Console.WriteLine("Succesfully saved relation with relationId: " + relation.ID);
          

        }

        public void AcceptFriend(string userID, string relationshipId)
        {
            Relationship relation = friendRepository.GetRelationshipById(relationshipId);

            if (relation == null)
                throw new RequestDoesNotExistException();

            if (relation.ActionUserID == userID)
                throw new RequestException("User who send request cant accept request");

            if (relation.UserOneID != userID && relation.UserTwoID != userID)
                throw new RequestException("User who is not involved in request cant interact with it");

            if (relation.Status != "PENDING")
                throw new RequestException("Request is not pending");


            Relationship newRelation = new Relationship(relation.ID, relation.UserOneID, relation.UserTwoID, relation.ActionUserID, "ACCEPTED", Guid.NewGuid().ToString());
            

            friendRepository.Update(newRelation);
        }

        public void DenyFriend(string profileId, string relationshipId)
        {
            Relationship relation = friendRepository.GetRelationshipById(relationshipId);

            if (relation == null)
                throw new RequestDoesNotExistException();

           
            if (relation.ActionUserID == profileId)
                throw new RequestException("User who send request cant deny request");

            if (relation.UserOneID != profileId && relation.UserTwoID != profileId)
                throw new RequestException("User who is not involved in request cant interact with it");

            if (relation.Status != "PENDING")
                throw new RequestException("Request is not pending");

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

        public void RemoveFriend(string userId , string friendId)
        {
            Relationship relation = friendRepository.GetRelationship(userId, friendId);
            if (relation == null)
                throw new RequestDoesNotExistException();
            Relationship newRelation = new Relationship(relation.ID,relation.UserOneID, relation.UserTwoID, relation.ActionUserID, "DENIED");
            friendRepository.Update(newRelation);
        }

        public List<BasicFriendView> GetFriends(string userID)
        {
            if (userID == null )
                throw new ArgumentNullException();

            if (userID.Equals(""))
                throw new ArgumentException("Argument may not be empty.");


            List<FriendViewModel> friendViews = new List<FriendViewModel>();
            List<Profile> friends;
            List<BasicFriendView> basicRelationInfo = new List<BasicFriendView>();
            List<string> friendIds = new List<string>();
            List<Relationship> relationships = friendRepository.GetAllAcceptedRelationshipsFromOneUser(userID);
            foreach (Relationship item in relationships)
            {
                if (item.UserOneID == userID)
                {
                   
                    basicRelationInfo.Add(new BasicFriendView(item.UserTwoID, item.IdentificationCode));
                }
                 
                else
                    basicRelationInfo.Add(new BasicFriendView(item.UserOneID, item.IdentificationCode));
            }

            return basicRelationInfo;
          

        }

        public List<PendingRequestViewModel> GetPendingFriends(string userID)
        {
            List<Relationship> relationships = friendRepository.GetAllPendingRelationships(userID);
            List<PendingRequestViewModel> pendingRequests = new List<PendingRequestViewModel>();
            for (int i = 0; i < relationships.Count; i++)
            {
                Relationship relationship = relationships[i];
                pendingRequests.Add(new PendingRequestViewModel(relationship.ActionUserID, relationship.ID));

            }
            return pendingRequests;
        }

        public Relationship GetFriend(string userID, string otherUserID)
        {
            return friendRepository.GetRelationship(userID, otherUserID);
        }
    }
}
