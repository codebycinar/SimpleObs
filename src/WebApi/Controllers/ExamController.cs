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


        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
