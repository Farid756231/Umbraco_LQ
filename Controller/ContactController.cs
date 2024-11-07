
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Umbraco.Cms.Core.Persistence;
using umbraco_lingoquest.Data;
using umbraco_lingoquest.Moduller;

namespace umbraco_lingoquest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
       private readonly DataContext _context;
        public ContactController(DataContext context)
        {
            _context = context;
        }

         
        [HttpGet]

        
        public async Task<ActionResult<IEnumerable<contactUs>>> GetContact()
        {

            return await _context.ContactUs.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> PostContact([FromBody] contactUs newContact)
        {

            if (newContact == null)
            {
                return BadRequest("Invalid quiz result data.");
            }

            _context.ContactUs.Add(newContact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostContact), new { id = newContact.Id }, newContact);
        }



    }
}
