using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourneyRent.Contracts.Models;

namespace TourneyRent.BusinessLogic.Models
{
	public class CreateRentalItemArgs : IImageUpload, ITransactionable
	{
		public IFormFile? ImageFile { get; set; }

		public string? BankAccountName { get; set; }
		public string? BankAccountNumber { get; set; }
		public string? TransactionReason { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime PeriodStart { get; set; }
		public DateTime PeriodEnd { get; set; }
		public int Price { get; set; }
		public List<DateTime> CalendarItems { get; set; }

	}
}
