using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Views
{
    public class RequestAnswerView
    {
        public string ProfileId { get; set; }
        public string RelationshipId { get; set; }

        public RequestAnswerView(string profileId, string relationshipId)
        {
            ProfileId = profileId;
            RelationshipId = relationshipId;
        }

        public RequestAnswerView()
        {
        }
    }
}
