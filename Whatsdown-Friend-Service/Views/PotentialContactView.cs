using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Views
{
    public class PotentialContactView
    {
        public string DisplayName { get; set; }
        public string UserID { get; set; }
        public string Image { get; set; }

        public PotentialContactView(string displayName, string userID, string image)
        {
            DisplayName = displayName;
            UserID = userID;
            Image = image;
        }

        public PotentialContactView()
        {
        }
    }
}
