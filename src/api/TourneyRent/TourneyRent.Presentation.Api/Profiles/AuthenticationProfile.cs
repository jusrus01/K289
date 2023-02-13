using AutoMapper;
using TourneyRent.Authentication.Models;
using TourneyRent.Presentation.Api.Views.Account;

namespace TourneyRent.Presentation.Api.Profiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<RegisterArgsView, RegisterArgs>();
            CreateMap<LoginArgsView, LoginArgs>();

            CreateMap<CreatedUser, CreatedUserView>();
        }
    }
}
