namespace MastermindChallenge.API.ModelDtos.Game
{
    public class SaveGameDto
    {
        public int Difficulty { get; set; }
        public int[] AnswerToGuess { get; set; }
        public int AttemptsUsed { get; set; }
        public bool IsWinner { get; set; }
        public string PlayerId { get; set; }
    }
}
