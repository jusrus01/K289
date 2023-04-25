using AutoMapper;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.RentalItems;
using TourneyRent.BusinessLogic.Models;
using TourneyRent.Presentation.Api.Views.CalendarItem;

namespace TourneyRent.Presentation.Api.Profiles
{
	public class RentalItemProfile : Profile
	{
		public RentalItemProfile() 
		{
			CreateMap<RentalItem, RentalItemView>();
			CreateMap<RentalItemCreate, RentalItem>();

			CreateMap<RentalItem, RentalItemDetailedView>();
			CreateMap<RentalItemCreate, CreateRentalItemArgs>();
			CreateMap<CreateRentalItemArgs, RentalItemDetailedView>();

			CreateMap<CalendarIRentalItemEntry, CalendarItemView>();
			CreateMap<CalendarItemView, CalendarIRentalItemEntry>();
		}
	}
}
