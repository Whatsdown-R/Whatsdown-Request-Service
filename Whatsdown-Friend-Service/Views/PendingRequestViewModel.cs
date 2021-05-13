using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Views
{
    public class PendingRequestViewModel
    {
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }
        public string RelationId { get; set; }

        public PendingRequestViewModel(string displayName, string profileImage, string relationId)
        {
            DisplayName = displayName;
            ProfileImage = profileImage;
            RelationId = relationId;
        }

        public PendingRequestViewModel()
        {
        }
    }
}
