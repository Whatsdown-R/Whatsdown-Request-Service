using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Views
{
    public class FriendViewModel
    {
        public string DisplayName { get; set; }
        public string IdentificationCode { get; private set; }
        public string ProfileImage { get; set; }
        public string LastMessage { get; set; }
        public string LastMessageName { get; set; }
        public DateTime LastMessageDate { get; set; }

        public FriendViewModel(string displayName, string identificationCode, string profileImage, string lastMessage, DateTime lastMessageDate, string lastMessageName)
        {
            DisplayName = displayName;
            IdentificationCode = identificationCode;
            ProfileImage = profileImage;
            LastMessage = lastMessage;
            LastMessageDate = lastMessageDate;
            LastMessageName = lastMessageName;
        }

        public FriendViewModel(string displayName, string identificationCode, string profileImage, string lastMessage, string lastMessageName)
        {
            DisplayName = displayName;
            IdentificationCode = identificationCode;
            ProfileImage = profileImage;
            LastMessage = lastMessage;
            LastMessageName = lastMessageName;
        }

        public FriendViewModel(string displayName, string identificationCode, string profileImage)
        {
            DisplayName = displayName;
            IdentificationCode = identificationCode;
            ProfileImage = profileImage;
        }

        public FriendViewModel()
        {
        }
    }
}
