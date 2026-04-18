using fco_database.Data;
using fco_database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fco_database.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonCardController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;
        
        public SeasonCardController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeasonCardModel>>> GetCards()
        {
            IEnumerable<SeasonCardModel> seasonCards = await _dbContext.season_cards.Where(x => x.IsActive == 1).ToListAsync();
            // if (!seasonCards.Any()) return NotFound();
            return Ok(seasonCards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonCardModel>> GetCard(string id)
        {
            SeasonCardModel? foundCard = await _dbContext.season_cards.FirstOrDefaultAsync(x => x.IsActive == 1&& x.Uid == id);
            if (foundCard == null) return NotFound();
            return Ok(foundCard);
        }

        // POST api/<SeasonCardController>
        [HttpPost]
        public async Task<ActionResult> AddCard([FromBody] SeasonCardModel iCard)
        {
            try
            {
                await _dbContext.AddAsync(iCard);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCard), new { id = iCard.Uid });
            }
            catch
            {
                return BadRequest("Something is error!!!");
            }
        }
        
        // DELETE api/<SeasonCardController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCard(string id)
        {
            SeasonCardModel? foundCard = await _dbContext.season_cards.FirstOrDefaultAsync(x => x.Uid == id);
            if (foundCard == null) return NotFound();

            foundCard.IsActive = 0;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("GetNumberOfCards")]
        public int GetNumberCards()
        {
            return _dbContext.season_cards.Where(x => x.IsActive == 1).Count();
        }
    }
}
