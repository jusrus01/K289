using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TourneyRent.DataLayer.Models;


namespace TourneyRent.DataLayer.Repositories;

public class TournamentRepository
{
    private readonly TourneyRentDbContext _context;

    public TournamentRepository(TourneyRentDbContext context)
    {
        _context = context;
    }

    public async Task<Tournament> GetSingleOrDefaultAsync(Expression<Func<Tournament, bool>> predicate)
    {
        return await _context.Tournaments.Include(x => x.Participants).SingleOrDefaultAsync(predicate);
    }
    
    public async Task<IEnumerable<Tournament>> GetAsync(Expression<Func<Tournament, bool>> predicate)
    {
        return await _context.Tournaments
            .Include(x => x.Participants)
            .Include(x => x.Prizes)
            .Where(predicate)
            .OrderByDescending(t => t.StartDate)
            .ThenByDescending(t => t.EndDate)
            .ToListAsync();
    }

    public async Task<Tournament> DeleteAsync(Tournament tournament)
    {
        _context.Participants.RemoveRange(tournament.Participants);
        var deletedTournament = new Tournament
        {
            Id = tournament.Id,
            Name = tournament.Name,
            StartDate = tournament.StartDate,
            EndDate = tournament.EndDate,
            EntryFee = tournament.EntryFee,
            ParticipantCount = tournament.ParticipantCount,
            ImageId = tournament.ImageId,
            OwnerId = tournament.OwnerId
        };
        _context.Tournaments.Remove(tournament);
        await _context.SaveChangesAsync();
        return deletedTournament;
    }

    public async Task<Tournament> UpdateTournamentAsync(int id, Tournament tournament)
    {
        var tournamentToUpdate = await _context.Tournaments.SingleAsync(x => x.Id == id);

        var updatedTourney = new Tournament();

        //if (tournament.Name == null)
        //    updatedTourney.Name = tournamentToUpdate.Name;
        //else
        //    updatedTourney.Name = tournament.Name;


        //if (tournament.StartDate == null)
        //    updatedTourney.StartDate = tournamentToUpdate.StartDate;
        //else
        //    updatedTourney.StartDate = tournament.StartDate;


        //if (tournament.EndDate == null)
        //    updatedTourney.EndDate = tournamentToUpdate.EndDate;
        //else
        //    updatedTourney.EndDate = tournament.EndDate;


        //if (tournament.EntryFee == null)
        //    updatedTourney.EntryFee = tournamentToUpdate.EntryFee;
        //else
        //    updatedTourney.EntryFee = tournament.EntryFee;


        //if (tournament.ParticipantCount == null)
        //    updatedTourney.ParticipantCount = tournamentToUpdate.ParticipantCount;
        //else
        //    updatedTourney.ParticipantCount = tournament.ParticipantCount;


        //if (tournament.ImageId == null)
        //    updatedTourney.ImageId = tournamentToUpdate.ImageId;
        //else
        //    updatedTourney.ImageId = tournament.ImageId;

        //if (tournament.BankAccountName == null)
        //    updatedTourney.BankAccountName = tournamentToUpdate.BankAccountName;
        //else
        //    updatedTourney.BankAccountName = tournament.BankAccountName;

        //if (tournament.BankAccountNumber == null)
        //    updatedTourney.BankAccountNumber = tournamentToUpdate.BankAccountNumber;
        //else
        //    updatedTourney.BankAccountNumber = tournament.BankAccountNumber;

        //if (tournament.TransactionReason == null)
        //    updatedTourney.TransactionReason = tournamentToUpdate.TransactionReason;
        //else
        //    updatedTourney.TransactionReason = tournament.TransactionReason;

        Console.WriteLine("DEBUG");

        await _context.SaveChangesAsync();

        return tournamentToUpdate;
    }

    public async Task CreateAsync(Tournament tournament)
    {
        await _context.AddAsync(tournament);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveParticipantAsync(TournamentParticipant participant)
    {
        _context.Participants.Remove(participant);
        await _context.SaveChangesAsync();
    }
}