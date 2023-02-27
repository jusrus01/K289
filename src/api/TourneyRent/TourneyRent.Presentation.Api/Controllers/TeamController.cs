using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.DataLayer;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.Teams;

namespace TourneyRent.Presentation.Api.Controllers
{

    [Route("Team")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;

        private readonly IMapper _mapper;

        public TeamController(TeamService teamservice, IMapper mapper)
        {
            _teamService = teamservice;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamView>> GetTeamByIdAsync(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if(team==null)
            {
                return NotFound();
            }

            var teamRead = _mapper.Map<TeamView>(team);
            return Ok(teamRead);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamView>>> GetAllTeamsAsync()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            var teamRead = _mapper.Map<IEnumerable<TeamView>>(teams);
            return Ok(teamRead);
        }

        [HttpPost]
        public async Task<ActionResult<TeamView>> AddTeamAsync(TeamCreate teamCreate)
        {
            var team = _mapper.Map<Team>(teamCreate);
            await _teamService.AddTeamAsync(team);
            var teamRead = _mapper.Map<TeamView>(team);
            return CreatedAtAction(nameof(GetTeamByIdAsync), new { id = teamRead.Id }, teamRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeamAsync(int id, TeamUpdate teamUpdate)
        {

            var team = _mapper.Map<Team>(teamUpdate);
            team.Id = id;

            await _teamService.UpdateTeamAsync(team);

            if(await _teamService.GetTeamByIdAsync(id) == null)
            {
                return NotFound();
            }

             

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamAsync(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if(team == null)
            {
                return NotFound();
            }

            await _teamService.DeleteTeamAsync(team);

            return NoContent();
        }

        [HttpPost("{teamId}/players")]
        public async Task<ActionResult<ApplicationUser>> AddPlayerAsync(int teamId, ApplicationUser player)
        {
            await _teamService.AddPlayerAsync(teamId, player);
            return CreatedAtAction(nameof(GetPlayerByIdAsync), new { teamId, id = player.Id }, player);
        }

        [HttpGet("{teamId}/players/{id}")]
        public async Task<ActionResult<ApplicationUser>> GetPlayerByIdAsync(int teamId, int id)
        {
            var player = _teamService.GetPlayerByIdAsync(teamId, id);
            return Ok(player);
        }

    }
}
