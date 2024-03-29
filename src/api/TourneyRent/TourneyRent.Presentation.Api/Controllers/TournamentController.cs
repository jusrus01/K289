using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournament(int id)
    {
        return Ok(await _tournamentService.DeleteAsync(id));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTournament(int id)
    {
        return Ok(await _tournamentService.GetTournamentByIdAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTournaments()
    {
        return Ok(await _tournamentService.GetAllTournamentsAsync());
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateTournament([FromForm] CreateTournamentArgsView createArgs)
    {
        var args = _mapper.Map<CreateTournamentArgs>(
            createArgs,
            opt =>
                opt.AfterMap((_, arg) =>
                    arg.Reservation =
                        JsonConvert.DeserializeObject<List<TournamentReservationArgs>>(createArgs.Reservation)));
        var createdTournament = await _tournamentService.CreateAsync(args);
        return CreatedAtAction(nameof(CreateTournament), createdTournament);
    }

    [Authorize]
    [HttpPost("{tournamentId}/Join")]
    public async Task<IActionResult> JoinTournament([FromRoute] int tournamentId,
        [FromBody] JoinTournamentArgsView joinArgs)
    {
        await _tournamentService.JoinAsync(tournamentId, joinArgs.TeamId);
        return Ok();
    }

    [Authorize]
    [HttpPost("{tournamentId}/Leave")]
    public async Task<IActionResult> LeaveTournament(int tournamentId)
    {
        await _tournamentService.LeaveAsync(tournamentId);
        return Ok();
    }

    [Authorize]
    [HttpGet("Owner/{ownerId}")]
    public async Task<IActionResult> GetOwnerTournaments(string ownerId)
    {
        return Ok(await _tournamentService.GetTournamentsAsync(ownerId));
    }

    [Authorize]
    [HttpGet("User/{userId}")]
    public async Task<IActionResult> GetJoinedTournaments(string userId)
    {
        return Ok(await _tournamentService.GetJoinedTournamentsAsync(userId));
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTournament(int id, [FromForm] UpdateTournamentArgsView tournamentArgs)
    {
        var args = _mapper.Map<UpdateTournamentArgs>(tournamentArgs);
        var updatedTournament = await _tournamentService.UpdateTournamentAsync(id, args);
        return Ok(updatedTournament);
    }

    [Authorize]
    [HttpPost("{tournamentId}/Winner/{userId}")]
    public async Task<IActionResult> SelectWinner(int tournamentId, Guid userId)
    {
        await _tournamentService.SelectWinnerAsync(tournamentId, userId);
        return Ok();
    }
}