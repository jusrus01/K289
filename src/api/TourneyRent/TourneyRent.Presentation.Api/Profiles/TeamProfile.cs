using AutoMapper;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.Team;

namespace TourneyRent.Presentation.Api.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamRead>();
            CreateMap<TeamCreate, Team>();
            CreateMap<TeamUpdate, Team>();
        }

    }
}
