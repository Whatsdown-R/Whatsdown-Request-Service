using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Exceptions;
using Whatsdown_Friend_Service.Models;
using Whatsdown_Friend_Service.Views;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whatsdown_Friend_Service.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly ILogger<FriendController> logger;
        RelationshipLogic friendlogic;
        public FriendController(FriendContext _context, ILogger<FriendController> logger)
        {
            this.logger = logger;
            this.friendlogic = new RelationshipLogic(_context);
        }


        //Change view to safer
        [HttpGet, Route("single")]
        public IActionResult GetFriend(string userId, string friendId)
        {
           Relationship relationship = friendlogic.GetFriend(userId, friendId);
            return Ok(new { friend = relationship });
        }

        [HttpPost, Authorize]
        public IActionResult SendFriendRequest(FriendRequestViewModel model)
        {
            try
            {
                string id = User.FindFirstValue("id");
                friendlogic.RequestFriend(id, model.friendId);
                return Ok();
            }catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
         
        }

        [HttpPut, Route("accept"), Authorize()]
        public IActionResult AcceptFriendRequest(RequestAnswerView request)
        {
            try
            {
                string id = User.FindFirstValue("id");
                this.logger.LogDebug("Accepting following FriendRequest: " + request.RelationshipId);
                friendlogic.AcceptFriend(id, request.RelationshipId);
                return Ok();
            }
            catch (Exception ex)
            {
            if (ex is RequestDoesNotExistException || ex is RequestException)
            {
            return BadRequest(ex.Message);
            }
             return Unauthorized("Something went wrong");
            }
         
        }

        [HttpGet(), Authorize, Route("pending")]
        public IActionResult GetPendingFriendRequests()
        {
            string id = User.FindFirstValue("id");
            logger.LogInformation("Getting pending friend requests form profileId: " + id);
            List<PendingRequestViewModel> relationships = friendlogic.GetPendingFriends(id);
            logger.LogInformation("Succesfully got pending requests from profileId: " + id);
            return Ok(new { PendingRequests = relationships });
        }

        [HttpGet(), Authorize()]
        public IActionResult GetFriends()
        {
            try
            {
                string id = User.FindFirstValue("id");
                List<BasicFriendView> friends = friendlogic.GetFriends(id);
                return Ok(new { friend = friends });
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized();
            }
          
        }

        [HttpPut, Route("decline")]
        public IActionResult DeclineFriendRequest(RequestAnswerView request)
        {
            try
            {
                friendlogic.DenyFriend(request.ProfileId, request.RelationshipId);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
         
           
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
                return BadRequest(ex.Message);
            }
            
        }

      

        // DELETE api/<FriendController>/5
      
    }
}
