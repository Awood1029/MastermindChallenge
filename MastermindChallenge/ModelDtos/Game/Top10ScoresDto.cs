namespace MastermindChallenge.API.ModelDtos.Game
{
    public class Top10ScoresDto
    {
        public string Username { get; set; }
        public int AttemptsUsed { get; set; }
        public int Difficulty { get; set; }
        public bool IsWinner { get; set; }
    }
}
