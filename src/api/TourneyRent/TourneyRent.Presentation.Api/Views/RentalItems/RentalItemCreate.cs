namespace TourneyRent.Presentation.Api.Views.RentalItems
{
	public class RentalItemCreate
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public DateTime PeriodStart { get; set; }
		public DateTime PeriodEnd { get; set; }
		public int Price { get; set; }
	}
}
