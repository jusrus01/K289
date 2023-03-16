using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer
{
	public class RentalItemRepository
	{
		private readonly TourneyRentDbContext _context;

		public RentalItemRepository(TourneyRentDbContext context)
		{
			_context = context;
		}

		public async Task<RentalItem> GetRentalItemAsync(int id)
		{
			return await _context.RentalItems.FindAsync(id);
		}

		public async Task<IEnumerable<RentalItem>> GetRentalItemsAsync()
		{
			return await _context.RentalItems.ToListAsync();
		}
		public async Task<int> CreateRentalItemAsync(RentalItem rentalItem)
		{
			_context.RentalItems.Add(rentalItem);
			await _context.SaveChangesAsync();
			return rentalItem.Id;
		}
		/*public async Task<RentalItem> DeleteAsync(int id)
		{
			var rentalItemToDelete = await _context.RentalItems.SingleAsync(x => x.Id == id);
			var deletedRentalItem = new RentalItem
			{
				Id = rentalItemToDelete.Id,
				Image = rentalItemToDelete.Image,
				PeriodStart = rentalItemToDelete.PeriodStart,
				PeriodEnd = rentalItemToDelete.PeriodEnd,
				Description = rentalItemToDelete.Description,
				Price = rentalItemToDelete.Price
			};
			_context.RentalItems.Remove(rentalItemToDelete);
			await _context.SaveChangesAsync();

			return rentalItemToDelete;
		}*/
		public async Task DeleteRentalItemAsync(RentalItem rentalItem)
		{
			_context.RentalItems.Remove(rentalItem);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateRentalItemAsync(RentalItem rentalItem)
		{
			_context.Entry(rentalItem).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
