using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whatsdown_Friend_Service.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        RelationshipLogic friendlogic;
        public FriendController(FriendContext _context)
        {
            this.friendlogic = new RelationshipLogic(_context);
        }


        //Change view to safer
        [HttpGet, Route("single")]
        public IActionResult GetFriend(string userId, string friendId)
        {
           Relationship relationship = friendlogic.GetFriend(userId, friendId);
            return Ok(new { friend = relationship });
        }

        [HttpPost]
        public IActionResult SendFriendRequest(string userId, string friendId)
        {
            try
            {
                friendlogic.RequestFriend(userId, friendId);
                return Ok();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        [HttpPut, Route("accept")]
        public IActionResult AcceptFriendRequest(string userId, string friendId)
        {
            try
            {
                friendlogic.AcceptFriend(userId, friendId);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetPendingFriendRequests(string userId)
        {
            List<Relationship> relationships = friendlogic.GetPendingFriends(userId);
            return Ok(new { relationships = relationships });
        }

        [HttpPut, Route("decline")]
        public IActionResult DeclineFriendRequest(string userID, string actionId)
        {
            try
            {
                friendlogic.DenyFriend(userID, actionId);
                return Ok();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
         
           
        }

        [HttpPut, Route("block")]
        public IActionResult BlockFriendRequest(string userID, string actionId)
        {
            try
            {
                friendlogic.BlockFriend(userID, actionId);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

      

        // DELETE api/<FriendController>/5
      
    }
}
