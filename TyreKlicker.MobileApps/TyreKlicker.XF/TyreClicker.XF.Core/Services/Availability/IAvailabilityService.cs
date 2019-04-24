using System.Collections.Generic;

namespace TyreKlicker.XF.Core.Services.Availability
{
    public interface IAvailabilityService
    {
        IEnumerable<Models.Availability.Availability> GetNext2Weeks();
    }
}