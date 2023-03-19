using AutoMapper;
using Microsoft.AspNetCore.Http;
using TourneyRent.BusinessLogic.Extensions;
using TourneyRent.BusinessLogic.Models.Tournaments;
using TourneyRent.DataLayer.Models;
using TourneyRent.DataLayer.Repositories;

namespace TourneyRent.BusinessLogic.Services;

public class TournamentService
{
    private readonly TournamentRepository _tournamentRepository;
    private readonly ImageRepository _imageRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TournamentService(
        IMapper mapper,
        TournamentRepository tournamentRepository,
        ImageRepository imageRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _tournamentRepository = tournamentRepository;
        _imageRepository = imageRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<TournamentInfo> DeleteAsync(int id)
    {
        return _mapper.Map<TournamentInfo>(await _tournamentRepository.DeleteAsync(id));
    }

    public async Task<IEnumerable<TournamentInfo>> GetAllValidAsync()
    {
        var tournaments = await _tournamentRepository.GetAsync(x => x.EndDate >= DateTime.UtcNow);
        return _mapper.Map<IEnumerable<TournamentInfo>>(tournaments);
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

    public async Task<TournamentInfo> GetTournamentByIdAsync(int id)
    {
        var tournaments = await _tournamentRepository.GetAsync(x => x.Id == id);
        return _mapper.Map<TournamentInfo>(tournaments.Single());
    }
}