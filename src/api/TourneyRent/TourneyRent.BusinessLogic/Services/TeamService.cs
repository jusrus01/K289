using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourneyRent.BusinessLogic.Exceptions;
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

        public async Task AddPlayerAsync(int teamId, ApplicationUser player)
        {
            var team = await _teamRepository.GetTeamByIdAsync(teamId);
            if(team == null)
            {
                throw new ArgumentException("Team not found.");
            }

            //player.TeamId = teamId (Jei zaidejas gali tureti tik viena komanda)
            team.Players.Add(player);

            await _teamRepository.UpdateTeamAsync(team);
        }

        public async Task<ApplicationUser> GetPlayerByIdAsync(int teamId, int id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(teamId);
            if (team == null)
            {
                throw new ArgumentNullException("Team was not found");
            }

            var player = team.Players.FirstOrDefault(p => Convert.ToInt32(p.Id) == id);
            if (player == null)
            {
                throw new ArgumentNullException("Player was not found");
            }

            return player;
        }



    }
}
