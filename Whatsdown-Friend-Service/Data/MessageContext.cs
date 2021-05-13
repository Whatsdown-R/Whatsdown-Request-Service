using MessageService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Friend_Service.Data
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options) : base(options)
        {

            if (!Database.IsInMemory())
                Database.EnsureCreated();
        }

        public DbSet<Message> Messages { get; set; }
    }
}
