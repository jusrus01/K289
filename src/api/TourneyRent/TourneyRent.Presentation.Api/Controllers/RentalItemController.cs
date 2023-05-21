using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourneyRent.BusinessLogic.Exceptions;
using TourneyRent.BusinessLogic.Models;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.DataLayer.Models;
using TourneyRent.DataLayer.Repositories;
using TourneyRent.Presentation.Api.Views.RentalItems;

namespace TourneyRent.Presentation.Api.Controllers;

[Route("RentalItem")]
[ApiController]
public class RentalItemController : Controller
{
    private readonly TransactionExecutor _executor;
    private readonly ImageRepository _imageRepository;
    private readonly IMapper _mapper;
    private readonly RentalItemService _rentalItemService;

    public RentalItemController(RentalItemService rentalItemService, IMapper mapper, TransactionExecutor executor,
        ImageRepository imageRepository)
    {
        _rentalItemService = rentalItemService;
        _mapper = mapper;
        _executor = executor;
        _imageRepository = imageRepository;
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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateRentalItem([FromForm] ManageRentalItemView itemCreate,
        CancellationToken cancellationToken = default)
    {
        var args = _mapper.Map<CreateRentalItemArgs>(itemCreate);
        var availableDays = (JsonSerializer.Deserialize<List<DateTime>>(itemCreate.AvailableAt) ?? new List<DateTime>())
            .ToList();
        args.CalendarItems = availableDays;

        await _rentalItemService.CreateRentalItemAsync(args, cancellationToken);
        var itemView = _mapper.Map<RentalItemDetailedView>(args);
        return CreatedAtAction(nameof(CreateRentalItem), itemView);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteRentalItemAsync(int id)
    {
        var rentalItem = await _rentalItemService.GetRentalItemAsync(id);
        if (rentalItem == null)
        {
            return NotFound();
        }

        try
        {
            await _rentalItemService.DeleteRentalItemAsync(rentalItem);
        }
        catch (Exception e)
        {
            throw new NotFoundException(e.Message);
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateRentalItemAsync(int id, [FromForm] ManageRentalItemView rentalUpdate)
    {
        await _executor.ExecuteAsync(async context =>
        {
            var rental = await context.RentalItems
                .Include(i => i.AvailableDays)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (rental == null)
            {
                throw new NotFoundException("Could not find rental item");
            }

            rental.Price = rentalUpdate.Price;
            rental.Description = rentalUpdate.Description;
            rental.Name = rentalUpdate.Name;
            rental.BankAccountName = rentalUpdate.BankAccountName;
            rental.BankAccountNumber = rentalUpdate.BankAccountNumber;
            rental.TransactionReason = rentalUpdate.TransactionReason;

            // assuming that the front-end will give correct dates :)
            var updatedAvailableDays = (JsonSerializer.Deserialize<List<DateTime>>(rentalUpdate.AvailableAt) ??
                                        new List<DateTime>())
                .Distinct()
                .ToList();
            var previousAvailableDays = rental.AvailableDays.Select(i => i.AvailableAt).ToList();
            var daysToDelete = previousAvailableDays.Except(updatedAvailableDays);

            context.CalendarItems.RemoveRange(rental.AvailableDays.Where(i => daysToDelete.Contains(i.AvailableAt))
                .ToList());

            var datesToUpdate = rental.AvailableDays
                .Select(i => i.AvailableAt)
                .Where(i => updatedAvailableDays.Contains(i))
                .ToList();
            var datesToAdd = updatedAvailableDays.Except(datesToUpdate);

            await context.CalendarItems.AddRangeAsync(datesToAdd.Select(i => new CalendarIRentalItemEntry
            {
                ItemId = rental.Id,
                AvailableAt = i,
                Price = rentalUpdate.Price
            }));

            var calendarItemsToUpdate = context.CalendarItems
                .Where(i => datesToUpdate.Contains(i.AvailableAt))
                .ToList();
            foreach (var i in calendarItemsToUpdate)
            {
                if (i.BuyerId != null)
                {
                    continue;
                }

                i.Price = rentalUpdate.Price;
            }

            context.CalendarItems.UpdateRange(calendarItemsToUpdate);

            if (rentalUpdate.ImageFile != null)
            {
                rental.ImageId = await _imageRepository
                    .UploadImageAsync(rentalUpdate)
                    .ConfigureAwait(false);
            }

            context.RentalItems.Update(rental);
        });

        return NoContent();
    }

    [HttpPut("{id}/highlight")]
    [Authorize]
    public async Task<IActionResult> UpdateHighlightCostAsync([FromRoute] int id,
        [FromBody] RentalItemHighlightFeeView price)
    {
        await _rentalItemService.UpdateHighlightCostAsync(id, price.Fee);

        return NoContent();
    }

    [HttpGet("{id:int}/AvailableDays")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<DateTime>>> GetAvailableDays(int id)
    {
        return Ok((await _rentalItemService.GetAvailableDaysAsync(id).ConfigureAwait(false)).Select(i => i.AvailableAt)
            .ToList());
    }
}