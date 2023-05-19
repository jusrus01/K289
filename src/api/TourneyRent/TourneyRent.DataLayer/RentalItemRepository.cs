using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
			return await _context.RentalItems.Include(x => x.AvailableDays).FirstAsync(x => x.Id == id);
		}

		public async Task<IEnumerable<RentalItem>> GetRentalItemsAsync()
		{
			return await _context.RentalItems.Include(x => x.AvailableDays).OrderByDescending(x => x.Id).ToListAsync();
		}
		public async Task CreateRentalItemAsync(RentalItem rentalItem, CancellationToken cancellationToken)
		{
			_context.RentalItems.Add(rentalItem);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteRentalItemAsync(RentalItem rentalItem)
		{
			if (rentalItem.AvailableDays?.Any(i => i.BuyerId != null) == true)
			{
				throw new Exception("Cannot delete item when there are reserved days. Please contact support.");
			}
			
			_context.RemoveRange(rentalItem.AvailableDays);
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
