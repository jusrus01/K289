using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourneyRent.Authentication.Models;
using TourneyRent.Authentication.Services;
using TourneyRent.BusinessLogic.Models;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.Presentation.Api.Views.Account;

namespace TourneyRent.Presentation.Api.Controllers
{
    [Route("Account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly AuthenticationService _authenticationService;
        private readonly AccountService _accountService;

        public AccountController(
            IMapper mapper,
            AuthenticationService authenticationService,
            AccountService accountService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
            _accountService = accountService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginArgsView loginArgs)
        {
            var userRoles = await _authenticationService.SignInAsync(_mapper.Map<LoginArgs>(loginArgs));
            return Ok(_mapper.Map<UserInfoView>(userRoles));
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterArgsView registerArgs)
        {
            var createdUser = await _authenticationService.RegisterAsync(_mapper.Map<RegisterArgs>(registerArgs));
            return Ok(_mapper.Map<CreatedUserView>(createdUser));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProfile(string id)
        {
            return Ok(_mapper.Map<UserProfileView>(await _accountService.GetProfileAsync(id)));
        }

        [HttpPost("Profile/ChangeMyImage")]
        public async Task<IActionResult> ChangeMyProfileImage([FromForm] ChangeMyProfileImageArgsView myProfileChangeArgs)
        {
            var assignedImageId = await _accountService.ChangeProfileImageAsync(myProfileChangeArgs);
            return Ok(new { ImageId = assignedImageId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UpdateUserProfileArgsView updateArgs)
        {
            var args = _mapper.Map<UpdateUserProfileArgs>(updateArgs);
            return Ok(_mapper.Map<UserProfileView>(await _accountService.UpdateProfileAsync(args)));
        }

        [HttpGet("Search/{search}")]
        public async Task<IActionResult> GetUsersAsync(string search = null)
        {
            return Ok(await _accountService.GetUsersAsync(search));
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _accountService.GetUsersAsync(null));
        }
    }
}
