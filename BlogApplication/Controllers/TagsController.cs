using BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<TagViewModel>> CreateTag([FromBody] TagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTagById), new { id = tag.TagID }, tag);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagViewModel>>> GetAllTags()
        {
            return await _context.Tags.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagViewModel>> GetTagById(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
                return NotFound();

            return tag;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] TagViewModel tag)
        {
            if (id != tag.TagID)
                return BadRequest();

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
                return NotFound();

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id) => _context.Tags.Any(e => e.TagID == id);
    }
}
