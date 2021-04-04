using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whatsdown_Friend_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        // GET: api/<FriendController>
        [HttpGet]
        public IEnumerable<string> GetFriends()
        {
            return new string[] { "value1", "value2" };
        }

    
        // POST api/<FriendController>
        [HttpPost]
        public void SendFriendRequest(string value)
        {
        }

        [HttpGet]
        public IEnumerable<string> GetPendingFriendRequests()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPut]
        public IEnumerable<string> DeclineFriendRequest()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPut]
        public IEnumerable<string> BlockFriendRequest()
        {
            return new string[] { "value1", "value2" };
        }

      

        // DELETE api/<FriendController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
