using Core.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolWebApp.UI.Helpers;
using SchoolWebApp.UI.Models;
using SchoolWebApp.UI.Services;
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
    }
}