using MessageService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Friend_Service.Models;

namespace Whatsdown_Friend_Service.Data
{
    public class FriendContext : DbContext
    {

        public FriendContext(DbContextOptions<FriendContext> options) : base(options)
        {

            if (!Database.IsInMemory())
                Database.EnsureCreated();
        }

        public DbSet<Relationship> Relationships { get; set; }
    }
}
