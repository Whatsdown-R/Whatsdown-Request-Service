using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Views
{
    public class BasicMessageView
    {
        public string displayName { get; set; }
        public string message { get; set; }

        public DateTime date {get;set;}

        public BasicMessageView(string displayName, string message, DateTime date)
        {
            this.displayName = displayName;
            this.message = message;
            this.date = date;
        }

        public BasicMessageView()
        {
        }
    }
}
