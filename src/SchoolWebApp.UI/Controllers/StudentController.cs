using Core.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolWebApp.UI.Helpers;
using SchoolWebApp.UI.Models;
using SchoolWebApp.UI.Services;
using System.Threading.Tasks;

namespace SchoolWebApp.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentClient _studentClient;

        public StudentController(IStudentClient studentClient)
        {
            _studentClient = studentClient;
        }

        public async Task<IActionResult> Index()
        {
            var auth = HttpContext.Session.GetObjectFromJson<JwtModel>("token");
            if (auth == null)
                return RedirectToAction("Index", "User");

            var studentResult = await _studentClient.GetStudentDetail(auth,auth.User.StudentId);
            if (studentResult.IsSuccessStatusCode)
            {
                string stringResult = await studentResult.Content.ReadAsStringAsync();
                var student = JsonConvert.DeserializeObject<StudentDetailViewModel>(stringResult);
                if (student != null)
                {
                    return View("Detail",student);
                }

            }
            return RedirectToAction("Index", "User");
        }

    }
}