using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Identity;
using SchoolWebApp.WebApi.Identity.Models;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SchoolWebApp.WebApi.Identity.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/manage")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
       

        public ManageController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            UrlEncoder urlEncoder
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        /// <summary>
        /// Get user information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserModel), 200)]
        [Route("userInfo")]
        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst("uid")?.Value);

            var userModel = new UserModel
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return Ok(userModel);
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }
    }
}
