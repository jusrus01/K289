using TourneyRent.BusinessLogic.Models;
using TourneyRent.Presentation.Api.Views.Account;

namespace TourneyRent.Presentation.Api.Profiles
{
    public class AccountProfile : AutoMapper.Profile
    {
        public AccountProfile()
        {
            CreateMap<UserProfile, UserProfileView>();
            CreateMap<UpdateUserProfileArgsView, UpdateUserProfileArgs>();
        }
    }
}