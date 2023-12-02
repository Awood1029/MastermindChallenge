namespace MastermindChallenge.Blazor.Server.Models
{
    public class GamePage
    {
        public string PlayerGuessString { get; set; }
        public int[] PlayerGuessArr { get; set; }

        public int Difficulty { get; set; } = 4;
        public int[] AnswerToGuess { get; set; }
        public List<string> Feedback { get; set; } = new List<string>();
        public int GuessCount { get; set; }
        public bool GameWon { get; set; }
        public bool GameEnded { get; set; }
    }

}
