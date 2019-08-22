using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using work1Back.Models;

namespace work1Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfile22Controller : ControllerBase
    {
        private readonly AuthentifcationContext _context;

        public UserProfile22Controller(AuthentifcationContext context)
        {
            _context = context;
        }

        [HttpGet("{Id}")]
        public object GetUserProfile22([FromRoute] string id)
        {
            ApplicationUser user = _context.ApplicationUsers.First(u => u.Id == id);

            if (user != null)
            {

                return new { user.UserName, user.Email, user.FullName };
            }
            else return BadRequest("Not Found");
        }

    }
}