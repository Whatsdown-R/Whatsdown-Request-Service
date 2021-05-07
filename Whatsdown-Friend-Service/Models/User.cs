using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Whatsdown_Authentication_Service.Models
{
    public class User
    {

        public string UserID { get;  set; }

        public string Email { get;  set; }


        public string PasswordSalt { get;  set; }

        public string PasswordHash { get;  set; }

        public Profile Profile { get;  set; }

        public User() { }

        public User(string userID, string email, string passwordSalt, string passwordHash, Profile profile)
        {
            UserID = userID;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Profile = profile;
        }
    }
}
