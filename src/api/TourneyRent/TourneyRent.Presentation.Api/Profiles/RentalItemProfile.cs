using AutoMapper;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.RentalItems;

namespace TourneyRent.Presentation.Api.Profiles
{
	public class RentalItemProfile : Profile
	{
		public RentalItemProfile() 
		{
			CreateMap<RentalItem, RentalItemView>();
			CreateMap<RentalItemCreate, RentalItem>();
		}
	}
}
