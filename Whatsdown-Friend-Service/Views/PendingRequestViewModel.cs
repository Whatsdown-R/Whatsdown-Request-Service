using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Views
{
    public class PendingRequestViewModel
    {
        public string ProfileId { get; set; }
        public string RelationId { get; set; }


        public PendingRequestViewModel(string profileId, string relationId)
        {
            ProfileId = profileId;
            this.RelationId = relationId;
        }

        public PendingRequestViewModel()
        {
        }
    }
}
