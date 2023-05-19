using TourneyRent.Contracts.Models;

namespace TourneyRent.Presentation.Api.Views.RentalItems;

public class ManageRentalItemView : IImageUpload
{
    public IFormFile ImageFile { get; set; }

    public string BankAccountName { get; set; }
    public string BankAccountNumber { get; set; }
    public string TransactionReason { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }

    public string AvailableAt { get; set; }
    
    public List<DateTime> CalendarItems { get; set; }
}