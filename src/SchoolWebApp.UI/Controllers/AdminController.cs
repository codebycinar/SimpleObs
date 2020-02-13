using Core.Data.DTO;
using Core.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolWebApp.UI.Helpers;
using SchoolWebApp.UI.Models;
using SchoolWebApp.UI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApp.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentClient _studentClient;

        public AdminController(IStudentClient studentClient)
        {
            _studentClient = studentClient;
        }

        public async Task<IActionResult> Index()
        {
            var auth = HttpContext.Session.GetObjectFromJson<JwtModel>("token");
            if (auth == null)
                return RedirectToAction("Index", "User");

            if (auth.IsAdmin)
            {
                var studentResult = await _studentClient.GetStudents(auth);
                if (studentResult.IsSuccessStatusCode)
                {
                    string stringResult = await studentResult.Content.ReadAsStringAsync();
                    var student = JsonConvert.DeserializeObject<List<StudentDTO>>(stringResult);
                    if (student != null)
                    {
                        return View("Index", student);
                    }

                }
                return RedirectToAction("Index", "User");
            }
            else
                return RedirectToAction("Index", "User");
        }

        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var auth = HttpContext.Session.GetObjectFromJson<JwtModel>("token");
            if (auth == null)
                return RedirectToAction("Index", "User");

            var studentResult = await _studentClient.GetStudentDetail(auth,id);
            if (studentResult.IsSuccessStatusCode)
            {
                string stringResult = await studentResult.Content.ReadAsStringAsync();
                var student = JsonConvert.DeserializeObject<StudentDetailViewModel>(stringResult);
                if (student != null)
                {
                    return View("Detail", student);
                }

            }
            return RedirectToAction("Index", "User");
        }
    }
}