using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourneyRent.BusinessLogic.Services;

namespace TourneyRent.Presentation.Api.Controllers;

[Authorize]
[Route("Prize")]
[ApiController]
public class PrizeController : ControllerBase
{
    private readonly PrizeService _prizeService;

    public PrizeController(PrizeService prizeService)
    {
        _prizeService = prizeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailablePrizes()
    {
        return Ok(await _prizeService.GetAvailablePrizesAsync());
    }
}