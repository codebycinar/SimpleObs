using AutoMapper;
using Core.Data.DTO;
using Core.Data.ViewModel;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;

        public StudentController(SchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDetailViewModel>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return _mapper.Map<List<StudentDetailViewModel>>(students);
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

            if (student == null)
            {
                return NotFound();
            }

            //examResults = new List<ExamResultsDTO>();
            //foreach (var item in student.StudentExams)
            //{
            //    examResults.Add(
            //        new ExamResultsDTO
            //        {
            //            Exam = new ExamDTO
            //            {
            //                ExamName = item.Exam.Name,
            //                ExamType = item.Exam.ExamType,
            //                Lesson = new LessonDTO
            //                {
            //                    LessonName = item.Exam.Lesson.Name
            //                }
            //            },
            //            Result = item.Result
            //        });
            //}
            StudentDetailViewModel result = new StudentDetailViewModel();
            result.Student = _mapper.Map<StudentDTO>(student);
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
