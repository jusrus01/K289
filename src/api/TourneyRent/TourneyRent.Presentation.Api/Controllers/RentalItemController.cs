﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourneyRent.BusinessLogic.Models;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.RentalItems;
using TourneyRent.Presentation.Api.Views.Teams;

namespace TourneyRent.Presentation.Api.Controllers
{
	[Route("RentalItem")]
	[ApiController]
	public class RentalItemController : Controller
	{
		private readonly RentalItemService _rentalItemService;

		private readonly IMapper _mapper;

		public RentalItemController (RentalItemService rentalItemService, IMapper mapper)
		{
			_rentalItemService = rentalItemService;
			_mapper = mapper;
		}

		/*[HttpGet("{id}")]
		public async Task<ActionResult<RentalItemView>> GetRentalItem(int id)
		{
			var rentalItem = await _rentalItemService.GetRentalItemAsync(id);
			if (rentalItem == null) 
			{
				return NotFound();
			}

			var rentalItemdDetailedView = _mapper.Map<RentalItemDetailedView>(rentalItem);
			return Ok(rentalItemdDetailedView);
		}*/
		[HttpGet("{id}")]
		public async Task<IActionResult> GetRentalItem(int id)
		{
			var rentalItem = await _rentalItemService.GetRentalItemAsync(id);
			if (rentalItem == null)
			{
				return NotFound();
			}

			//var rentalItemdDetailedView = _mapper.Map<RentalItemDetailedView>(rentalItem);
			return Ok(rentalItem);
		}

		[HttpGet]
		public async Task<ActionResult<RentalItemView>> GetRentalItems()
		{
			var rentalItems = await _rentalItemService.GetRentalItemsAsync();


			var rentalItemView = _mapper.Map<IEnumerable<RentalItemView>>(rentalItems);
			return Ok(rentalItemView);
		}

		/*[HttpPost, Authorize]
		public async Task<ActionResult<RentalItemView>> CreateRentalItem(RentalItemCreate itemCreate)
		{
			var item = _mapper.Map<RentalItem>(itemCreate);
			await _rentalItemService.CreateRentalItemAsync(item);
			var itemView = _mapper.Map<RentalItemView>(item);
			return CreatedAtAction(nameof(GetRentalItem), new { id = itemView.Id }, itemView);
		}*/
		[HttpPost, Authorize]
		public async Task<IActionResult> CreateRentalItem([FromForm]RentalItemCreate itemCreate)
		{
			var args = _mapper.Map<CreateRentalItemArgs>(itemCreate);
			await _rentalItemService.CreateRentalItemAsync(args);
			var itemView = _mapper.Map<RentalItemDetailedView>(args);
			return CreatedAtAction(nameof(CreateRentalItem), itemView);
		}

			[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRentalItemAsync(int id)
		{
			var rentalItem = await _rentalItemService.GetRentalItemAsync(id);
			if (rentalItem == null)
			{
				return NotFound();
			}

			await _rentalItemService.DeleteRentalItemAsync(rentalItem);

			return NoContent();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateRentalItemAsync(int id, RentalItem rentalUpdate)
		{

			rentalUpdate.Id = id;

			await _rentalItemService.UpdateTeamAsync(rentalUpdate);

			if (await _rentalItemService.GetRentalItemAsync(id) == null)
			{
				return NotFound();
			}

			return NoContent();
		}

	}
}
