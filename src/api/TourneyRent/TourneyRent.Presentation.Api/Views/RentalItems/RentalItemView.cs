using System.ComponentModel.DataAnnotations;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.CalendarItem;

namespace TourneyRent.Presentation.Api.Views.RentalItems
{
	public class RentalItemView
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public Guid? ImageId { get; set; }

		public ICollection<CalendarItemView> AvailableDays { get; set; }
	}
}
