﻿using Microsoft.EntityFrameworkCore;
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

       public async Task<Team> GetTeamByIdAsync(int id)
        {
            return await _context.Teams.AsNoTracking().Include(t => t.Members).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams.Include(t => t.Members).ToListAsync();
        }

        public async Task AddTeamAsync(Team team)
        {
            
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateTeamAsync(Team team)
        {
            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(Team team)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }
       
        public async Task AddTeamMemberAsync(TeamMember teamMember)
        {
            await _context.TeamMembers.AddAsync(teamMember);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTeamMemberAsync(TeamMember teamMember)
        {
            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();
        }

        public async Task<TeamMember> GetTeamMemberByIdAsync(int teamId, string memberId)
        {
            return await _context.TeamMembers.AsNoTracking()
                .FirstOrDefaultAsync(t => t.TeamId == teamId && t.UserId == memberId);
                
        }

        public async Task<List<TeamMember>> GetTeamMembersAsync(int teamId)
        {
            return await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId)
                .ToListAsync();
        }

        public async Task UpdateTeamMemberAsync(TeamMember teamMember)
        {
            _context.Entry(teamMember).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Team>> GetTeamsByUserIdAsync(string userId)
        {
           var ids =  await _context.TeamMembers.Where(tm => tm.UserId == userId)
                .Select(tm => tm.TeamId)
                .ToListAsync();

            return await _context.Teams.Where(t => ids.Contains(t.Id)).ToListAsync();        }


    }
}
