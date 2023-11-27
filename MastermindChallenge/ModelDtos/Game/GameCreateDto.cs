namespace MastermindChallenge.API.ModelDtos.Game
{
    public class GameCreateDto
    {
        public int Difficulty { get; set; }
        public int[]? AnswerToGuess { get; set; }
    }
}
