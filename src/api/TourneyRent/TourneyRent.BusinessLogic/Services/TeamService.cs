using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourneyRent.BusinessLogic.Exceptions;
using TourneyRent.BusinessLogic.Extensions;
using TourneyRent.DataLayer;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.BusinessLogic.Services
{
    public class TeamService
    {
        private readonly TeamRepository _teamRepository;
        
        public TeamService(TeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
            
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {

            return await _teamRepository.GetTeamByIdAsync(id);



        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _teamRepository.GetAllTeamsAsync();
        }

        public async Task AddTeamAsync(Team team)
        {
            await _teamRepository.AddTeamAsync(team);

        

        }

        public async Task UpdateTeamAsync(Team team)
        {
            await _teamRepository.UpdateTeamAsync(team);

        }

        public async Task DeleteTeamAsync(Team team)
        {
            await _teamRepository.DeleteTeamAsync(team);

        }

        public async Task AddTeamMemberAsync(TeamMember teamMember)
        {
            await _teamRepository.AddTeamMemberAsync(teamMember);
        }

        public async Task RemoveTeamMemberAsync(TeamMember teamMember)
        {
            await _teamRepository.RemoveTeamMemberAsync(teamMember);
        }

        public async Task<TeamMember> GetTeamMemberByIdAsync(int teamId, string memberId)
        {
            var teamMember = await _teamRepository.GetTeamMemberByIdAsync(teamId,memberId);
            if (teamMember == null)
            {
                throw new ArgumentException($"Team member not found.");
            }

            return teamMember;
        }

        public async Task<List<TeamMember>> GetTeamMembersAsync(int teamId)
        {
            var teamMembers = await _teamRepository.GetTeamMembersAsync(teamId);
            return teamMembers;
        }

        public async Task UpdateTeamMemberAsync(TeamMember teamMember)
        {
            await _teamRepository.UpdateTeamMemberAsync(teamMember);

        }

        public async Task<IEnumerable<Team>> GetTeamsByUserIdAsync(string userId)
        {
            return await _teamRepository.GetTeamsByUserIdAsync(userId);
        }




    }
}
