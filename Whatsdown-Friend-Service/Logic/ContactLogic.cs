using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Models;
using Whatsdown_Friend_Service.Views;

namespace Whatsdown_Friend_Service.Logic
{
    public class ContactLogic
    {
        ContactRepository contactRepository;
        FriendRepository friendRepository;
        public ContactLogic(FriendContext _context)
        {
            this.contactRepository = new ContactRepository(_context);

            this.friendRepository = new FriendRepository(_context);
        }



        public List<PotentialContactView> GetProfilesByName(string name, string userId)
        {
            if (name.Length < 5)
                throw new ArgumentException();
            List<string> userIds = new List<string>();
            List<Profile> profiles = this.contactRepository.GetContactsByName(name);
            List<Relationship> friends = this.friendRepository.GetAllRelationshipsFromOneUser(userId);
            List<PotentialContactView> contacts = new List<PotentialContactView>();
            for (int i = 0; i < friends.Count; i++)
            {
                Relationship relationship = friends[i];
                string test;
                if (relationship.UserOneID.Equals(userId))
                {
                  test = relationship.UserTwoID;
                }
                else
                    test = relationship.UserOneID;

                userIds.Add(test);
            }
            

            for (int i = 0; i < profiles.Count; i++)
            {
                Profile profile = profiles[i];
                if (userIds.Contains(profile.UserID))
                    profiles.Remove(profile);
            }

            for (int i = 0; i < profiles.Count; i++)
            {
                Profile profile = profiles[i];

                PotentialContactView contact = new PotentialContactView(profile.displayName, profile.profileId, profile.profileImage);
                contacts.Add(contact);
            }

            return contacts;
            
        }
    }
}
