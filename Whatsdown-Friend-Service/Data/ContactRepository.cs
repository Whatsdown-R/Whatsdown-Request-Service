using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Models;

namespace Whatsdown_Friend_Service
{
    public class ContactRepository
    {
        FriendContext friendContext;

        public ContactRepository(FriendContext auth)
        {

            this.friendContext = auth;
        }
        public List<Profile> GetContactsByName(string name)
        {
            return this.friendContext.Profiles.Where(c => c.displayName.Contains(name)).Take(100).ToList();
        }


    }
}
