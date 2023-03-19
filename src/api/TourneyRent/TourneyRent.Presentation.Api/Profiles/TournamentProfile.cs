using AutoMapper;
using TourneyRent.BusinessLogic.Models.Tournaments;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.Tournaments;

namespace TourneyRent.Presentation.Api.Profiles;

public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<Tournament, TournamentInfo>()
            .ForMember(
                dest => dest.Participants,
                opt => 
                    opt.MapFrom(x => new List<TournamentParticipantInfo>()));
        CreateMap<CreateTournamentArgsView, CreateTournamentArgs>();
    }
}