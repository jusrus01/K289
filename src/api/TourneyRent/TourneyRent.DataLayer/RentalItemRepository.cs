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
			return await _context.RentalItems.Include(x => x.AvailableDays).OrderBy(x => x.HighlightFee).ToListAsync();
		}
		public async Task CreateRentalItemAsync(RentalItem rentalItem)
		{
			_context.RentalItems.Add(rentalItem);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteRentalItemAsync(RentalItem rentalItem)
		{
			_context.RemoveRange(rentalItem.AvailableDays);
			_context.Remove(rentalItem.ImageId);
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
