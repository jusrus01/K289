using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourneyRent.Contracts.Models;

namespace TourneyRent.DataLayer.Models
{
	public class RentalItem : IImage, ITransactionable
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		public Guid? ImageId { get; set; }

		[Required]
		public DateTime PeriodStart { get; set; }

		[Required]
		public DateTime PeriodEnd { get; set; }

		[Required]
		public int Price { get; set; }

        public ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }

		public ICollection<CalendarIRentalItemEntry> AvailableDays { get; set; }

		public string BankAccountName { get; set; }
		public string BankAccountNumber { get; set; }
		public string TransactionReason { get; set; }
	}
}
