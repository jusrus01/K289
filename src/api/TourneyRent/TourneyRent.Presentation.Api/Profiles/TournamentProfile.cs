using AutoMapper;
using TourneyRent.BusinessLogic.Models.Tournaments;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.Teams;
using TourneyRent.Presentation.Api.Views.Tournaments;

namespace TourneyRent.Presentation.Api.Profiles;

public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<TournamentParticipant, TournamentParticipantInfo>();
        CreateMap<Tournament, TournamentInfo>()
            .ForMember(dest => dest.Prize, opt => opt.MapFrom(i => i.Prizes.FirstOrDefault()));
        CreateMap<CreateTournamentArgsView, CreateTournamentArgs>();
        CreateMap<UpdateTournamentArgs, Tournament>();
        CreateMap<UpdateTournamentArgsView, UpdateTournamentArgs>();
    }
}