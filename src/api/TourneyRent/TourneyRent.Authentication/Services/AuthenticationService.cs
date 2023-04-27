using Microsoft.AspNetCore.Identity;
using System.Security.Authentication;
using TourneyRent.Authentication.Models;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.Authentication.Services
{
    public class AuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserInfo> SignInAsync(LoginArgs loginArgs)
        {
            var user = await _userManager.FindByEmailAsync(loginArgs.Email);
            EnsureValidUser(user);
            await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(user, loginArgs.Password, false, false);
            EnsureValidCredentials(result);
            var roles = await _userManager.GetRolesAsync(user);
            return new UserInfo(user.Id, roles);
        }

        public async Task<CreatedUser> RegisterAsync(RegisterArgs registerArgs)
        {
            await EnsureUserDoesNotExistAsync(registerArgs);
            return await CreateNewUserAsync(registerArgs);
        }

        private async Task<CreatedUser> CreateNewUserAsync(RegisterArgs registerArgs)
        {
            var newUser = new ApplicationUser
            {
                Email = registerArgs.Email,
                UserName = registerArgs.Email,
                FirstName = registerArgs.FirstName,
                LastName = registerArgs.LastName
            };

            var identityResult = await _userManager.CreateAsync(newUser, registerArgs.Password);
            if (!identityResult.Succeeded)
            {
                throw new AuthenticationException("Failed to create an account.");
            }

            return new CreatedUser(newUser.Email, newUser.FirstName, newUser.LastName);
        }

        private async Task EnsureUserDoesNotExistAsync(RegisterArgs registerArgs)
        {
            if (await IsUserAlreadyRegisteredAsync(registerArgs.Email))
            {
                throw new AuthenticationException("User is already registered");
            }
        }

        private async Task<bool> IsUserAlreadyRegisteredAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        private static void EnsureValidUser(ApplicationUser user)
        {
            if (user == null)
            {
                throw new AuthenticationException("User not found");
            }
        }

        private static void EnsureValidCredentials(SignInResult result)
        {
            if (!result.Succeeded)
            {
                throw new AuthenticationException("Invalid credentials");
            }
        }
    }
}
