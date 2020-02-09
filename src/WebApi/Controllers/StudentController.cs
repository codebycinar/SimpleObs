using AutoMapper;
using Core.Data.DTO;
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
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return _mapper.Map<List<StudentDTO>>(students);
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return _mapper.Map<StudentDTO>(student);
        }

        // GET: api/Student/5
        [HttpGet("[action]/{id}")]
        //[Route("[action]/{id}")]
        public async Task<ActionResult<List<ExamDTO>>> GetStudentExamResults(int id)
        {
            var student = await _context.Students
                .Include(exams => exams.StudentExams)
                    .ThenInclude(e => e.Exam)
                        .ThenInclude(l=>l.Lesson)
                .Include(lessons => lessons.StudentLessons)
                    .ThenInclude(l => l.Lesson)
                .FirstOrDefaultAsync(x => x.Id == id);

            List<ExamDTO> results = null;

            if (student == null)
            {
                return NotFound();
            }

            results = new List<ExamDTO>();
            foreach (var item in student.StudentExams)
            {
                results.Add(
                    new ExamDTO
                    {
                        ExamName = item.Exam.Name,
                        ExamType = item.Exam.ExamType,
                        Lesson = new LessonDTO
                        {
                            LessonName = item.Exam.Lesson.Name
                        },
                        Result = item.Result
                    });
            }
            return _mapper.Map<List<ExamDTO>>(results);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(s => s.Id == id);
        }
    }
}
