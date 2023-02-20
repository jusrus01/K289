namespace TourneyRent.BusinessLogic.Exceptions
{
    /// <summary>
    /// Other exceptions should inherit from this exception.
    /// Those exceptions should be thrown in the service layer since
    /// GlobalExceptionMiddleware will catch them and return correct
    /// error response for the front-end to handle.
    /// </summary>
    public abstract class BaseDomainException : Exception
    {
        public BaseDomainException(string errorMessage, Exception inner)
            :
            base(errorMessage, inner)
        {
        }
    }
}
