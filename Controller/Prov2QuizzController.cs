using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using umbraco_lingoquest.Data;
using umbraco_lingoquest.Moduller;

namespace umbraco_lingoquest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Prov2QuizzController : ControllerBase
    {
        private readonly DataContext _context;

        public Prov2QuizzController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prov2Quizz>>> GetGrammarQuizzes()
        {

            return await _context.Prov2Quizzs.ToListAsync();
        }
    }
}
