using TyreKlicker.Application.Exceptions;

namespace TyreKlicker.API.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string errorCode, string errorScenario) : base(errorCode, errorScenario)
        {
            Code = errorCode;
            ErrorScenario = errorScenario;
        }
    }
}