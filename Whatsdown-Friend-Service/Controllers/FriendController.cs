﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Logic;
using Whatsdown_Friend_Service.Models;
using Whatsdown_Friend_Service.Views;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whatsdown_Friend_Service.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        RelationshipLogic friendlogic;
        public FriendController(FriendContext _context, MessageContext _context2)
        {
            this.friendlogic = new RelationshipLogic(_context, _context2);
        }


        //Change view to safer
        [HttpGet, Route("single")]
        public IActionResult GetFriend(string userId, string friendId)
        {
           Relationship relationship = friendlogic.GetFriend(userId, friendId);
            return Ok(new { friend = relationship });
        }

        [HttpPost]
        public IActionResult SendFriendRequest(FriendRequestViewModel model)
        {
            try
            {
                friendlogic.RequestFriend(model.userId, model.friendId);
                return Ok();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        [HttpPut, Route("accept")]
        public IActionResult AcceptFriendRequest(RequestAnswerView request)
        {
            try
            {
                friendlogic.AcceptFriend(request.ProfileId, request.RelationshipId);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        [HttpGet(), Route("pending/{profileId}")]
        public IActionResult GetPendingFriendRequests(string profileId)
        {
            List<PendingRequestViewModel> relationships = friendlogic.GetPendingFriends(profileId);
            return Ok(new { PendingRequests = relationships });
        }

        [HttpGet(), Route("{profileId}")]
        public IActionResult GetFriends(string profileId)
        {
            List<FriendViewModel> friends = friendlogic.GetFriends(profileId);
            return Ok(new { friends = friends });
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
