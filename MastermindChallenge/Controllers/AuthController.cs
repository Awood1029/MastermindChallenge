using AutoMapper;
using MastermindChallenge.API.Data;
using MastermindChallenge.API.ModelDtos.Player;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MastermindChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<Player> _userManager;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<Player> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(PlayerDto playerDto)
        {
            try
            {
                var player = _mapper.Map<Player>(playerDto);
                player.UserName = playerDto.UserName;
                var result = await _userManager.CreateAsync(player, playerDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    };
                    return BadRequest(ModelState);
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(PlayerLoginDto playerDto)
        {
            _logger.LogInformation($"Login attempt for {playerDto.UserName}");
            try
            {
                var user = await _userManager.FindByEmailAsync(playerDto.UserName);
                var passwordValid = await _userManager.CheckPasswordAsync(user, playerDto.Password);

                if (user == null || !passwordValid) return NotFound();

                return Accepted();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
}
