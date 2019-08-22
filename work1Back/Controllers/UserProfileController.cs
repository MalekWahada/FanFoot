using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using work1Back.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ForAdmin")]
        public string GetForAdmin()
        {
            return "Web method for Admin";
        }

        [HttpGet]
        [Authorize(Roles = "Fan")]
        [Route("ForFan")]
        public string GetCustomer()
        {
            return "Web method for Customer";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Fan")]
        [Route("ForAdminOrCustomer")]
        public string GetForAdminOrCustomer()
        {
            return "Web method for Admin or Fan";
        }
    }
}

// for google login
/*[HttpGet]
public IActionResult ExternalLogin(string provider)
{
    var callbackUrl = Url.Action("ExternalLoginCallback");
    var authenticationProperties = new AuthenticationProperties { RedirectUri = callbackUrl };
    return this.Challenge(authenticationProperties, provider);
}

[HttpGet]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{
    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    return this.Ok(new
    {
        NameIdentifier = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier),
        Email = result.Principal.FindFirstValue(ClaimTypes.Email),
        Picture = result.Principal.FindFirstValue("image")
    });
}*/
