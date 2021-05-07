using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Whatsdown_Authentication_Service.Models
{
    public class Profile
    {
      
        public string profileId { get;  set; }
        public string displayName { get;  set; }
        public string status { get;  set; }
        public string? profileImage { get;  set; }
        public string? gender { get;  set; }
        public string UserID { get;  set; }
        
        public User user { get;  set; }

        public Profile()
        {

        }

        public Profile(string profileId, string displayName, string status, string profileImage, string gender, string userID, User user)
        {
            this.profileId = profileId;
            this.displayName = displayName;
            this.status = status;
            this.profileImage = profileImage;
            this.gender = gender;
            UserID = userID;
            this.user = user;
        }

    }
}
