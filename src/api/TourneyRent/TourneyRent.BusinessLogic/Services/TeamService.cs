using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Team> GetTeamById(int id)
        {
            return await _teamRepository.GetTeamById(id);
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _teamRepository.GetAllTeams();
        }

        public async Task AddTeam(Team team)
        {
            await _teamRepository.AddTeam(team);
        }

        public async Task UpdateTeam(Team team)
        {
            await _teamRepository.UpdateTeam(team);
        }

        public async Task DeleteTeam(Team team)
        {
            await _teamRepository.DeleteTeam(team);
        }

        public async Task AddPlayer(int teamId, ApplicationUser player)
        {
            var team = await _teamRepository.GetTeamById(teamId);
            if(team == null)
            {
                throw new ArgumentException("Team not found.");
            }

            //player.TeamId = teamId (Jei zaidejas gali tureti tik viena komanda)
            team.Players.Add(player);

            await _teamRepository.UpdateTeam(team);
        }


    }
}
