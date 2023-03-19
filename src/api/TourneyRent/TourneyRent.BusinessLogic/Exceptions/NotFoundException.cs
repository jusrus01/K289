namespace TourneyRent.BusinessLogic.Exceptions
{
    internal class NotFoundException : BaseDomainException
    {
        public NotFoundException(string errorMessage, Exception? inner = null)
            :
            base(errorMessage, inner)
        {
        }
    }
}
