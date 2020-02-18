using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Newtonsoft.Json;
using SchoolWebApp.UI.Helpers;
using SchoolWebApp.UI.Models;
using SchoolWebApp.UI.Services;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SchoolWebApp.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserClient _userClient;

        public UserController(IUserClient userClient)
        {
            _userClient = userClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                JwtModel jwt = null;
                var loginResult = await _userClient.Login(login);
                if (loginResult.IsSuccessStatusCode)
                {
                    string stringJWT = await loginResult.Content.ReadAsStringAsync();
                    jwt = JsonConvert.DeserializeObject<JwtModel>(stringJWT);
                    SetUser(jwt);
                    HttpContext.Session.SetObjectAsJson("token", jwt);
                    if (jwt.IsAdmin)
                        return RedirectToAction("Index", "Admin");
                    return RedirectToAction("Index", "Student");
                }
                else
                    ModelState.AddModelError("", "Username or Password incorrets");
            }
            return View();
        }

        private void SetUser(JwtModel jwt)
        {
            var user = new GenericPrincipal(new ClaimsIdentity(jwt.User.UserName), new string[] { "student" });
            HttpContext.User = user;

        }
    }
}