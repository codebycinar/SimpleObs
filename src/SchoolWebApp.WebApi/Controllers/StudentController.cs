using AutoMapper;
using Core.Data.DTO;
using Core.Data.ViewModel;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebApp.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
  
    public class StudentController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;

        public StudentController(SchoolDbContext context, ILogger<StudentController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Student
        [HttpGet]
     
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return _mapper.Map<List<StudentDTO>>(students);
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDetailViewModel>> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(gr => gr.Grade)
                    .ThenInclude(grl => grl.LessonResults)
                .Include(exams => exams.StudentExams)
                    .ThenInclude(e => e.Exam)
                        .ThenInclude(l => l.Lesson)
                .Include(lessons => lessons.StudentLessons)
                    .ThenInclude(l => l.Lesson)
                .FirstOrDefaultAsync(x => x.Id == id);

            var schoolResults = await _context.GradeLessons
                            .ToListAsync();
            
            var examResults = new List<ExamResultsDTO>();
            var lessonResults = new List<LessonResultsDTO>();

            if (student == null)
            {
                return NotFound();
            }

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

        private decimal GetGradeAvgByLesson(int lessonId, int gradeId)
        {
            var gradeResults = _context.GradeLessons.FirstOrDefault(x => x.GradeId.Equals(gradeId) && x.LessonId.Equals(lessonId));
            return gradeResults.Average;
        }
        private bool StudentExists(int id)
        {
            return _context.Students.Any(s => s.Id == id);
        }
    }
}
