using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using umbraco_lingoquest.Data;
using umbraco_lingoquest.Moduller;

namespace umbraco_lingoquest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {

        private readonly DataContext _context;

        public QuizController(DataContext context)
        {
            _context = context;
        }

        // GET: api/quiz
        [HttpGet]
        public ActionResult<List<QuizViewModel>> GetQuizzes()
        {
            var quizzes = _context.Quizzes.ToList();
            if (quizzes == null || !quizzes.Any())
            {
                return NotFound("No quizzes found.");
            }


            return Ok(quizzes);
        }
    }
}
