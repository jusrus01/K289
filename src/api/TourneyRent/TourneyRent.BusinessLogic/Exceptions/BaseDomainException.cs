namespace TourneyRent.BusinessLogic.Exceptions
{
    public abstract class BaseDomainException : Exception
    {
        public BaseDomainException(string errorMessage, Exception inner)
            :
            base(errorMessage, inner)
        {
        }
    }
}
