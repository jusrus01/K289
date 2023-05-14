using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TourneyRent.BusinessLogic.Exceptions;
using TourneyRent.BusinessLogic.Extensions;
using TourneyRent.BusinessLogic.Models.Tournaments;
using TourneyRent.Contracts.Enums;
using TourneyRent.DataLayer;
using TourneyRent.DataLayer.Models;
using TourneyRent.DataLayer.Repositories;

namespace TourneyRent.BusinessLogic.Services;

public class TournamentService
{
    private readonly TransactionExecutor _executor;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ImageRepository _imageRepository;
    private readonly MailService _mailService;
    private readonly AccountService _accountService;
    private readonly IMapper _mapper;
    private readonly PaymentTransactionRepository _paymentTransactionRepository;
    private readonly PrizeRepository _prizeRepository;
    private readonly TeamRepository _teamRepository;
    private readonly TournamentRepository _tournamentRepository;

    public TournamentService(
        IMapper mapper,
        TournamentRepository tournamentRepository,
        ImageRepository imageRepository,
        IHttpContextAccessor httpContextAccessor,
        TransactionExecutor executor,
        PaymentTransactionRepository paymentTransactionRepository,
        TeamRepository teamRepository,
        PrizeRepository prizeRepository,
        MailService mailService,
        AccountService accountService)
    {
        _tournamentRepository = tournamentRepository;
        _imageRepository = imageRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _executor = executor;
        _paymentTransactionRepository = paymentTransactionRepository;
        _teamRepository = teamRepository;
        _prizeRepository = prizeRepository;
        _mailService = mailService;
        _accountService = accountService;
    }

    public async Task<TournamentInfo> DeleteAsync(int id)
    {
        var tournament = await _tournamentRepository.GetSingleOrDefaultAsync(x => x.Id == id);

        if (tournament == null)
        {
            throw new NotFoundException("Tournament not found");
        }

        if (tournament.Participants.Any() && tournament.EntryFee > 0)
        {
            throw new TournamentException(
                "Cannot delete tournament that has participants that already payed. Please contact support.");
        }

        if (tournament.TransactionId != null)
        {
            throw new TournamentException("Cannot delete tournament that has bought rental items.");
        }

        return _mapper.Map<TournamentInfo>(await _tournamentRepository.DeleteAsync(tournament));
    }

    public async Task<TournamentInfo> CreateAsync(CreateTournamentArgs createArgs)
    {
        var imageId = await _imageRepository.UploadImageAsync(createArgs);
        var tournament = new Tournament
        {
            EndDate = createArgs.EndDate,
            EntryFee = createArgs.EntryFee,
            StartDate = createArgs.StartDate,
            ImageId = imageId,
            Name = createArgs.Name,
            ParticipantCount = createArgs.ParticipantCount,
            OwnerId = _httpContextAccessor.GetAuthenticatedUserId(),
            BankAccountNumber = createArgs.BankAccountNumber,
            BankAccountName = createArgs.BankAccountName,
            TransactionReason = createArgs.TransactionReason,
            Prizes = new List<Prize>()
        };
        var prize = createArgs.PrizeId != null ? await _prizeRepository.GetByIdAsync(createArgs.PrizeId.Value) : null;

        await _executor.ExecuteAsync(async context =>
        {
            if (prize != null)
            {
                tournament.Prizes.Add(prize);
            }

            // oof
            decimal sum = 0;
            foreach (var reservation in createArgs.Reservation)
            {
                // TODO: handle better
                var rentalItem = await context.RentalItems
                    .Include(i => i.AvailableDays)
                    .FirstAsync(i => i.Id == reservation.Id);
                var days = rentalItem.AvailableDays
                    .Where(i => reservation.Days.Contains(i.AvailableAt))
                    .ToList();

                if (days.Any(i => !string.IsNullOrWhiteSpace(i.BuyerId)))
                {
                    throw new TournamentException("Cannot reserve day. Please clear out your cart and try again.");
                }

                foreach (var day in days)
                {
                    var transactionId =
                        await _paymentTransactionRepository.CreateAsync(_httpContextAccessor.GetAuthenticatedUserId(),
                            day.Price);
                    day.BuyerId = _httpContextAccessor.GetAuthenticatedUserId();
                    day.TransactionId = transactionId;

                    sum += day.Price;
                }

                context.CalendarItems.UpdateRange(days);
            }

            tournament.TransactionId = await _paymentTransactionRepository
                .CreateAsync(
                    _httpContextAccessor.GetAuthenticatedUserId(),
                    sum)
                .ConfigureAwait(false);

            await _tournamentRepository.CreateAsync(tournament);
        });
        return _mapper.Map<TournamentInfo>(tournament);
    }

    public async Task<IEnumerable<TournamentInfo>> GetAllTournamentsAsync()
    {
        var tournaments = await _tournamentRepository.GetAsync(_ => true);
        return _mapper.Map<IEnumerable<TournamentInfo>>(tournaments); // Does not set IsJoined correctly
    }

    public async Task<IEnumerable<TournamentInfo>> GetTournamentsAsync(string ownerId)
    {
        var tournaments = await _tournamentRepository.GetAsync(i => i.OwnerId == ownerId);
        return _mapper.Map<IEnumerable<TournamentInfo>>(tournaments);
    }

