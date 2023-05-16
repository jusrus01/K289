namespace TourneyRent.BusinessLogic.Exceptions
{
    public class NotFoundException : BaseDomainException
    {
        public NotFoundException(string errorMessage, Exception inner = null)
            :
            base(errorMessage, inner)
        {
        }
    }
}
