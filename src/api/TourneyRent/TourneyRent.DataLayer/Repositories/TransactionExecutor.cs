namespace TourneyRent.DataLayer.Repositories;

public class TransactionExecutor
{
    private readonly TourneyRentDbContext _context;

    public TransactionExecutor(TourneyRentDbContext context)
    {
        _context = context;
    }
    
    public async Task ExecuteAsync(Func<Task> transactionTask)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await transactionTask();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}