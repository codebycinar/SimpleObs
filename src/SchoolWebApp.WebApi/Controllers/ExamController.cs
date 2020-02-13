using AutoMapper;
using Core.Data.DTO;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExamController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;

        public ExamController(SchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: api/Exam
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDTO>>> GetExams()
        {
            var exams = await _context.Exams.ToListAsync();
            return _mapper.Map<List<ExamDTO>>(exams);
        }

        // GET: api/Exam/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamDTO>> GetExam(int id)
        {
            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
            {
                return NotFound();
            }

            return _mapper.Map<ExamDTO>(exam);
        }

        [HttpGet("[action]/{lessonId}")]
        public async Task<ActionResult<List<ExamDTO>>> GetExamsByLesson(int lessonId)
        {
            var exams = await _context.Exams.Where(x => x.LessonId == lessonId)
                .Include(l => l.Lesson)
                .Include(s => s.StudentExams)
                .ToListAsync();

            if (exams == null)
            {
                return NotFound();
            }
          
            return _mapper.Map<List<ExamDTO>>(exams);
        }

        // GET: api/Student/5
        [HttpGet("[action]/{gradeId}")]
        public async Task<ActionResult<StudentDTO>> GetClassAvg(int studentId)
        {
            var student = await _context.Students
                .Include(exams => exams.StudentExams)
                    .ThenInclude(e => e.Exam)
                        .ThenInclude(l => l.Lesson)
                .Include(lessons => lessons.StudentLessons)
                    .ThenInclude(l => l.Lesson)
                .Include(cl => cl.Grade)
                .FirstOrDefaultAsync(x => x.Id == studentId);

            List<ExamResultsDTO> examResults = null;

            if (student == null)
            {
                return NotFound();
            }

            examResults = new List<ExamResultsDTO>();
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
                                LessonName = item.Exam.Lesson.Name
                            }
                        },
                        Result = item.Result
                    });
            }

            var result = _mapper.Map<StudentDTO>(student);
            result.ExamsResults = examResults;
            return result;
        }
        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
