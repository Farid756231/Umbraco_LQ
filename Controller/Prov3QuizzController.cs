using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using umbraco_lingoquest.Data;
using umbraco_lingoquest.Moduller;

namespace umbraco_lingoquest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Prov3QuizzController : ControllerBase
    {
        private readonly DataContext _context;

        public Prov3QuizzController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Prov3Quizz>>> GetImages()
        {
            var images = await _context.prov3Quizzs.ToListAsync();
            return Ok(images);
        }
    }
}
