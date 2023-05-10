using AutoMapper;
using TourneyRent.BusinessLogic.Models.Tournaments;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.Tournaments;

namespace TourneyRent.Presentation.Api.Profiles;

public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<TournamentParticipant, TournamentParticipantInfo>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(i => i.User != null ? i.User.Email : ""));
        CreateMap<Tournament, TournamentInfo>()
            .ForMember(dest => dest.Prize, opt => opt.MapFrom(i => i.Prizes.FirstOrDefault()));
        CreateMap<CreateTournamentArgsView, CreateTournamentArgs>()
            .ForMember(i => i.Reservation, j => j.Ignore());
        CreateMap<UpdateTournamentArgs, Tournament>();
        CreateMap<UpdateTournamentArgsView, UpdateTournamentArgs>();
    }
}