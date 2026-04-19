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
        public ActionResult<IEnumerable<SeasonCardModel>> GetCards()
        {
            IEnumerable<SeasonCardModel> seasonCards =  _dbContext.season_cards.Where(x => x.IsActive == 1);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCard(string id,[FromBody] SeasonCardDTO idata)
        {
            SeasonCardModel? foundCard = await _dbContext.season_cards.FirstOrDefaultAsync(x => x.Uid == id);
            if (foundCard == null) return NotFound();
            if (idata.IsActive != 0 || idata.IsActive != 1) return NoContent(); // 0 or 1

            if (idata.Salary != null) foundCard.Salary = idata.Salary.Value;
            if (idata.IsActive != null) foundCard.IsActive = idata.IsActive.Value;
 
            await _dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpGet("total")]
        public int TotalNumberCards()
        {
            return _dbContext.season_cards.Where(x => x.IsActive == 1).Count();
        }

        [HttpGet("inactive")]
        public ActionResult<IEnumerable<SeasonCardModel>> InactiveCards()
        {
            IEnumerable<SeasonCardModel> inactiveCards = _dbContext.season_cards.Where(x => x.IsActive == 0);
            return Ok(inactiveCards);   
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SeasonCardModel>>> SearchCards(string? foot,int? minOvr,string? season)
        {
            var fquery = _dbContext.season_cards.AsQueryable();

            if (foot != null) fquery = fquery.Where(c => c.Foot == foot);
            if (minOvr != null) fquery = fquery.Where(c => c.Ovr >= minOvr);
            if (season != null) fquery = fquery.Where(c => c.Season == season);

            var result = await fquery.ToListAsync();
            return Ok(result);
        }
    }
}
