using AutoMapper;
using MastermindChallenge.API.Data;
using MastermindChallenge.API.ModelDtos.Player;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MastermindChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly MastermindChallengeDbContext _dbContext;
        private readonly ILogger<PlayerController> _logger;
        private readonly IMapper _mapper;

        public PlayerController(MastermindChallengeDbContext dbContext, ILogger<PlayerController> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/player-scores
        [HttpGet]
        [Route("player-scores")]
        public async Task<ActionResult<IEnumerable<PlayerScoresDto>>> GetPlayerScores()
        {
            try
            {
                var players = _mapper.Map<IEnumerable<PlayerScoresDto>>(await _dbContext.Players.ToListAsync());
                return Ok(players);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(GetPlayerScores)} action: {ex.Message}");
                return StatusCode(500, "Couldn't perform action");
            }

        }
    }
}
