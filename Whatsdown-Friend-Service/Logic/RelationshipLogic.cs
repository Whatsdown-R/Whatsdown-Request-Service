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
        MessageRepository messageRepository;

        public RelationshipLogic(FriendContext _context, MessageContext messageContext )
        {
            this.friendRepository = new FriendRepository(_context);
            this.messageRepository = new MessageRepository(messageContext);
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

        public void AcceptFriend(string userID, string relationshipId)
        {
            Relationship relation = friendRepository.GetRelationshipById(relationshipId);

            if (relation == null)
                throw new RequestDoesNotExistException();

            if (relation.ActionUserID == userID)
                return;

            if (relation.UserOneID != userID && relation.UserTwoID != userID)
                return;
           
            if (relation.Status != "PENDING")
                return;

            
            Relationship newRelation = new Relationship(relation.ID, relation.UserOneID, relation.UserTwoID, relation.ActionUserID, "ACCEPTED", Guid.NewGuid().ToString());
            

            friendRepository.Update(newRelation);
        }

        public void DenyFriend(string profileId, string relationshipId)
        {
            Relationship relation = friendRepository.GetRelationshipById(relationshipId);

            if (relation == null)
                throw new RequestDoesNotExistException();

            if (relation.ID != relationshipId)
                return;
           
            if (relation.ActionUserID == profileId)
                return;

            if (relation.UserOneID != profileId && relation.UserTwoID != profileId)
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

        public List<FriendViewModel> GetFriends(string userID)
        {
            List<FriendViewModel> friendViews = new List<FriendViewModel>();
            List<Profile> friends;
            List<string> friendIds = new List<string>();
            List<Relationship> relationships = friendRepository.GetAllAcceptedRelationshipsFromOneUser(userID);
            foreach (Relationship item in relationships)
            {
                if (item.UserOneID == userID)
                    friendIds.Add(item.UserTwoID);
                else
                    friendIds.Add(item.UserOneID);
            }

            friends = friendRepository.GetListOfProfilesFromListOfIds(friendIds);

            foreach (Profile item in friends)
            {
                foreach (Relationship relationship in relationships)
                {
                    if (relationship.UserOneID == item.profileId || relationship.UserTwoID == item.profileId )
                    {
                        BasicMessageView view = messageRepository.GetMostRecentMessage(relationship.IdentificationCode);
                        if (view.displayName != null)
                        {
                            string senderName = "";
                            if (view.displayName == userID)
                            {
                                Profile profileFromMessage = friendRepository.GetProfileFromProfileId(view.displayName);
                                senderName = profileFromMessage.displayName;
                            }
                            else
                            {
                                senderName = item.displayName;
                            }
                          
                            friendViews.Add(new FriendViewModel(item.displayName, relationship.IdentificationCode, item.profileImage, view.message, view.date, senderName));
                        }
                        else
                        {
                            friendViews.Add(new FriendViewModel(item.displayName, relationship.IdentificationCode, item.profileImage, "", ""));
                        }
                       
                        
                        break;
                    }
                }
            }

            return friendViews;
        }

        public List<PendingRequestViewModel> GetPendingFriends(string userID)
        {
            List<Relationship> relationships = friendRepository.GetAllPendingRelationships(userID);
            List<PendingRequestViewModel> pendingRequests = new List<PendingRequestViewModel>();
            for (int i = 0; i < relationships.Count; i++)
            {
                Relationship relationship = relationships[i];
                Profile user = friendRepository.GetProfileFromProfileId(relationship.ActionUserID);
                pendingRequests.Add(new PendingRequestViewModel(user.displayName, user.profileImage, relationship.ID));

            }
            return pendingRequests;
        }

        public Relationship GetFriend(string userID, string otherUserID)
        {
            return friendRepository.GetRelationship(userID, otherUserID);
        }
    }
}
