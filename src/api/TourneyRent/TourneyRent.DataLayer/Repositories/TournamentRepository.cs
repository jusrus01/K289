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

    public async Task<IEnumerable<Tournament>> GetAsync(Expression<Func<Tournament, bool>> predicate)
    {
        return await _context.Tournaments
            .Where(predicate)
            .ToListAsync();
    }

    public async Task CreateAsync(Tournament tournament)
    {
        await _context.AddAsync(tournament);
        await _context.SaveChangesAsync();
    }
}