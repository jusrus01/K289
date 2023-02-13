using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TourneyRent.Authentication.Models;
using TourneyRent.Authentication.Services;
using TourneyRent.Presentation.Api.Views.Account;

namespace TourneyRent.Presentation.Api.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AuthenticationService _authenticationService;

        public AccountController(IMapper mapper, AuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginArgsView loginArgs)
        {
            await _authenticationService.SignInAsync(_mapper.Map<LoginArgs>(loginArgs));
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterArgsView registerArgs)
        {
            var createdUser = await _authenticationService.RegisterAsync(_mapper.Map<RegisterArgs>(registerArgs));
            return Ok(_mapper.Map<CreatedUserView>(createdUser));
        }
    }
}
