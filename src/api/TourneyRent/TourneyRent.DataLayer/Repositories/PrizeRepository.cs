using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer.Repositories;

public class PrizeRepository
{
    private readonly TourneyRentDbContext _context;

    public PrizeRepository(TourneyRentDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(Prize prize)
    {
        await _context.AddAsync(prize).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task UpdateAsync(Prize prize)
    {
        _context.Update(prize);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<Prize> GetByIdAsync(Guid id)
    {
        return await _context.Prizes.SingleOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Prize>> GetAsync(Expression<Func<Prize, bool>> predicate)
    {
        return await _context.Prizes.Where(predicate).ToListAsync();
    }
}