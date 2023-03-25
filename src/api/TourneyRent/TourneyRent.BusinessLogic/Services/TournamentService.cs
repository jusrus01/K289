using AutoMapper;
using Microsoft.AspNetCore.Http;
using TourneyRent.BusinessLogic.Exceptions;
using TourneyRent.BusinessLogic.Extensions;
using TourneyRent.BusinessLogic.Models.Tournaments;
using TourneyRent.DataLayer;
using TourneyRent.DataLayer.Models;
using TourneyRent.DataLayer.Repositories;

namespace TourneyRent.BusinessLogic.Services;

public class TournamentService
{
    private readonly TournamentRepository _tournamentRepository;
    private readonly ImageRepository _imageRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TransactionExecutor _executor;
    private readonly PaymentTransactionRepository _paymentTransactionRepository;
    private readonly TeamRepository _teamRepository;

    public TournamentService(
        IMapper mapper,
        TournamentRepository tournamentRepository,
        ImageRepository imageRepository,
        IHttpContextAccessor httpContextAccessor,
        TransactionExecutor executor,
        PaymentTransactionRepository paymentTransactionRepository,
        TeamRepository teamRepository)
    {
        _tournamentRepository = tournamentRepository;
        _imageRepository = imageRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _executor = executor;
        _paymentTransactionRepository = paymentTransactionRepository;
        _teamRepository = teamRepository;
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
            throw new TournamentException("Cannot delete tournament that has participants that already payed. Please contact support.");
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
            BankAccountNumber = createArgs.BankAccountNumber ?? string.Empty,
            BankAccountName = createArgs.BankAccountName ?? string.Empty,
            TransactionReason = createArgs.TransactionReason ?? string.Empty
        };
        await _tournamentRepository.CreateAsync(tournament);
        return _mapper.Map<TournamentInfo>(tournament);
    }
    
    public async Task<IEnumerable<TournamentInfo>> GetAllValidAsync()
    {
        var tournaments = await _tournamentRepository.GetAsync(x => x.EndDate >= DateTime.UtcNow);
        return _mapper.Map<IEnumerable<TournamentInfo>>(tournaments); // Does not set IsJoined correctly
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
        {
            throw new TournamentException("Tournament is full");
        }
        
        var userId = _httpContextAccessor.GetAuthenticatedUserId();
        if (tournament.Participants.Any(x => x.UserId == userId))
        {
            throw new TournamentException("User already joined");
        }

        if (teamId != null)
        {
            var team = await _teamRepository.GetTeamByIdAsync(teamId.Value);
            if (!team.Members.Any(x => x.UserId == userId))
            {
                throw new TournamentException("User is not a part of this team");
            }
        }

        await _executor.ExecuteAsync(async () =>
        {
            var transactionId = await _paymentTransactionRepository.CreateAsync(
                userId,
                Convert.ToDecimal(tournament.EntryFee));
            var participant = new TournamentParticipant
            {
                TeamId = teamId,
                UserId = userId,
                TransactionId = transactionId,
                TournamentId = tournamentId,
            };
            tournament.Participants.Add(participant);
        });
    }

    public async Task LeaveAsync(int tournamentId)
    {
        var tournament = await GetTournamentAsync(tournamentId);
        var userId = _httpContextAccessor.GetAuthenticatedUserId();
        var participant = tournament.Participants.SingleOrDefault(p => p.UserId == userId);
        if (participant == null)
        {
            throw new TournamentException("User is not in the tournament");
        }
        
        await _executor.ExecuteAsync(async () =>
        {
            if (participant.TransactionId != null)
            {
                await _paymentTransactionRepository.RemoveAsync(participant.TransactionId.Value);
            }
            
            await _tournamentRepository.RemoveParticipantAsync(participant);
        });
    }

    private async Task<Tournament?> GetTournamentAsync(int tournamentId)
    {
        var tournament = await _tournamentRepository.GetSingleOrDefaultAsync(x => x.Id == tournamentId);
        if (tournament == null)
        {
            throw new NotFoundException("Tournament not found");
        }

        return tournament;
    }
}