using AutoMapper;
using TourneyRent.BusinessLogic.Models.Prizes;
using TourneyRent.DataLayer.Repositories;

namespace TourneyRent.BusinessLogic.Services;

public class PrizeService
{
    private readonly PrizeRepository _prizeRepository;
    private readonly IMapper _mapper;

    public PrizeService(IMapper mapper, PrizeRepository prizeRepository)
    {
        _mapper = mapper;
        _prizeRepository = prizeRepository;
    }

    public async Task<IEnumerable<PrizeInfo>> GetAvailablePrizesAsync()
    {
        var prizes = await _prizeRepository.GetAsync(prize => prize.TournamentId == null);
        return _mapper.Map<IEnumerable<PrizeInfo>>(prizes);
    }
}