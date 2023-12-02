﻿using MastermindChallenge.Blazor.Server.Providers;

namespace MastermindChallenge.Blazor.Server.Services.Game
{
    public class GameService : IGameService
    {
        private readonly IClient _httpClient;
        private readonly ApiAuthenticationStateProvider _authStateProvider;

        public GameService(IClient httpClient, ApiAuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }
        public string CheckAnswer()
        {
            return "";
        }

        public async Task SaveGameAsync(SaveGameDto gameDto)
        {
            var user = (await _authStateProvider.GetAuthenticationStateAsync()).User;
            gameDto.PlayerId = user.FindFirst(u => u.Type.Contains("uid"))?.Value;
            var result = _httpClient.SaveGameAsync(gameDto);
        }

        public async Task<int[]> GetRandomNumber(int answerLength = 4)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://www.random.org/integers/?num=4&min=0&max=7&col=1&base=10&format=plain&rnd=new");
            // We get the answer as a single string, so we need to split it into an array of chars to then convert to an array of ints
            var answerStringArr = response.Split("\n");
            int[] answerToGuessArr = new int[answerLength];
            for (int i = 0; i < answerStringArr.Length - 1; i++)
            {
                answerToGuessArr[i] = int.Parse(answerStringArr[i]);
            }
            return answerToGuessArr;
        }

        public async Task<IEnumerable<Top10ScoresDto>> GetLeaderboard(int difficulty)
        {
            var response = await _httpClient.LeaderboardAsync(difficulty);

            return response.ToList();
        }
    }
}
