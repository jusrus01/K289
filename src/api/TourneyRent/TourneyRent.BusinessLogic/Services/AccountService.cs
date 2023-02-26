using Microsoft.AspNetCore.Identity;
using TourneyRent.BusinessLogic.Exceptions;
using TourneyRent.BusinessLogic.Models;
using TourneyRent.DataLayer.Models;
using TourneyRent.DataLayer.Repositories;

namespace TourneyRent.BusinessLogic.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ImageRepository _imageRepository;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            ImageRepository imageRepository)
        {
            _userManager = userManager;
            _imageRepository = imageRepository;
        }

        public async Task<UserProfile> UpdateProfileAsync(UpdateUserProfileArgs updateArgs)
        {
            var user = await GetUserAsync(updateArgs.Id);
            user.ImageId = await _imageRepository.UploadImageAsync(updateArgs, Guid.Parse(user.Id));
            await _userManager.UpdateAsync(user);
            return new UserProfile(user.FirstName, user.LastName, user.ImageId);
        }

        public async Task<UserProfile> GetProfileAsync(string userId)
        {
            var user = await GetUserAsync(userId);
            return new UserProfile(user.FirstName, user.LastName, user.ImageId);
        }

        private static void EnsureValidUser(ApplicationUser user)
        {
            if (user == null)
            {
                throw new NotFoundException("User not found", null);
            }
        }

        private async Task<ApplicationUser> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            EnsureValidUser(user);
            return user;
        }
    }
}
