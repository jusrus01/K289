using AutoMapper;
using TourneyRent.BusinessLogic.Models.Prizes;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.Presentation.Api.Profiles;

public class PrizeProfile : Profile
{
    public PrizeProfile()
    {
        CreateMap<Prize, PrizeInfo>();
    }
}