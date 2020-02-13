using Infrastructure.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApp.WebApi.Identity.Controllers
{
    [Produces("application/json")]
    [Route("api/userRoles")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        /// <summary>
        /// Get a user roles
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [Route("get/{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            return Ok(await _userManager.GetRolesAsync(user));
        }
    }
}
