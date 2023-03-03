﻿using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.DataLayer;
using TourneyRent.DataLayer.Enumerators;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.TeamMembers;
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

        [HttpPost("{teamId}/members")]
        public async Task<ActionResult<TeamMemberView>> AddTeamMemberAsync(int teamId, TeamMemberCreate teamMemberCreate)
        {
            var team = await _teamService.GetTeamByIdAsync(teamId);
            if(team == null)
            {
                return NotFound();
            }

            //if(team.CreatorId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            //{
            //    teamMemberCreate.Role = TeamRole.TeamLeader;
            //}

            var teamMember = _mapper.Map<TeamMember>(teamMemberCreate);
            teamMember.TeamId = teamId;
            await _teamService.AddTeamMemberAsync(teamMember);
            
            

            var teamMemberRead = _mapper.Map<TeamMemberView>(teamMember);
            return CreatedAtAction(nameof(GetTeamMemberByIdAsync), new { teamId = teamMember.TeamId, memberId = teamMemberRead.UserId }, teamMemberRead);
        }

        [HttpGet("{teamId}/members/{memberId}")]
        public async Task<ActionResult<TeamMemberView>> GetTeamMemberByIdAsync(int teamId, string memberId)
        {
            var teamMember = await _teamService.GetTeamMemberByIdAsync(teamId, memberId);
            if(teamMember == null)
            {
                return NotFound();
            }

            var member = _mapper.Map<TeamMemberView>(teamMember);
            return Ok(member);
        }

        [HttpGet("{teamId}/members")]
        public async Task<ActionResult<List<TeamMemberView>>> GetTeamMembersAsync(int teamId)
        {
            var teamMembers = await _teamService.GetTeamMembersAsync(teamId);
            return Ok(_mapper.Map<List<TeamMemberView>>(teamMembers));
        }

    }
}
