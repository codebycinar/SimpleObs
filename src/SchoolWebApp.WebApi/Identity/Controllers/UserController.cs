using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolWebApp.WebApi.Identity.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApplicationUser>), 200)]
        [Route("get")]
        public IActionResult Get() => Ok(_userManager.Users);

        /// <summary>
        /// Get a user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApplicationUser), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("get/{Id}")]
        public IActionResult Get(string Id)
        {
            if (String.IsNullOrEmpty(Id))
                return BadRequest(new string[] { "Empty parameter!" });

            return Ok(_userManager.Users.Where(user => user.Id == Id));
        }

    }
}
