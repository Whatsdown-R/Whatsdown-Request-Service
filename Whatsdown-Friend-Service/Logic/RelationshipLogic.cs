using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Friend_Service.Data;

namespace Whatsdown_Friend_Service
{
    public class RelationshipLogic
    {
        FriendRepository friendRepository;
        public RelationshipLogic(FriendContext _context)
        {
            this.friendRepository = new FriendRepository(_context);
        }

        public void RequestFriend()
        {

        }

        public void AcceptFriend()
        {

        }

        public void DenyFriend()
        {

        }

        public void BlockFriend()
        {

        }

        public void GetFriends()
        {

        }

        public void GetPendingFriends()
        {

        }
    }
}
