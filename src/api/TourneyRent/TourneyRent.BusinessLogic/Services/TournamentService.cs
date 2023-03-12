using AutoMapper;
using TourneyRent.BusinessLogic.Models.Tournaments;
using TourneyRent.DataLayer.Models;
using TourneyRent.DataLayer.Repositories;

namespace TourneyRent.BusinessLogic.Services;

public class TournamentService
{
    private readonly TournamentRepository _tournamentRepository;
    private readonly ImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public TournamentService(IMapper mapper, TournamentRepository tournamentRepository, ImageRepository imageRepository)
    {
        _tournamentRepository = tournamentRepository;
        _imageRepository = imageRepository;
        _mapper = mapper;
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
            ParticipantCount = createArgs.ParticipantCount
        };
        await _tournamentRepository.CreateAsync(tournament);
        return _mapper.Map<TournamentInfo>(tournament);
    }
}