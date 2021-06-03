using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Logic;
using Whatsdown_Friend_Service.Models;
using Whatsdown_Friend_Service.Views;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whatsdown_Friend_Service.Controllers
{
    [Route("api/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        ContactLogic friendlogic;
        public ContactController(FriendContext _context)
        {
            this.friendlogic = new ContactLogic(_context);
        }


        //Change view to safer
        [HttpGet]
        public IActionResult GetFriend(string name, string profileId)
        {
            try
            {
                List<PotentialContactView> profiles = friendlogic.GetProfilesByName(name, profileId);
                return Ok(new { profiles = profiles });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized();
            }
           
        }

    }
}
