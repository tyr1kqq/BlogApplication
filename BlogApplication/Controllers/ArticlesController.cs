using BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ArticleViewModel>> CreateArticle([FromBody] ArticleViewModel article)
        {
            if (ModelState.IsValid)
            {
                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetArticleById), new { id = article.ArticleID }, article);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleViewModel>>> GetAllArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleViewModel>> GetArticleById(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
                return NotFound();

            return article;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] ArticleViewModel article)
        {
            if (id != article.ArticleID)
                return BadRequest();

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
                return NotFound();

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<IEnumerable<ArticleViewModel>>> GetArticlesByAuthor(int authorId)
        {
            return await _context.Articles.Where(a => a.UserID == authorId).ToListAsync();
        }

        private bool ArticleExists(int id) => _context.Articles.Any(e => e.ArticleID == id);
    }
}
