// The purpose of this class is to simply return error from various different responses from our application
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        // Get: https://localhost:5001/api/buggy/auth
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret() 
        {
            return "Secret Text";
        }

        // Get: https://localhost:5001/api/buggy/not-found
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound() 
        {
            var thing = _context.Users.Find(-1);

            if (thing == null) return NotFound();

            return Ok(thing);
        }

        // Get: https://localhost:5001/api/buggy/server-error
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError() 
        {
            // If we dont find the entity that we have searched for then the Find() method will return null
            var thing = _context.Users.Find(-1);

            // Here when we try to convert the null that we have received from Find() method it will give NullReferenceException
            var thingToReturn = thing.ToString();

            return thingToReturn;
        }

        // Get: https://localhost:5001/api/buggy/bad-request
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest() 
        {
            return BadRequest("This was not a good request");
        }

    }
}