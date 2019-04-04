namespace TyreKlicker.Application.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string errorCode, string errorScenario) : base(errorCode, errorScenario)
        {
        }
    }
}