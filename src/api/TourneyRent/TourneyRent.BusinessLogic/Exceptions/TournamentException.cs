namespace TourneyRent.BusinessLogic.Exceptions;

public class TournamentException : BaseDomainException
{
    public TournamentException(string errorMessage, Exception inner = null) : base(errorMessage, inner)
    {
    }
}