﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourneyRent.BusinessLogic.Models;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.CalendarItem;
using TourneyRent.Presentation.Api.Views.RentalItems;
using TourneyRent.Presentation.Api.Views.Teams;

namespace TourneyRent.Presentation.Api.Controllers;

[Route("RentalItem")]
[ApiController]
public class RentalItemController : Controller
{
    private readonly RentalItemService _rentalItemService;

    private readonly IMapper _mapper;

    public RentalItemController(RentalItemService rentalItemService, IMapper mapper)
    {
        _rentalItemService = rentalItemService;
        _mapper = mapper;
    }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetRentalItem(int id)
		{
			var rentalItem = await _rentalItemService.GetRentalItemAsync(id);
			if (rentalItem == null)
			{
				return NotFound();
			}

			var rentalItemdDetailedView = _mapper.Map<RentalItemDetailedView>(rentalItem);
			return Ok(rentalItemdDetailedView);
		}

		[HttpGet]
		public async Task<IActionResult> GetRentalItems()
		{
			var rentalItems = await _rentalItemService.GetRentalItemsAsync();

			var rentalItemView = _mapper.Map<IEnumerable<RentalItemView>>(rentalItems);
			return Ok(rentalItemView);
		}

		[HttpPost, Authorize]
		public async Task<IActionResult> CreateRentalItem([FromForm]RentalItemCreate itemCreate)
		{
			var args = _mapper.Map<CreateRentalItemArgs>(itemCreate);
			await _rentalItemService.CreateRentalItemAsync(args);
			var itemView = _mapper.Map<RentalItemDetailedView>(args);
			return CreatedAtAction(nameof(CreateRentalItem), itemView);
		}

		[Authorize]
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

		[Authorize]
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

		[Authorize]
		[HttpPut("{id}/highlight")]
		public async Task<IActionResult> UpdateHighlightCostAsync([FromRoute] int id, [FromBody] RentalItemHighlightFeeView price)
		{
			await _rentalItemService.UpdateHighlightCostAsync(id, price.Fee);

			return NoContent();
		}
	// TODO: return only dates that can be reserved.
	// take into account dates that are before current date etc
	// also sort
	[HttpGet("{id:int}/AvailableDays")]
		[Authorize]
		public async Task<IActionResult> GetAvailableDays(int id)
		{
			var rentalItem = await _rentalItemService.GetRentalItemAsync(id);
			if (rentalItem == null)
			{
				return NotFound();
			}

			var calendarItemView = _mapper.Map<CalendarItemView>(rentalItem);

			return Ok(calendarItemView);
	}

}

    
