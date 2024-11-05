using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using umbraco_lingoquest.Data;
using umbraco_lingoquest.Moduller;

namespace umbraco_lingoquest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Prov1ResultController : ControllerBase
    {

        private readonly DataContext _context;

        public Prov1ResultController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Prov1Result
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prov1Result>>> GetProv1Results()
        {
            // Hämtar alla resultat från databasen
            return await _context.Prov1results.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Prov1Result>> PostProv1Result(Prov1Result result)
        {
            if (result == null)
            {
                return BadRequest("Result is null");
            }

            // Lägg till resultatet i databasen
            _context.Prov1results.Add(result);
            await _context.SaveChangesAsync();

            // Returnera skapad resurs med dess ID
            return CreatedAtAction(nameof(GetProv1Results), new { id = result.Id }, result);
        }

    }
}
