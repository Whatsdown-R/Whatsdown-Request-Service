using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Views
{
    public class PendingRequestViewModel
    {
        public string ProfileId { get; set; }


        public PendingRequestViewModel(string profileId)
        {
            ProfileId = profileId;
            
        }

        public PendingRequestViewModel()
        {
        }
    }
}
