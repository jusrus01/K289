using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.RentalItems;

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
    public async Task<ActionResult<RentalItemView>> GetRentalItem(int id)
    {
        var rentalItem = await _rentalItemService.GetRentalItemAsync(id);
        if (rentalItem == null) return NotFound();

        var rentalItemView = _mapper.Map<RentalItemView>(rentalItem);
        return Ok(rentalItemView);
    }

    [HttpGet]
    public async Task<ActionResult<RentalItemView>> GetRentalItems()
    {
        var rentalItems = await _rentalItemService.GetRentalItemsAsync();


        var rentalItemView = _mapper.Map<IEnumerable<RentalItemView>>(rentalItems);
        return Ok(rentalItemView);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<RentalItemView>> CreateRentalItem(RentalItemCreate itemCreate)
    {
        var item = _mapper.Map<RentalItem>(itemCreate);
        await _rentalItemService.CreateRentalItemAsync(item);
        var itemView = _mapper.Map<RentalItemView>(item);
        return CreatedAtAction(nameof(GetRentalItem), new { id = itemView.Id }, itemView);
    }

    // TODO: return only dates that can be reserved.
    // take into account dates that are before current date etc
    // also sort
    [HttpGet("{id:int}/AvailableDays")]
    [Authorize]
    public ActionResult<IEnumerable<DateTime>> GetAvailableDays(int id)
    {
        return new List<DateTime>
        {
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(10),
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(10),
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(10)
        };
    }
}