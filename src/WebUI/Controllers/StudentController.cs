using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebUI.Factory;
using WebUI.Models;
using WebUI.Utilities;

namespace WebUI.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IOptions<SettingsModel> _appSettings;

        public StudentController(ILogger<StudentController> logger, IOptions<SettingsModel> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetStudents()
        {
            var data = await ApiClientFactory.Instance.GetStudents();
            return View(data);
        }
        public async Task<IActionResult> GetStudent(int id)
        {
            var data = await ApiClientFactory.Instance.GetStudent(id);
            return View("Detail", data);
        }
        public async Task<IActionResult> GetResults(int id)
        {
            var data = await ApiClientFactory.Instance.GetStudentResults(id);
            return View(data);
        }
    }
}