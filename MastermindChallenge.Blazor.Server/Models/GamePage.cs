namespace MastermindChallenge.Blazor.Server.Models
{
    public class GamePage
    {
        public string PlayerGuess { get; set; }
        public int Difficulty { get; set; } = 4;
    }
}
