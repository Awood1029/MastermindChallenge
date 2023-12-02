﻿using AutoMapper;
using MastermindChallenge.API.Data;
using MastermindChallenge.API.ModelDtos.Game;
using MastermindChallenge.API.ModelDtos.Player;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Numerics;
using System.Text.Json;

namespace MastermindChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly MastermindChallengeDbContext _dbContext;
        private readonly ILogger<GameController> _logger;
        private readonly IMapper _mapper;

        public GameController(MastermindChallengeDbContext dbContext, ILogger<GameController> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("save-game")]
        public async Task<ActionResult<string>> SaveGameAsync(SaveGameDto gameDto)
        {
            try
            {
                var game = _mapper.Map<Game>(gameDto);
                await _dbContext.Games.AddAsync(game);
                await _dbContext.SaveChangesAsync();
                return Ok(200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(SaveGameAsync)} action: {ex.Message}");
                return Problem($"Something went wrong in the {nameof(SaveGameAsync)}", statusCode: 500);
            }
        }

        [HttpGet]
        [Route("leaderboard")]
        public async Task<ActionResult<IEnumerable<Top10ScoresDto>>> GetLeaderboard()
        {
            try
            {
                var scores = new List<Top10ScoresDto>();
                var difficulties = new List<int> { 4, 5, 6 };
                foreach (var difficulty in difficulties)
                {
                    var result = await _dbContext.Games
                                .Join(_dbContext.Players,
                                    game => game.PlayerId,
                                    player => player.Id,
                                    (game, player) => new Top10ScoresDto
                                    {
                                        AttemptsUsed = game.AttemptsUsed,
                                        Difficulty = game.Difficulty,
                                        IsWinner = game.IsWinner,
                                        Username = player.UserName
                                    })
                                .Where(game => game.IsWinner == true && game.Difficulty == difficulty)
                                .OrderBy(game => game.AttemptsUsed).Take(10).ToListAsync();
                    scores.AddRange(result);
                }
                
                return Ok(scores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetLeaderboard)} action: {ex.Message}");
                return Problem($"Something went wrong in the {nameof(GetLeaderboard)}", statusCode: 500);
            }
        }

        public static int[] GetRandomNumber(int answerLength)
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync("https://www.random.org/integers/?num=4&min=0&max=7&col=1&base=10&format=plain&rnd=new").Result.Split("\n");
            int[] answerToGuessArr = new int[answerLength];
            for (int i = 0; i < response.Length - 1; i++)
            {
                answerToGuessArr[i] = int.Parse(response[i]);
            }
            return answerToGuessArr;
        }
    }
}
