using TourneyRent.Presentation.Api.Views.CalendarItem;

namespace TourneyRent.Presentation.Api.Views.RentalItems
{
	public class RentalItemDetailedView
	{
		public Guid? ImageId { get; set; }

		public string? BankAccountName { get; set; }
		public string? BankAccountNumber { get; set; }
		public string? TransactionReason { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public List<CalendarItemView> AvailableDays { get; set; } 
	}
}
