﻿using System.ComponentModel.DataAnnotations;

namespace TourneyRent.Presentation.Api.Views.RentalItems
{
	public class RentalItemView
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public int Price { get; set; }
	}
}
