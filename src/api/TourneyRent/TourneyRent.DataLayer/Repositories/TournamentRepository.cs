using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer.Repositories;

public class TournamentRepository
{
    private readonly TourneyRentDbContext _context;

    public TournamentRepository(TourneyRentDbContext context)
    {
        _context = context;
    }

    public async Task<Tournament?> GetSingleOrDefaultAsync(Expression<Func<Tournament, bool>> predicate)
    {
        return await _context.Tournaments.Include(x => x.Participants).SingleOrDefaultAsync(predicate);
    }
    
    public async Task<IEnumerable<Tournament>> GetAsync(Expression<Func<Tournament, bool>> predicate)
    {
        return await _context.Tournaments
            .Include(x => x.Participants)
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<Tournament> DeleteAsync(Tournament tournament)
    {
        _context.Participants.RemoveRange(tournament.Participants);
        var deletedTournament = new Tournament
        {
            Id = tournament.Id,
            Name = tournament.Name,
            StartDate = tournament.StartDate,
            EndDate = tournament.EndDate,
            EntryFee = tournament.EntryFee,
            ParticipantCount = tournament.ParticipantCount,
            ImageId = tournament.ImageId,
            OwnerId = tournament.OwnerId
        };
        _context.Tournaments.Remove(tournament);
        await _context.SaveChangesAsync();
        return deletedTournament;
    }
    
    public async Task CreateAsync(Tournament tournament)
    {
        await _context.AddAsync(tournament);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveParticipantAsync(TournamentParticipant participant)
    {
        _context.Participants.Remove(participant);
        await _context.SaveChangesAsync();
    }
}