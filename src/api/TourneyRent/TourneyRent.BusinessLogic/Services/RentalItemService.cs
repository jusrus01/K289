using Microsoft.AspNetCore.Http;
using TourneyRent.BusinessLogic.Extensions;
using TourneyRent.DataLayer;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.BusinessLogic.Services
{
	public class RentalItemService
	{
		private readonly RentalItemRepository _rentalItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RentalItemService (RentalItemRepository rentalItemRepository, IHttpContextAccessor httpContextAccessor)
		{
			_rentalItemRepository = rentalItemRepository;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<RentalItem> GetRentalItemAsync (int id)
		{
			return await _rentalItemRepository.GetRentalItemAsync(id);
		}

		public async Task<IEnumerable<RentalItem>> GetRentalItemsAsync()
		{
			return await _rentalItemRepository.GetRentalItemsAsync();
		}

		public async Task CreateRentalItemAsync(RentalItem item)
		{
			item.OwnerId = _httpContextAccessor.GetAuthenticatedUserId();
			await _rentalItemRepository.CreateRentalItemAsync(item);
		}
	}
}
