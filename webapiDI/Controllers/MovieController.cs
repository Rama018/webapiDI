using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapiDI.Models;
using Microsoft.EntityFrameworkCore;

namespace webapiDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieContext _context;

        public MovieController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {

            if (_context.Move == null)
            {
                return NotFound();
            }
            return await _context.Move.ToListAsync();

        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetID([FromRoute] int id)
        {


            var ID = await _context.Move.FindAsync(id);
            if (ID == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(ID);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Movie>> Add(Movie movie)
        {

            await _context.Move.AddAsync(movie);
            await _context.SaveChangesAsync();

            return Ok(movie);



        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Movie movie,  int id)
        {
            if (id != movie.Id || movie==null)
            {
                return BadRequest();
            }
          _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!test(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ;
                }

            }
            return NoContent();
        }



        private bool test(int id)
        {
            return (_context.Move?.Any(e => e.Id == id)).GetValueOrDefault(); // ?? false;
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Move == null)
            {
                return NotFound();
            }

            var ById = await _context.Move.FindAsync(id);

            if(ById== null)
            {
                return NotFound();
            }

            _context.Move.Remove(ById);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
    }
}
