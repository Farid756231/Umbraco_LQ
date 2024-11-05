using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using umbraco_lingoquest.Data;
using umbraco_lingoquest.Moduller;

namespace umbraco_lingoquest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Prov2QuizzResultController : ControllerBase
    {
        private readonly DataContext _context;
        public Prov2QuizzResultController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prov2QuizzResult>>> GetGrammaticResults()
        {
            return await _context.Prov2QuizzResults.ToListAsync();
        }


        [HttpPost]
        public async Task<IActionResult> PostGrammaticResult([FromBody] Prov2QuizzResult grammaticResult)
        {
            if (grammaticResult == null)
            {
                return BadRequest("Invalid grammatic result data.");
            }

            _context.Prov2QuizzResults.Add(grammaticResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostGrammaticResult), new { id = grammaticResult.Id }, grammaticResult);
        }
    }
}
