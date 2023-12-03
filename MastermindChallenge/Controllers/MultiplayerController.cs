using MastermindChallenge.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MastermindChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultiplayerController : ControllerBase
    {
        private readonly MastermindChallengeDbContext _dbContext;
        private readonly ILogger<MultiplayerController> _logger;

        public MultiplayerController(MastermindChallengeDbContext dbContext, ILogger<MultiplayerController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/sessions")]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessionsAsync()
        {
            var response = await _dbContext.Sessions.Where(session => session.PlayerCount < 2).ToListAsync();
            return Ok(response);
        }

        [HttpPost]
        [Route("api/create-session")]
        public async Task<ActionResult<Session>> CreateSessionAsync()
        {
            try
            {
                var session = new Session()
                {
                    SessionId = Guid.NewGuid().ToString(),
                    PlayerCount = 1
                };

                await _dbContext.Sessions.AddAsync(session);
                await _dbContext.SaveChangesAsync();
                return Ok(session);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateSessionAsync)} action: {ex.Message}");
                return Problem($"Something went wrong in the {nameof(CreateSessionAsync)}", statusCode: 500);
            }
        }
    }
}
