using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TourneyRent.BusinessLogic.Exceptions;
using TourneyRent.BusinessLogic.Extensions;
using TourneyRent.BusinessLogic.Models;
using TourneyRent.Contracts.Models;
using TourneyRent.DataLayer.Models;
using TourneyRent.DataLayer.Repositories;

namespace TourneyRent.BusinessLogic.Services;

public class AccountService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ImageRepository _imageRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountService(
        UserManager<ApplicationUser> userManager,
        ImageRepository imageRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _imageRepository = imageRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> ChangeProfileImageAsync(IImageUpload image, string? userId = null)
    {
        var resolvedUserId = userId ?? _httpContextAccessor.GetAuthenticatedUserId();
        var user = await GetUserAsync(resolvedUserId);
        var uploadedImageId = await _imageRepository.UploadImageAsync(image, user.ImageId ?? Guid.NewGuid());
        user.ImageId = uploadedImageId;
        await _userManager.UpdateAsync(user);
        return uploadedImageId.Value;
    }

    public async Task<UserProfile> UpdateProfileAsync(UpdateUserProfileArgs updateArgs)
    {
        var user = await GetUserAsync(updateArgs.Id);

        user.FirstName = updateArgs.FirstName;
        user.LastName = updateArgs.LastName;
        user.Email = updateArgs.Email;

        await _userManager.UpdateAsync(user);
        return new UserProfile(user.Email, user.FirstName, user.LastName, user.ImageId);
    }

    public async Task<UserProfile> GetProfileAsync(string userId)
    {
        var user = await GetUserAsync(userId);
        return new UserProfile(user.Email, user.FirstName, user.LastName, user.ImageId);
    }

    private static void EnsureValidUser(ApplicationUser user)
    {
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
    }

    private async Task<ApplicationUser> GetUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        EnsureValidUser(user);
        return user;
    }

    public async Task<IEnumerable<ApplicationUser>> GetUsersAsync(string search = null)
    {
        var users = string.IsNullOrWhiteSpace(search)
            ? await _userManager.Users.ToListAsync()
            : await _userManager.Users.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search))
                .ToListAsync();

        return users;
    }
}