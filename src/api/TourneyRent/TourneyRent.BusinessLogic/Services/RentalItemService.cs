using TourneyRent.BusinessLogic.Models.Tournaments;
using Microsoft.AspNetCore.Http;
using TourneyRent.BusinessLogic.Extensions;
using TourneyRent.DataLayer;
using TourneyRent.DataLayer.Models;
using TourneyRent.DataLayer.Repositories;
using TourneyRent.BusinessLogic.Models;
using AutoMapper;

namespace TourneyRent.BusinessLogic.Services
{
	public class RentalItemService
	{
		private readonly RentalItemRepository _rentalItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ImageRepository _imageRepository;
		private readonly IMapper _mapper;
		private readonly TransactionExecutor _executor;
		private readonly PaymentTransactionRepository _paymentTransactionRepository;

		public RentalItemService (
			RentalItemRepository rentalItemRepository,
			IHttpContextAccessor httpContextAccessor,
			IMapper mapper,
			ImageRepository imageRepository,
			TransactionExecutor executor,
			PaymentTransactionRepository paymentTransactionRepository)
		{
			_rentalItemRepository = rentalItemRepository;
			_httpContextAccessor = httpContextAccessor;
			_imageRepository = imageRepository;
			_mapper = mapper;
			_executor = executor;
			_paymentTransactionRepository = paymentTransactionRepository;
		}

		public async Task<RentalItem> GetRentalItemAsync (int id)
		{
			return await _rentalItemRepository.GetRentalItemAsync(id);
		}

		public async Task<IEnumerable<RentalItem>> GetRentalItemsAsync()
		{
			return await _rentalItemRepository.GetRentalItemsAsync();
		}

		public async Task<RentalItem> CreateRentalItemAsync(CreateRentalItemArgs createArgs)
		{
			var image = await _imageRepository.UploadImageAsync(createArgs);

			var rentalItem = new RentalItem
			{
				BankAccountName = createArgs.BankAccountName,
				BankAccountNumber = createArgs.BankAccountNumber,
				TransactionReason = createArgs.TransactionReason,

				ImageId = image,
				Name = createArgs.Name,
				Description = createArgs.Description,
				Price = createArgs.Price,
				AvailableDays = createArgs.CalendarItems.Select(day => new CalendarIRentalItemEntry { AvailableAt = day, Price = createArgs.Price}).ToList(),

				HighlightFee = 0,

				OwnerId = _httpContextAccessor.GetAuthenticatedUserId()
			};

			await _rentalItemRepository.CreateRentalItemAsync(rentalItem);
			return rentalItem;
		}
		public async Task DeleteRentalItemAsync(RentalItem rentalItem)
		{
			await _rentalItemRepository.DeleteRentalItemAsync(rentalItem);

		}
		public async Task UpdateTeamAsync(RentalItem rentalItem)
		{
			await _rentalItemRepository.UpdateRentalItemAsync(rentalItem);

		}
		public async Task UpdateHighlightCostAsync(int id, int? fee)
		{
			var rentalItem = await GetRentalItemAsync(id);
			var userId = _httpContextAccessor.GetAuthenticatedUserId();

			await _executor.ExecuteAsync(async _ =>
			{
				var transactionId = await _paymentTransactionRepository.CreateAsync(
					userId,
					Convert.ToDecimal(fee));

				rentalItem.HighlightFee += Convert.ToDecimal(fee);
			});
			await _rentalItemRepository.UpdateRentalItemAsync(rentalItem);
		}
	}
}
