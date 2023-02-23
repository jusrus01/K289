using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.DataLayer;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.Team;

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
        public async Task<ActionResult<TeamRead>> GetTeamById(int id)
        {
            var team = await _teamService.GetTeamById(id);
            if(team==null)
            {
                return NotFound();
            }

            var teamRead = _mapper.Map<TeamRead>(team);
            return Ok(teamRead);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamRead>>> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeams();
            var teamRead = _mapper.Map<IEnumerable<TeamRead>>(teams);
            return Ok(teamRead);
        }

        [HttpPost]
        public async Task<ActionResult<TeamRead>> AddTeam(TeamCreate teamCreate)
        {
            var team = _mapper.Map<Team>(teamCreate);
            await _teamService.AddTeam(team);
            var teamRead = _mapper.Map<TeamRead>(team);
            return CreatedAtAction(nameof(GetTeamById), new { id = teamRead.Id }, teamRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, TeamUpdate teamUpdate)
        {
            var team = _mapper.Map<Team>(teamUpdate);
            team.Id = id;

            try
            {
                await _teamService.UpdateTeam(team);
            }
            catch(DbUpdateConcurrencyException)
            {
                if(await _teamService.GetTeamById(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _teamService.GetTeamById(id);
            if(team == null)
            {
                return NotFound();
            }

            await _teamService.DeleteTeam(team);

            return NoContent();
        }

        [HttpPost("{teamId}/players")]
        public async Task<ActionResult<ApplicationUser>> AddPlayer(int teamId, ApplicationUser player)
        {
            await _teamService.AddPlayer(teamId, player);
            return CreatedAtAction(nameof(GetPlayerById), new { teamId, id = player.Id }, player);
        }

        [HttpGet("{teamId}/players/{id}")]
        public async Task<ActionResult<ApplicationUser>> GetPlayerById(int teamId, int id)
        {
            var team = await _teamService.GetTeamById(teamId);
            if(team == null)
            {
                return NotFound();
            }

            var player = team.Players.FirstOrDefault(p => Convert.ToInt32(p.Id) == id);
            if(player==null)
            {
                return NotFound(); ;
            }
            return Ok(player);
        }

    }
}
