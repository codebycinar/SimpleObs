using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Helpers;
using WebApi.Identity.ViewModels;
using WebApi.Settings;
using Microsoft.Extensions.Options;
using WebApi.Identity.Models;

namespace WebApi.Identity.Controllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenSettings _jwt;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IOptions<JwtSecurityTokenSettings> jwt
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._configuration = configuration;
            this._jwt = jwt.Value;
        }

        /// <summary>
        /// Confirms a user email address
        /// </summary>
        /// <param name="model">ConfirmEmailViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmailViewModel model)
        {

            if (model.UserId == null || model.Code == null)
            {
                return BadRequest(new string[] { "Error retrieving information!" });
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return BadRequest(new string[] { "Could not find user!" });

            var result = await _userManager.ConfirmEmailAsync(user, model.Code);
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors.Select(x => x.Description));
        }

      

        /// <summary>
        /// Log into account
        /// </summary>
        /// <param name="model">LoginViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TokenModel), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody]LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return BadRequest(new string[] { "Invalid credentials." });

            var tokenModel = new TokenModel()
            {
                HasVerifiedEmail = false
            };

            // Only allow login if email is confirmed
            if (!user.EmailConfirmed)
            {
                return Ok(tokenModel);
            }

            // Used as user lock
            if (user.LockoutEnabled)
                return BadRequest(new string[] { "This account has been locked." });

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                tokenModel.HasVerifiedEmail = true;

                if (user.TwoFactorEnabled)
                {
                    tokenModel.TFAEnabled = true;
                    return Ok(tokenModel);
                }
                else
                {
                    JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                    tokenModel.TFAEnabled = false;
                    tokenModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    tokenModel.Expiration = jwtSecurityToken.ValidTo;

                    return Ok(tokenModel);
                }
            }

            return BadRequest(new string[] { "Invalid login attempt." });
        }

        /// <summary>
        /// Log in with TFA 
        /// </summary>
        /// <param name="model">LoginWith2faViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TokenModel), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("tfa")]
        public async Task<IActionResult> LoginWith2fa([FromBody]LoginWith2faViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest(new string[] { "Invalid credentials." });

            if (await _userManager.VerifyTwoFactorTokenAsync(user, "Authenticator", model.TwoFactorCode))
            {
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);

                var tokenModel = new TokenModel()
                {
                    HasVerifiedEmail = true,
                    TFAEnabled = false,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Expiration = jwtSecurityToken.ValidTo
                };

                return Ok(tokenModel);
            }
            return BadRequest(new string[] { "Unable to verify Authenticator Code!" });
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}