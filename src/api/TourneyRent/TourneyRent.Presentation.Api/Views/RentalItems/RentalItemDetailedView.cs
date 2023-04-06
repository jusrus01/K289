namespace TourneyRent.Presentation.Api.Views.RentalItems
{
	public class RentalItemDetailedView
	{
		public IFormFile? ImageFile { get; set; }

		public string? BankAccountName { get; set; }
		public string? BankAccountNumber { get; set; }
		public string? TransactionReason { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime PeriodStart { get; set; }
		public DateTime PeriodEnd { get; set; }
		public int Price { get; set; }
	}
}
