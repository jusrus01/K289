using TourneyRent.BusinessLogic.Models.Tournaments;
using TourneyRent.DataLayer;
using TourneyRent.DataLayer.Models;
using TourneyRent.DataLayer.Repositories;

namespace TourneyRent.BusinessLogic.Services
{
	public class RentalItemService
	{
		private readonly RentalItemRepository _rentalItemRepository;

		public RentalItemService (RentalItemRepository rentalItemRepository)
		{
			_rentalItemRepository = rentalItemRepository;
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
			await _rentalItemRepository.CreateRentalItemAsync(item);
		}
		public async Task DeleteRentalItemAsync(RentalItem rentalItem)
		{
			await _rentalItemRepository.DeleteRentalItemAsync(rentalItem);

		}
		public async Task UpdateTeamAsync(RentalItem rentalItem)
		{
			await _rentalItemRepository.UpdateRentalItemAsync(rentalItem);

		}
	}
}
