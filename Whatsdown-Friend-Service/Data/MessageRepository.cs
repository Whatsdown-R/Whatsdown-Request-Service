using MessageService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Friend_Service.Views;

namespace Whatsdown_Friend_Service.Data
{
    public class MessageRepository
    {
        MessageContext messageContext;

        public MessageRepository(MessageContext auth)
        {

            this.messageContext = auth;
        }


        public BasicMessageView GetMostRecentMessage(string identification)
        {
            Message message = messageContext.Messages.Where(c => c.identificationCode == identification).OrderByDescending(c => c.date).FirstOrDefault();
            BasicMessageView view = new BasicMessageView();
            if (message != null) { 
                if (message.message.Length > 15)
                {
                    message.message = message.message.Substring(0, 14);
                    message.message = message.message + "...";
                }
                view = new BasicMessageView(message.senderId, message.message, message.date);
            }
          

            
            return view;
        }
    }
}
