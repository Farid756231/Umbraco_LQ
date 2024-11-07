using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using umbraco_lingoquest.Data;
using umbraco_lingoquest.Moduller;

namespace umbraco_lingoquest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Prov3QuizzResultController : ControllerBase
    {
        private readonly DataContext _repository;

        public Prov3QuizzResultController(DataContext repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prov3QuizzResult>>> GetImagesResult()
        {
            return await _repository.prov3QuizzResults.ToListAsync();
        }



        [HttpPost]
        public async Task<ActionResult> PostImagesResult([FromBody] Prov3QuizzResult newResult)
        {

            if (newResult == null)
            {
                return BadRequest("Invalid quiz result data.");
            }

            _repository.prov3QuizzResults.Add(newResult);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(PostImagesResult), new { id = newResult.Id }, newResult);
        }
    }
}
