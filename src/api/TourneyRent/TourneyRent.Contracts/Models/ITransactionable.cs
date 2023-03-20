namespace TourneyRent.Contracts.Models;

public interface ITransactionable
{
    string BankAccountName { get; set; }
    string BankAccountNumber { get; set; }
    string TransactionReason { get; set; }
}