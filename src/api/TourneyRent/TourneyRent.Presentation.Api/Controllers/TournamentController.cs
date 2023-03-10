using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourneyRent.BusinessLogic.Models.Tournaments;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.Presentation.Api.Views.Tournaments;

namespace TourneyRent.Presentation.Api.Controllers;

[Route("Tournament")]
[ApiController]
public class TournamentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly TournamentService _tournamentService;

    public TournamentController(IMapper mapper, TournamentService tournamentService)
    {
        _mapper = mapper;
        _tournamentService = tournamentService;
    }

    [Authorize, HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournament(int id)
    {
        return Ok(await _tournamentService.DeleteAsync(id));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllValidTournaments(int id)
    {
        return Ok(await _tournamentService.GetTournamentByIdAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllValidTournaments()
    {
        return Ok(await _tournamentService.GetAllValidAsync());
    }

    [Authorize, HttpPost]
    public async Task<IActionResult> CreateTournament([FromForm] CreateTournamentArgsView createArgs)
    {
        var args = _mapper.Map<CreateTournamentArgs>(createArgs);
        var createdTournament = await _tournamentService.CreateAsync(args);
        return CreatedAtAction(nameof(CreateTournament), createdTournament);
    }
}