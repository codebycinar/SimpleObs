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
    public class ClassController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;


        public ClassController(SchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Class
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDTO>>> GetClassrooms()
        {
            var entities = await _context.Classrooms.ToListAsync();
            return _mapper.Map<List<GradeDTO>>(entities);
        }

        // GET: api/Class/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeDTO>> GetClassroom(int id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);

            if (classroom == null)
            {
                return NotFound();
            }

            return _mapper.Map<GradeDTO>(classroom);
        }
        private bool ClassroomExists(int id)
        {
            return _context.Classrooms.Any(e => e.Id == id);
        }
        //// PUT: api/Class/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutClassroom(int id, Classroom classroom)
        //{
        //    if (id != classroom.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(classroom).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClassroomExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Class
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Classroom>> PostClassroom(Classroom classroom)
        //{
        //    _context.Classrooms.Add(classroom);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetClassroom", new { id = classroom.Id }, classroom);
        //}

        //// DELETE: api/Class/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Classroom>> DeleteClassroom(int id)
        //{
        //    var classroom = await _context.Classrooms.FindAsync(id);
        //    if (classroom == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Classrooms.Remove(classroom);
        //    await _context.SaveChangesAsync();

        //    return classroom;
        //}
    }
}
