using AutoMapper;
using MastermindChallenge.API.Data;
using MastermindChallenge.API.ModelDtos.Game;
using MastermindChallenge.API.ModelDtos.Player;

namespace MastermindChallenge.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<PlayerDto, Player>().ReverseMap();
            CreateMap<GameCreateDto, Game>().ReverseMap();
        }
    }
}
