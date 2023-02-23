using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer
{
    public class TeamRepository
    {
        private readonly TourneyRentDbContext _context;

        public TeamRepository(TourneyRentDbContext context)
        {
            _context = context;
        }

       public async Task<Team> GetTeamById(int id)
        {
            return await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _context.Teams.Include(t => t.Players).ToListAsync();
        }

        public async Task AddTeam(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeam(Team team)
        {
            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeam(Team team)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }
       

        

    }
}
