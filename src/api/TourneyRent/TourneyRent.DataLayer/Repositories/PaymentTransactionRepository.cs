using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer.Repositories;

public class PaymentTransactionRepository
{
    private readonly TourneyRentDbContext _context;

    public PaymentTransactionRepository(TourneyRentDbContext context)
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

    public async Task RemoveAsync(Guid transactionId)
    {
        _context.Remove(new Transaction { Id = transactionId });
        await _context.SaveChangesAsync();
    }
}