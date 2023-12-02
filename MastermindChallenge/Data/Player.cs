using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MastermindChallenge.API.Data;

public partial class Player : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    ICollection<Game> Games { get; set; }
}
