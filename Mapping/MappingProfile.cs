using AutoMapper;
using TeamApi.Dtos;
using TeamApi.Models;

namespace TeamApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Team, TeamDto>().ReverseMap();
        CreateMap<TeamCreateDto, Team>();
    }
}
