using System;
using System.Collections.Generic;

namespace MastermindChallenge.API.Data;

public partial class Game
{
    public int Id { get; set; }

    public int Difficulty { get; set; }
    public int[] AnswerToGuess { get; set; }
    public int AttemptsUsed { get; set; }
    public bool IsWinner { get; set; }
}
