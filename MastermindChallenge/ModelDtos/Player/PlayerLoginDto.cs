using System.ComponentModel.DataAnnotations;

namespace MastermindChallenge.API.ModelDtos.Player
{
    public class PlayerLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
