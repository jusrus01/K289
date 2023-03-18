using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourneyRent.DataLayer.Models
{
	public class RentalItem
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		//[Required]
		//public byte[] Image { get; set; }

		public string Image { get; set; }

		[Required]
		public DateTime PeriodStart { get; set; }

		[Required]
		public DateTime PeriodEnd { get; set; }

		[Required]
		public int Price { get; set; }

        public ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }
    }
}
