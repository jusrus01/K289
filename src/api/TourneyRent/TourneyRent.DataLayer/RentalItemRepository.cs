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
	}
}
