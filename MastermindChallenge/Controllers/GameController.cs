﻿using AutoMapper;
using MastermindChallenge.API.Data;
using MastermindChallenge.API.ModelDtos.Game;
using MastermindChallenge.API.ModelDtos.Player;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        [Route("create-game")]
        public async Task<ActionResult<GameCreateDto>> CreateGame(GameCreateDto gameDto)
        {
            try
            {
                var game = _mapper.Map<Game>(gameDto);
                game.AnswerToGuess = GetRandomNumber(4);
                await _dbContext.Games.AddAsync(game);
                await _dbContext.SaveChangesAsync();
                var test = CreatedAtAction(nameof(CreateGame), new { id = game.Id, answerToGuess = game.AnswerToGuess }, game);
                return CreatedAtAction(nameof(CreateGame), new { id = game.Id, answerToGuess = game.AnswerToGuess }, game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateGame)} action: {ex.Message}");
                return Problem($"Something went wrong in the {nameof(CreateGame)}", statusCode: 500);
            }
        }

        public static int[] GetRandomNumber(int answerLength) 
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync("https://www.random.org/integers/?num=4&min=0&max=7&col=1&base=10&format=plain&rnd=new").Result.Split("\n");
            int[] answerToGuessArr = new int[answerLength];
            for (int i = 0; i <  response.Length - 1; i++)
            {
                answerToGuessArr[i] = int.Parse(response[i]);
            }
            return answerToGuessArr;
        }
    }
}
