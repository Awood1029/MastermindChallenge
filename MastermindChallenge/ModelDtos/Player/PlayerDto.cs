using System.ComponentModel.DataAnnotations;

namespace MastermindChallenge.API.ModelDtos.Player
{
    public class PlayerDto : PlayerLoginDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
