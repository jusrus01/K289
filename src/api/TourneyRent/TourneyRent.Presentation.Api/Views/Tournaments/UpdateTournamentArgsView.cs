using System.ComponentModel.DataAnnotations;

namespace TourneyRent.Presentation.Api.Views.Tournaments;

public class UpdateTournamentArgsView
{
    public IFormFile ImageFile { get; set; }

    public string BankAccountName { get; set; }
    public string BankAccountNumber { get; set; }
    public string TransactionReason { get; set; }

    [Required] public string Name { get; set; }
    
    [Required] public DateTime? StartDate { get; set; }
    [Required] public DateTime? EndDate { get; set; }
}