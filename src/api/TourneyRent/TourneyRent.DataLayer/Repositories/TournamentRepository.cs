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

    public async Task<Tournament> DeleteAsync(int id)
    {
        var tournamentToDelete = await _context.Tournaments.SingleAsync(x => x.Id == id);
        var deletedTournament = new Tournament
        {
            Id = tournamentToDelete.Id,
            Name = tournamentToDelete.Name,
            StartDate = tournamentToDelete.StartDate,
            EndDate = tournamentToDelete.EndDate,
            EntryFee = tournamentToDelete.EntryFee,
            ParticipantCount = tournamentToDelete.ParticipantCount,
            ImageId = tournamentToDelete.ImageId,
            OwnerId = tournamentToDelete.OwnerId
        };
        _context.Tournaments.Remove(tournamentToDelete);
        await _context.SaveChangesAsync();
        
        return deletedTournament;
    }

    public async Task CreateAsync(Tournament tournament)
    {
        await _context.AddAsync(tournament);
        await _context.SaveChangesAsync();
    }
}