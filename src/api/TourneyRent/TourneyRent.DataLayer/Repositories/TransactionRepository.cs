using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer.Repositories;

public class TransactionRepository
{
    private readonly TourneyRentDbContext _context;

    public TransactionRepository(TourneyRentDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> CreateAsync(string userId, decimal totalPrice)
    {
        var id = Guid.NewGuid();
        await _context.AddAsync(new Transaction
        {
            Id = id,
            UserId = userId,
            TotalPrice = totalPrice
        });
        await _context.SaveChangesAsync();
        return id;
    }
}