    public async Task<IEnumerable<TournamentInfo>> GetJoinedTournamentsAsync(string userId)
    {
        var tournaments = await _tournamentRepository.GetAsync(i => i.Participants.Any(p => p.UserId == userId));
        var tournamentsInfo = _mapper.Map<IEnumerable<TournamentInfo>>(tournaments);
        
        return tournamentsInfo
            .Select(tournament =>
            {
                var participant = tournament.Participants.FirstOrDefault(p => p.UserId == userId);
                
                ParticipantStatus? status = null;
                if (participant.IsWinner)
                {
                    status = ParticipantStatus.Won;
                }
                else if (!tournament.IsWinnerSelected)
                {
                    status = ParticipantStatus.InProgress;
                }
                else
                {
                    status = ParticipantStatus.Lost;
                }

                tournament.ParticipantStatus = status;

                return tournament;
            });
    }

    public async Task<TournamentInfo> GetTournamentByIdAsync(int id)
    {
        var tournaments = await _tournamentRepository.GetAsync(x => x.Id == id);
        var tournament = tournaments.Single();
        var tournamentInfo = _mapper.Map<TournamentInfo>(tournament);
        var userId = _httpContextAccessor.GetAuthenticatedUserId();
        tournamentInfo.IsJoined = tournament.Participants.Any(x => x.UserId == userId);
        return tournamentInfo;
    }

    public async Task JoinAsync(int tournamentId, int? teamId)
    {
        var tournament = await GetTournamentAsync(tournamentId);

        if (tournament.Participants.Count == tournament.ParticipantCount)
            throw new TournamentException("Tournament is full");

        var userId = _httpContextAccessor.GetAuthenticatedUserId();
        if (tournament.Participants.Any(x => x.UserId == userId)) throw new TournamentException("User already joined");

        if (teamId != null)
        {
            var team = await _teamRepository.GetTeamByIdAsync(teamId.Value);
            if (!team.Members.Any(x => x.UserId == userId))
                throw new TournamentException("User is not a part of this team");
        }

        await _executor.ExecuteAsync(async _ =>
        {
            var transactionId = await _paymentTransactionRepository.CreateAsync(
                userId,
                Convert.ToDecimal(tournament.EntryFee));
            var participant = new TournamentParticipant
            {
                TeamId = teamId,
                UserId = userId,
                TransactionId = transactionId,
                TournamentId = tournamentId
            };
            tournament.Participants.Add(participant);
        });
    }

    public async Task LeaveAsync(int tournamentId)
    {
        var tournament = await GetTournamentAsync(tournamentId);
        var userId = _httpContextAccessor.GetAuthenticatedUserId();
        var participant = tournament.Participants.SingleOrDefault(p => p.UserId == userId);
        if (participant == null) throw new TournamentException("User is not in the tournament");

        await _executor.ExecuteAsync(async _ =>
        {
            if (participant.TransactionId != null)
                await _paymentTransactionRepository.RemoveAsync(participant.TransactionId.Value);

            await _tournamentRepository.RemoveParticipantAsync(participant);
        });
    }

    private async Task<Tournament> GetTournamentAsync(int tournamentId)
    {
        var tournament = await _tournamentRepository.GetSingleOrDefaultAsync(x => x.Id == tournamentId);
        if (tournament == null) throw new NotFoundException("Tournament not found");

        return tournament;
    }

    public async Task<TournamentInfo> UpdateTournamentAsync(int id, UpdateTournamentArgs updateArgs)
    {
        var tournamentsToUpdate = await _tournamentRepository.GetAsync(x => x.Id == id);
        var tournamentToUpdate = tournamentsToUpdate.FirstOrDefault();

        if (tournamentToUpdate == null)
            throw new ArgumentException($"Tournament with ID {id} not found");

        //tournamentToUpdate.Name = updateArgs.Name;
        //tournamentToUpdate.StartDate = updateArgs.StartDate.ToUniversalTime();
        //tournamentToUpdate.EndDate = updateArgs.EndDate.ToUniversalTime();
        //tournamentToUpdate.EntryFee = updateArgs.EntryFee;
        //tournamentToUpdate.ParticipantCount = updateArgs.ParticipantCount;
        var imageId = await _imageRepository.UploadImageAsync(updateArgs);

        if(imageId != null)
            tournamentToUpdate.ImageId = imageId;

        var tourney = _mapper.Map(updateArgs, tournamentToUpdate);


        await _tournamentRepository.UpdateTournamentAsync(id, tournamentToUpdate);

        return _mapper.Map<TournamentInfo>(tournamentToUpdate);
    }

    public async Task SelectWinnerAsync(int tournamentId, Guid userId)
    {
        var tournament = await GetTournamentAsync(tournamentId).ConfigureAwait(false);
        if (tournament.EndDate > DateTime.UtcNow)
            throw new TournamentException("Cannot select winner. Tournament is still in progress.");

        var userIdString = userId.ToString();
        var winner = tournament.Participants.SingleOrDefault(i => i.UserId == userIdString);
        if (winner == null) throw new TournamentException("Cannot find selected winner.");

        await _executor.ExecuteAsync(async _ =>
        {
            // TODO: ensure that winner is saved in DB
            tournament.IsWinnerSelected = true;
            winner.IsWinner = true;
            await _tournamentRepository.UpdateAsync(tournament);
        });

#pragma warning disable CS4014
        Task.Run(async () =>
#pragma warning restore CS4014
        {
            await _mailService.SendEmailAsync(
                $"Congratulations! You've won the {tournament.Name} tournament!",
                winner.User.Email,
                winner.User.Email,
                "You have won the tournament. The organizer will contact you with details on how to receive your prize.");

            foreach (var participant in tournament.Participants)
            {
                if (participant.UserId == winner.UserId) continue;

                await _mailService.SendEmailAsync(
                    $"Update about {tournament.Name} tournament",
                    participant.User.Email,
                    participant.User.Email,
                    "Unfortunately you did not win the tournament. Better luck next time!");
            }
        });
    }
}