using AutoMapper;
using Core.Data.DTO;
using Core.Data.ViewModel;
using Infrastructure;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace SchoolWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(SchoolDbContext context, IMapper mapper, ILogger<StudentController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("Detail/{id}")]
        public IActionResult Detail(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user.UserName == "admin")
            {
                var data = GetStudent(id);
                return View(data);
            }
            else
                return RedirectToAction("Index", "User");
        }

        [Route("StudentList")]
        private IActionResult StudentList()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user.UserName == "admin")
            {
                var students = _context.Students.ToListAsync();
                var data = _mapper.Map<List<StudentDetailViewModel>>(students);
                return View(data);
            }
            else
                return RedirectToAction("Index", "User");
        }

        private StudentDetailViewModel GetStudent(int id)
        {
            var student = _context.Students
                .Include(gr => gr.Grade)
                    .ThenInclude(grl => grl.LessonResults)
                .Include(exams => exams.StudentExams)
                    .ThenInclude(e => e.Exam)
                        .ThenInclude(l => l.Lesson)
                .Include(lessons => lessons.StudentLessons)
                    .ThenInclude(l => l.Lesson)
                .FirstOrDefault(x => x.Id == id);

            var schoolResults = _context.GradeLessons
                .Include(gr => gr.Grade)
                            .ToList();

            if (student == null)
            {
                return null;
            }

            var examResults = new List<ExamResultsDTO>();
            var lessonResults = new List<LessonResultsDTO>();

            foreach (var item in student.StudentExams)
            {
                examResults.Add(
                    new ExamResultsDTO
                    {
                        Exam = new ExamDTO
                        {
                            ExamName = item.Exam.Name,
                            ExamType = item.Exam.ExamType,
                            Lesson = new LessonDTO
                            {
                                LessonId = item.Exam.LessonId,
                                LessonName = item.Exam.Lesson.Name
                            }
                        },
                        Result = item.Result
                    });
            }

            foreach (var item in student.StudentLessons)
            {
                lessonResults.Add(new LessonResultsDTO
                {
                    Lesson = new LessonDTO
                    {
                        LessonId = item.LessonId,
                        LessonName = item.Lesson.Name
                    },
                    Result = item.Result
                });
            }

            StudentDetailViewModel result = new StudentDetailViewModel();
            result.Student = _mapper.Map<StudentDTO>(student);
            result.Student.ExamsResults = examResults;
            result.Student.LessonResults = lessonResults;
            result.SchoolLessonResults = _mapper.Map<List<GradeLessonResultDTO>>(schoolResults);

            return result;
        }
    }
}